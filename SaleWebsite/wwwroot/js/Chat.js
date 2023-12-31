﻿var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();

var UserChatDisplay = document.getElementById("UserChatDisplay");
var ChatMessagesList = document.getElementById("ChatMessagesList")
var ChatRecipientList = document.getElementById("ChatRecipientList")
var user = null;
var chat = null;
var receiver = null;
var chatDisplayed = false;
var selectedChat = 0;
connection.start()

function sendMessage() {
    let messageInput = document.getElementById("UserMessageContent");
    connection.invoke("SendMessage", user.userId, receiver.userId, chat.id, messageInput.value).catch(err => console.error(err));
    messageInput.value = "";
}


connection.on("ReceiveMessage", (_chatMessage) => {
    if (_chatMessage.chatId == chat.id) {
        createListItem(_chatMessage);
        ChatMessagesList.scrollTop = ChatMessagesList.scrollHeight;
    }
    else {
        connection.invoke("GetRecipients");
    }
})

connection.on("GetRecipients", (_user) => {
    user = _user;
    ChatRecipientList.innerHTML = ''

    for (const _chat of user.chats) {
        let active = "white"
        if (_chat.id == selectedChat) active = "#eee"
        createRecipientItem(_chat, active)
    }
})

connection.on("GetMessages", (_chat) => {
    chat = _chat
    ChatMessagesList.innerHTML = "";
    UserChatDisplay.innerHTML = "";
    appendUserChat();
    
    for (const msg of _chat.chatMessages) {
        createListItem(msg);
    }
    ChatMessagesList.scrollTop = ChatMessagesList.scrollHeight;
})


function getChatMessages(_chatId) {
    if (chat == null) {
        document.getElementById("chatForm").classList.remove("d-none");
        document.getElementById("chatPoster").classList.add("d-none");
    }
    connection.invoke("GetMessages", _chatId);
    receiver = user.chats.find(x => x.id === _chatId).participants[0];
    selectedChat = _chatId;
}
setInterval(() => {
    if (receiver != null) {
        connection.invoke("GetMessages", selectedChat)
    }
}, 60000)

function sendMessageKeyHandler(event) {
    if (event.key === "Enter") sendMessage();   
}


function appendUserChat() {
    
    let divElement = document.createElement("div");
    divElement.classList.add("w-100", "d-flex", "gap-2", "border-start", "pt-2");
    divElement.style.backgroundColor = "#229ED9";

    // Create an img element with the specified attributes
    let imgElement = document.createElement("img");
    let imgUrl = "/imgs/user/BlankImage.webp"

    if (receiver.img.length > 0) imgUrl = receiver.img;
    imgElement.src = imgUrl;
    imgElement.alt = "avatar";
    imgElement.classList.add("rounded-circle", "d-flex", "align-self-start", "me-3", "shadow-1-strong", "ms-2", 'object-fit-cover');
    imgElement.width = "30";
    imgElement.height = "30"

    // Create a p element with the class "text-black" and text content
    let pElement = document.createElement("p");
    pElement.classList.add("text-white");
    pElement.textContent = receiver.name + " " + receiver.surname;

    // Append the img and p elements to the div element
    divElement.appendChild(imgElement);
    divElement.appendChild(pElement);

    UserChatDisplay.appendChild(divElement)
}

function createListItem(msg) {
    
    // Create a new list item (li) element
    const li = document.createElement('li');
    li.className = 'd-flex mb-4 gap-2 align-items-center';
    if (msg.senderId == user.userId) li.classList.add("align-self-end");
    
    li.style.width = "fit-content"
    // Create an image element
    const img = document.createElement('img');
    let imgUrl = "/imgs/user/BlankImage.webp"
    if (msg.sender.img.length > 0) imgUrl = msg.sender.img;
    img.src = imgUrl;
    img.alt = 'avatar';
    img.className = 'rounded-circle d-flex shadow-1-strong object-fit-cover';
    img.width = '60';
    img.height = '60';

    // Create a card element
    const card = document.createElement('div');
    card.classList.add('card','rounded-lg')
    

    // Create a card header element
    const cardHeader = document.createElement('div');
    cardHeader.className = 'card-header d-flex align-items-center gap-2 bg-white p-3';

    // Create a name paragraph element
    const nameParagraph = document.createElement('p');
    nameParagraph.className = 'fw-bold m-0';
    let _title = "You";
    if (msg.senderId != user.userId) {
        _title = msg.sender.name + " " + msg.sender.surname
    }
    nameParagraph.textContent = _title;

    // Create a time paragraph element
    const timeParagraph = document.createElement('p');
    timeParagraph.className = 'text-muted small m-0';
    timeParagraph.textContent = formatTimeDifference(msg.timestamp);

    // Append name and time paragraphs to the card header
    cardHeader.appendChild(nameParagraph);
    cardHeader.appendChild(timeParagraph);

    // Create a card body element
    const cardBody = document.createElement('div');
    cardBody.className = 'card-body p-3';

    // Create a content paragraph element
    const contentParagraph = document.createElement('p');
    contentParagraph.className = 'm-0';
    contentParagraph.textContent = msg.message;
    // Append the content paragraph to the card body
    cardBody.appendChild(contentParagraph);

    card.appendChild(cardHeader);
    card.appendChild(cardBody);

    // Append the image and card to the list item
    if (msg.senderId == receiver.userId) {
        li.appendChild(img);
        li.appendChild(card);
    }
    else {
        li.appendChild(card);
        li.appendChild(img);
    }
    
    ChatMessagesList.appendChild(li)
}

