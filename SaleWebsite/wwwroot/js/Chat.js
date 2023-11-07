const { signalR } = require("./signalr/dist/browser/signalr");

let connection = new signalR.HubConnectionBuilder()
    .WithUrl('/ChatHub', { qs: { clientId } })
    .build();

var ChatMessagesList = document.getElementById("ChatMessagesList")


connection.on("ReceiveMessage", (user, message) => {
    const ListItem = createListItem(user, message);
    ChatMessagesList.appendChild(ListItem);
})

function sendMessage() {
    const user = 

    connection.invoke("SendMessage", user, message).catch(err => console.error(err));
}


connection.start()

function createListItem(user, message) {
    // Create a new list item (li) element
    const li = document.createElement('li');
    li.className = 'd-flex justify-content-between mb-4';

    // Create an image element
    const img = document.createElement('img');
    img.src = 'https://mdbcdn.b-cdn.net/img/Photos/Avatars/avatar-6.webp';
    img.alt = 'avatar';
    img.className = 'rounded-circle d-flex align-self-start me-3 shadow-1-strong';
    img.width = '60';

    // Create a card element
    const card = document.createElement('div');
    card.className = 'card';

    // Create a card header element
    const cardHeader = document.createElement('div');
    cardHeader.className = 'card-header d-flex justify-content-between p-3';

    // Create a name paragraph element
    const nameParagraph = document.createElement('p');
    nameParagraph.className = 'fw-bold mb-0';
    nameParagraph.textContent = 'Brad Pitt';

    // Create a time paragraph element
    const timeParagraph = document.createElement('p');
    timeParagraph.className = 'text-muted small mb-0';
    timeParagraph.innerHTML = '<i class="far fa-clock"></i> 12 mins ago';

    // Append name and time paragraphs to the card header
    cardHeader.appendChild(nameParagraph);
    cardHeader.appendChild(timeParagraph);

    // Create a card body element
    const cardBody = document.createElement('div');
    cardBody.className = 'card-body';

    // Create a content paragraph element
    const contentParagraph = document.createElement('p');
    contentParagraph.className = 'mb-0';
    contentParagraph.textContent = message;
    // Append the content paragraph to the card body
    cardBody.appendChild(contentParagraph);

    // Append the image and card to the list item
    li.appendChild(img);
    li.appendChild(card);

    return li;
}