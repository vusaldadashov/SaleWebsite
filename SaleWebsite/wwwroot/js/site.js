// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var imgs = document.querySelectorAll(".imgs-small");
var poster = document.getElementById("PosterImg");
let imgIndex = 0;

function slideButtonHandler(inc) { //inc is 1 or -1 for button action
    imgIndex += inc
    if (imgIndex == -1) imgIndex = imgs.length - 1
    if (imgIndex == imgs.length) imgIndex = 0
    setPosterUrl(imgIndex)
}
function setPosterUrl(index) {
    imgIndex = index
    poster.setAttribute("src", imgs[index].src)
}


function removeImage(id) {
    let imageCardStack = document.getElementById("image_card_stack")
    let ImagesToRemove = document.getElementById("ImagesToRemove");
    let image_card = document.getElementById("image_card_" + id);
    imageCardStack.removeChild(image_card)
    ImagesToRemove.value += id + ',';
}


// script.js
// script.js
const imagesInput = document.getElementById('imagesInput');
const imagesPreview = document.getElementById('imagesPreview');
const imagesCount = document.getElementById("imagesCount");
var selectedFiles = [];
function imagesInputOnChange() {
    imagesPreview.innerHTML = ''; // Clear previous preview
    console.log("Input File is used")
    for (const file of imagesInput.files) {
        selectedFiles.push(file)
    }
    updateImageCount()
    const files = selectedFiles;
    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        // Check if the selected file is an image
        if (file.type.startsWith('image/')) {
            const div = document.createElement('div');
            div.classList.add("imagesPreview");

            const button = document.createElement('button');
            button.classList.add("btn-close");
            button.classList.add("close_button")

            const img = document.createElement('img');
            img.style.maxWidth = '100%';
            img.style.height = 'auto';
            img.src = URL.createObjectURL(file);

            div.appendChild(img);
            div.appendChild(button);
            imagesPreview.appendChild(div);

            button.addEventListener("click", function () {
                imagesPreview.removeChild(div);
                removeFileFromFileList(file);
            });
        } else {
            alert('Please select an image file.');
        }
    }
}

function removeFileFromFileList(IndexFile) {
    const dt = new DataTransfer();
    const files = selectedFiles

    for (let i = 0; i < files.length; i++) {
        const file = files[i]
        if (IndexFile !== file)
            dt.items.add(file) // here you exclude the file. thus removing it.
    }

    selectedFiles = selectedFiles.filter(fl => fl !== IndexFile)

    updateImageCount()

    imagesInput.files = dt.files // Assign the updates list
}

function updateImageCount() {
    let length = selectedFiles.length
    let msg = ""
    if (length == 1) msg = "1 image selected"
    if (length > 1) msg = length + " images selected"
    imagesCount.textContent = msg;
}