function createRecipientItem(_chat, _active) {
    console.log(_chat)
    // Create li element
    var listItem = document.createElement('li');
    listItem.classList.add('p-2', 'border-bottom', 'rounded');
    listItem.style.backgroundColor = _active;
    listItem.setAttribute("onclick", `getChatMessages(${_chat.id})`)

    // Create anchor element
    var anchorElement = document.createElement('a');
    anchorElement.href = '#';
    anchorElement.classList.add('d-flex', 'justify-content-between', 'text-decoration-none');

    // Create the first div with flex-row class
    var firstDiv = document.createElement('div');
    firstDiv.classList.add('d-flex', 'flex-row');

    // Create image element
    var imageElement = document.createElement('img');
    let imgUrl = "/imgs/user/BlankImage.webp"
    if (_chat.participants[0].img.length > 0) imgUrl = _chat.participants[0].img;
    imageElement.src = imgUrl;
    imageElement.alt = 'avatar';
    imageElement.classList.add('rounded-circle', 'd-flex', 'align-self-center', 'me-3', 'shadow-1-strong', 'object-fit-contain');
    imageElement.width = '50';
    imageElement.height = '50'

    // Create the second div inside the first div
    var secondDiv = document.createElement('div');
    secondDiv.classList.add('pt-1');

    // Create name paragraph element
    var nameParagraph = document.createElement('p');
    nameParagraph.classList.add('fw-bold', 'mb-0');
    //nameParagraph.textContent = _recipient.name + " " + _recipient.surname;
    nameParagraph.textContent = _chat.participants[0].name + " " + _chat.participants[0].surname;

    //create last message label
    let messageParagraph = document.createElement('p')
    messageParagraph.classList.add('small', 'text-muted')
    if (_chat.lastMessage != null) messageParagraph.textContent = _chat.lastMessage.message
    // Append name and message paragraphs to the second div
    secondDiv.appendChild(nameParagraph);
    if (_chat.lastMessage != null)  secondDiv.appendChild(messageParagraph);
    // Append image and second div to the first div
    firstDiv.appendChild(imageElement);
    firstDiv.appendChild(secondDiv);

    // Create the third div
    var thirdDiv = document.createElement('div');
    thirdDiv.classList.add('pt-1');

    // Create time paragraph element
    var timeParagraph = document.createElement('p');
    timeParagraph.classList.add('small', 'text-muted', 'mb-1');
    if (_chat.lastMessage != null) timeParagraph.textContent = formatTimeDifference(_chat.lastMessage.timestamp);

    // Create badge span element
    var badgeSpan = document.createElement('span');
    badgeSpan.classList.add('badge', 'bg-danger', 'float-end');
    badgeSpan.textContent = _chat.unreadMessagesCount;

    // Append time paragraph and badge span to the third div
    if (_chat.lastMessage != null) thirdDiv.appendChild(timeParagraph);
    if (_chat.unreadMessagesCount > 0) thirdDiv.appendChild(badgeSpan);

    // Append the first div and third div to the anchor element
    anchorElement.appendChild(firstDiv);
    anchorElement.appendChild(thirdDiv);

    // Append the anchor element to the list item
    listItem.appendChild(anchorElement);

    // Append the list item to the parent node
     // Replace 'yourParentNodeId' with the actual ID of your parent node
    ChatRecipientList.appendChild(listItem);

}


function formatTimeDifference(timestamp) {
    const now = new Date();
    const messageTime = new Date(timestamp);

    // Calculate the time difference in milliseconds
    const timeDifference = now - messageTime;

    // Convert milliseconds to seconds
    const seconds = Math.floor(timeDifference / 1000);

    // Define time intervals
    const minute = 60;
    const hour = minute * 60;
    const day = hour * 24;

    // Determine the appropriate time format
    if (seconds < minute) {
        return 'Just now';
    } else if (seconds < hour) {
        const minutes = Math.floor(seconds / minute);
        return `${minutes} minute${minutes > 1 ? 's' : ''} ago`;
    } else if (seconds < day) {
        const hours = Math.floor(seconds / hour);
        return `${hours} hour${hours > 1 ? 's' : ''} ago`;
    } else if (messageTime.toDateString() === now.toDateString()) {
        return 'Today';
    } else if (messageTime.toDateString() === new Date(now - day).toDateString()) {
        return 'Yesterday';
    } else {
        // For messages older than yesterday, you can customize the display further
        const days = Math.floor(seconds / day);
        return `${days} day${days > 1 ? 's' : ''} ago`;
    }
}
