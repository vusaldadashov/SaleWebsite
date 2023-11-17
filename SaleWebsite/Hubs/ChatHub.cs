using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SaleWebsite.Models;
using SaleWebsite.Session_Extensions;
using System.Collections.Concurrent;
using System.Diagnostics;
namespace SaleWebsite.Hubs;

public class ChatHub : Hub
{
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _context;
    private static readonly ConcurrentDictionary<int, string> Conns = new();
    public ChatHub(DataContext dataContext, IHttpContextAccessor context)
    {
        _dataContext = dataContext;
        _context = context;
    }

    #region When Server Connected to client
    public override async Task OnConnectedAsync()
    {
        // When a user connects, associate their user ID with their connection ID
        var user = _context?.HttpContext?.Session.GetObjectFromJson<User>("user");
        if (user == null)
        {
            await Task.CompletedTask; return;
        }
        Conns[user.UserId] = Context.ConnectionId;

        await GetRecipients();
        await base.OnConnectedAsync();
    }

    #endregion

    #region SendMessage Method
    public async Task SendMessage(int senderId, int receiverId, int chatId, string message)
    {
        string ViewStatus = "unread";
        string connId = "";
        var chatMessage = new ChatMessage()
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            ChatId = chatId,
            Message = message,
            Timestamp = DateTime.Now,
            ViewStatus = ViewStatus
        };
      
        if (Conns.TryGetValue(receiverId, out var connectionId)) connId = connectionId;
        if(connId.Length > 0)
        {
            chatMessage.ViewStatus = "seen";
        }
        await _dataContext.ChatMessages.AddAsync(chatMessage);
        await _dataContext.SaveChangesAsync();


        var messageDetails = _dataContext.ChatMessages
            .Where(m => m.Id == chatMessage.Id)
            .Select(m => new
            {
                m.Id,
                m.Message,
                m.Timestamp,
                m.ViewStatus,
                m.SenderId,
                m.ReceiverId,
                m.ChatId,
                Sender = m.Sender != null ? new
                {
                    m.Sender.UserId,
                    m.Sender.Name,
                    m.Sender.Surname,
                    m.Sender.Img
                    // Include other properties if needed
                } : null,
                Receiver = m.Receiver != null ? new
                {
                    m.Receiver.UserId,
                    m.Receiver.Name,
                    m.Receiver.Surname,
                    m.Receiver.Img
                    // Include other properties if needed
                }: null
            })
            .FirstOrDefault();
        if (connId.Length > 0) { await Clients.Client(connId).SendAsync("ReceiveMessage", messageDetails); }

        await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", messageDetails);
    }

    #endregion

    #region GetChatMessages 
    public async Task GetMessages(int chatId)
    {
        var user = _context?.HttpContext?.Session.GetObjectFromJson<User>("user");
        if (user == null)
        {
            await Task.CompletedTask; return;
        }

        var chatMessages = _dataContext.Chats
            .Where(c => c.Id == chatId)
            .Select(c => new
            {
                c.Id,
                ChatMessages = c.ChatMessages.Select(m => new
                {
                    m.Id,
                    m.ViewStatus,
                    m.Message,
                    m.Timestamp,
                    // Include all properties of the ChatMessage entity
                    m.ChatId,
                    m.SenderId,
                    m.ReceiverId,
                    // Include all properties of the User entity for Sender and Receiver
                    Sender = m.Sender != null ? new
                    {
                        m.Sender.UserId,
                        m.Sender.Name,
                        m.Sender.Surname,
                        m.Sender.Img
                        // Include other properties if needed
                    } : null,
                    Receiver = m.Receiver != null ? new
                    {
                        m.Receiver.UserId,
                        m.Receiver.Name,
                        m.Receiver.Surname,
                        m.Receiver.Img
                        // Include other properties if needed
                    } : null
                })
            })
            .FirstOrDefault();
        

        await Clients.Client(Context.ConnectionId).SendAsync("GetMessages", chatMessages);
        await Task.Delay(1000);
        var updateMessages = _dataContext.ChatMessages
        .Where(m => m.ChatId == chatId && m.ReceiverId == user.UserId && m.ViewStatus == "unread");

        foreach (var message in updateMessages)
        {
            message.ViewStatus = "seen";
            _dataContext.ChatMessages.Update(message);
        }
        await _dataContext.SaveChangesAsync();
       

        await GetRecipients();
    }

    #endregion

    #region GetUserRecipients
    public async Task GetRecipients()
    {
        var user = _context?.HttpContext?.Session.GetObjectFromJson<User>("user");
        if (user == null)
        {
            await Task.CompletedTask; return;
        }

        var userData = _dataContext.Users
            .Where(u => u.UserId == user.UserId)
            .Select(u => new
            {
                u.UserId,
                u.Name,
                u.Surname,
                u.Img,
                Chats = (u.Chats ?? Enumerable.Empty<Chat>()).Select(c => new
                {
                    c.Id,
                    Participants = c.Participants
                        .Where(p => p.UserId != user.UserId) // Exclude the user itself
                        .Select(p => new
                        {
                            p.UserId,
                            p.Name,
                            p.Surname,
                            p.Img
                            // Include other participant properties if needed
                        }),
                    UnreadMessagesCount = _dataContext.ChatMessages
                        .Count(m => m.ReceiverId == user.UserId && m.ChatId == c.Id && m.ViewStatus == "unread")
                })
            })
            .FirstOrDefault();


        await Clients.Client(Context.ConnectionId).SendAsync("GetRecipients",userData);
    }

    #endregion

    #region If Server Disconnected
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var user = _context?.HttpContext?.Session.GetObjectFromJson<User>("user");
        if (user == null)
        {
            return Task.CompletedTask;
        }
        Conns.TryRemove(user.UserId, out _);
        return base.OnDisconnectedAsync(exception);
    }

    #endregion
}