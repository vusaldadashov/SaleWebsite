using Microsoft.AspNetCore.SignalR;
using SaleWebsite.Models;
using SaleWebsite.Session_Extensions;

namespace SaleWebsite.Hubs;
public class ChatHub : Hub
{
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ChatHub(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
    {
        _dataContext = dataContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task SendMessage(string receiverId, string message)
    {
        ISession session = _httpContextAccessor.HttpContext.Session;

        var senderId = session.GetObjectFromJson<User>("user").Id;
        var chatMessage = new ChatMessage()
        {
            SenderId = senderId,
            ReceiverId = int.Parse(receiverId),
            Content = message,
            Timestamp = DateTime.UtcNow,
            UserId = senderId
        };

        await _dataContext.ChatMessages.AddAsync(chatMessage);
        await _dataContext.SaveChangesAsync();

        await Clients.User(senderId.ToString()).SendAsync("ReceiveMessage", message);
        await Clients.User(receiverId).SendAsync("ReceiveMessage" ,message);
    }
}

