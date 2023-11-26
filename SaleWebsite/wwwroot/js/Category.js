var AllCatogories = {
    "home": [ "furniture", "appliances", "decor", "office", "gardening"],
    "transportation": ["cars", "bikes", "motorcycles", "accessories"],
    "technology":[ "electronics", "gadgets", "computers", "accessories"],
    "clothing":[ "men's", "women's", "children's", "shoes", "accessories"],
    "health":[ "vitamins", "supplements", "fitness equipment", "personal care"],
    "entertainment":[ "games", "movies", "music", "books", "toys"],
    "sports":[ "outdoor", "fitness", "team sports", "equipment"],
    "beauty":[ "skincare", "haircare", "makeup", "fragrances"],
    "food":[ "snacks", "beverages", "cooking essentials", "sweets"],
    "office":[ "stationery", "office furniture", "electronics", "supplies"],
    "toys":[ "action figures", "building sets", "dolls", "educational"],
    "books":[ "fiction", "non-fiction", "children's", "audiobooks"],
    "pets":[ "dog supplies", "cat supplies", "small pets", "pet care"],
    "outdoors":[ "camping", "hiking", "fishing", "outdoor gear"],
    "music":[ "instruments", "music accessories", "sheet music"],
    "art":[ "painting", "drawing", "sculpture", "craft supplies"],
    "crafts":[ "knitting", "crocheting", "sewing", "craft supplies"],
    "jewelry":[ "necklaces", "bracelets", "rings", "watches"],
    "travel":[ "luggage", "travel accessories", "backpacks"],
    "games":[ "board games", "card games", "puzzles", "video games"],
    "collectibles":[ "coins", "stamps", "memorabilia", "collectible toys"],
    "fitness":[ "exercise equipment", "wearables", "nutrition"],
    "baby":[ "clothing", "gear", "toys", "feeding"],
    "tools":[ "hand tools", "power tools", "tool accessories", "storage"],
    "gifts":[ "personalized gifts", "gift cards", "gift baskets", "gift wrap"],
    "decor":[ "wall art", "decorative accents", "lighting", "rugs"],
    "party":[ "decorations", "party favors", "tableware", "invitations"],
    "gardening":[ "seeds", "tools", "planters", "garden decor"],
    "vintage":[ "clothing", "collectibles", "furniture", "jewelry"],
    "education":[ "educational toys", "books", "learning aids"],
    "business":[ "office supplies", "professional services", "equipment"],
    "finance":[ "investments", "financial planning", "accounting services"],
    "photography":[ "cameras", "lenses", "photography accessories", "prints"],
    "electronics":[ "smartphones", "tablets", "laptops", "audio devices"],
    "accessories":[ "bags", "hats", "scarves", "wallets"],
    "furniture":[ "living room", "bedroom", "office", "outdoor"],
    "appliances":[ "kitchen", "laundry", "home comfort", "small appliances"]
};

var subAllCategories = {
    "cars":[ "Sedan", "SUV (Sport Utility Vehicle)", "Hatchback", "Coupe", "Convertible", "Truck", "Electric Vehicle (EV)", "Hybrid", "Luxury"],
    "bikes":[ "Mountain Bikes", "Road Bikes", "BMX Bikes", "Hybrid Bikes"],
    "motorcycles":[ "Sport Bikes", "Cruisers", "Touring Bikes", "Dirt Bikes"],
    "electronics":[ "Smartphones", "Tablets", "Laptops", "Audio Devices"],
    "furniture":[ "Living Room", "Bedroom", "Office", "Outdoor"],
    "appliances":[ "Kitchen", "Laundry", "Home Comfort", "Small Appliances"],
    "gadgets":[ "Wearables", "Smart Home Devices", "Fitness Trackers", "VR Headsets"],
    "clothing":[ "Men's", "Women's", "Children's", "Shoes"],
    "vitamins":[ "Multivitamins", "Minerals", "Vitamin C", "Vitamin D"],
    "fitness equipment":[ "Treadmills", "Elliptical Machines", "Weights", "Yoga Mats"],
    "games":[ "Board Games", "Card Games", "Puzzles", "Video Games"],
    "craft supplies":[ "Knitting", "Crocheting", "Sewing", "Art Supplies"],
    "jewelry":[ "Necklaces", "Bracelets", "Rings", "Watches"],
    "backpacks":[ "Hiking Backpacks", "Laptop Backpacks", "Travel Backpacks", "School Backpacks"],
    "board games":[ "Strategy Games", "Party Games", "Family Games", "Card Games"],
    "dog supplies":[ "Dog Food", "Dog Toys", "Dog Beds", "Dog Grooming"],
    "office supplies":[ "Pens", "Notebooks", "Desk Organizers", "Staplers"],
    "personal care":[ "Skincare", "Haircare", "Body Wash", "Fragrances"],
    "stationery":[ "Notebooks", "Pens", "Planners", "Stickers"],
    "action figures":[ "Superheroes", "Movie Characters", "Anime Figures", "Collectible Toys"],
    "wall art":[ "Canvas Prints", "Posters", "Photography", "Wall Decals"],
    "luggage":[ "Suitcases", "Duffel Bags", "Travel Backpacks", "Rolling Luggage"],
    "cameras":[ "DSLR Cameras", "Mirrorless Cameras", "Point-and-Shoot Cameras", "Action Cameras"],
    "smartphones":[ "iPhone", "Samsung Galaxy", "Google Pixel", "OnePlus"],
    "tablets":[ "iPad", "Samsung Galaxy Tab", "Amazon Fire", "Microsoft Surface"],
    "laptops":[ "MacBook", "Dell XPS", "HP Spectre", "Lenovo ThinkPad"],
    "audio devices":[ "Headphones", "Speakers", "Earbuds", "Soundbars"],
    "living room":[ "Sofas", "Coffee Tables", "TV Stands", "Bookshelves"],
    "bedroom":[ "Beds", "Dressers", "Nightstands", "Wardrobes"],
    "office":[ "Desks", "Office Chairs", "Bookcases", "File Cabinets"],
    "outdoor":[ "Patio Furniture", "Outdoor Decor", "Grills", "Gardening Tools"],
    "kitchen":[ "Cookware", "Cutlery", "Small Appliances", "Dining Sets"],
    "laundry":[ "Washers", "Dryers", "Laundry Baskets", "Ironing Boards"],
    "home comfort":[ "Air Conditioners", "Heaters", "Humidifiers", "Fans"],
    "small appliances":[ "Blenders", "Toasters", "Coffee Makers", "Microwaves"],
    "wearables":[ "Smartwatches", "Fitness Trackers", "Smart Glasses", "Hearables"],
    "smart home devices":[ "Smart Thermostats", "Smart Lights", "Smart Locks", "Smart Cameras"],
    "fitness trackers":[ "Fitbit", "Garmin", "Xiaomi", "Samsung"],
    "VR headsets":[ "Oculus Rift", "HTC Vive", "PlayStation VR", "Valve Index"],
    "men's":[ "Shirts", "Pants", "Suits", "Activewear"],
    "women's":[ "Dresses", "Tops", "Bottoms", "Athleisure"],
    "children's":[ "Clothing", "Toys", "Accessories", "Footwear"],
    "shoes":[ "Sneakers", "Boots", "Sandals", "Flats"],
    "multivitamins":[ "Centrum", "One A Day", "Nature Made", "Garden of Life"],
    "minerals":[ "Calcium", "Iron", "Zinc", "Magnesium"],
    "vitamin C":[ "Emergen-C", "Nature's Way", "NOW Foods", "Solaray"],
    "vitamin D":[ "Nature's Bounty", "SmartyPants", "NatureWise", "Solgar"],
    "treadmills":[ "NordicTrack", "ProForm", "Sole Fitness", "LifeSpan"],
    "elliptical machines":[ "Schwinn", "Sole Fitness", "Nautilus", "Horizon Fitness"],
    "weights":[ "Dumbbells", "Kettlebells", "Barbells", "Weight Plates"],
    "yoga mats":[ "Liforme", "Manduka", "Gaiam", "Liforme"],
    "card games":[ "Uno", "Cards Against Humanity", "Exploding Kittens", "Magic: The Gathering"],
    "puzzles":[ "Ravensburger", "Buffalo Games", "White Mountain", "EuroGraphics"],
    "video games":[ "PlayStation", "Xbox", "Nintendo", "PC"]
}



var cars = {
        "Abarth": "",
        "ABM": "", 
        "Acura": "",
        "Alfa Romeo": "",
        "Aprilia": "",
        "Aston Martin": "",
        "ATV": "",
        "Audi": "",
        "Avatr": "",
        "Baic": "",
        "Bajaj": "",
        "Benelli": "",
        "Bentley": "",
        "Bestune": "",
        "BMC": "",
        "BMW": "",
        "Brilliance": "",
        "Buick": "",
        "BYD": "",
        "C.Moto": "",
        "Cadillac": "",
        "Can-Am": "",
        "CFMOTO": "",
        "Changan": "",
        "Chery": "",
        "Chevrolet": "",
        "Chrysler": "",
        "Citroen": "",
        "Dacia": "",
        "Dadi": "",
        "Daewoo": "",
        "DAF": "",
        "Daihatsu": "",
        "Dayun": "",
        "DFSK": "",
        "Dnepr": "",
        "Dodge": "",
        "DongFeng": "",
        "Ducati": "",
        "ERF": "",
        "FAW": "",
        "Ferrari": "",
        "Fiat": "",
        "Ford": "",
        "Foton": "",
        "Gabro": "",
        "GAC": "",
        "GAZ": "",
        "Geely": "",
        "Genesis": "",
        "GMC": "",
        "Grandmoto": "",
        "GWM": "",
        "Haima": "",
        "Haojue": "",
        "Harley-Davidson": "",
        "Haval": "",
        "HiPhi": "",
        "Honda": "",
        "Hongqi": "",
        "HOWO": "",
        "Hozon": "",
        "Hummer": "",
        "Hyundai": "",
        "IJ": "",
        "Ikarus": "",
        "IM": "",
        "Infiniti": "",
        "Iran Khodro": "",
        "Isuzu": "",
        "Iveco": "",
        "JAC": "",
        "Jaguar": "",
        "Jawa": "",
        "Jeep": "",
        "Jetour": "",
        "Jiangmen": "",
        "Jianshe": "",
        "Jinbei": "",
        "JMC": "",
        "KAIYI": "",
        "KamAz": "",
        "Kanuni": "",
        "Karry": "",
        "Kawasaki": "",
        "Kayo": "",
        "Khazar": "",
        "Kia": "",
        "King Long": "",
        "KrAZ": "",
        "KTM": "",
        "Kuba": "",
        "LADA (VAZ)": "",
        "Lamborghini": "",
        "Land Rover": "",
        "Leapmotor": "",
        "Lexus": "",
        "Li Auto": "",
        "Lifan": "",
        "Lincoln": "",
        "Lotus": "",
        "MAN": "",
        "Maple": "",
        "Maserati": "",
        "MAZ": "",
        "Mazda": "",
        "Megelli": "",
        "Mercedes": "",
        "Mercedes-Maybach": "",
        "Mercury": "",
        "MG": "",
        "Mini": "",
        "Minsk": "",
        "Mitsubishi": "",
        "Mondial": "",
        "Moskvich": "",
        "Moto Guzzi": "",
        "Muravey": "",
        "MV Agusta": "",
        "Nama": "",
        "Nissan": "",
        "NIU": "",
        "Opel": "",
        "PAZ": "",
        "Peugeot": "",
        "Polad": "",
        "Polestar": "",
        "Pontiac": "",
        "Porsche": "",
        "Radar": "",
        "RAF": "",
        "Ravon": "",
        "Renault": "",
        "RKS": "",
        "Rolls-Royce": "",
        "Rover": "",
        "Royal Enfield": "",
        "Saipa": "",
        "Saturn": "",
        "Scania": "",
        "Scion": "",
        "SEAT": "",
        "Shacman": "",
        "Shaolin": "",
        "Skoda": "",
        "Skywell": "",
        "Smart": "",
        "Soueast": "",
        "Ssang Yong": "",
        "Subaru": "",
        "Suzuki": "",
        "SYM": "",
        "Tesla": "",
        "Tofas": "",
        "Toyota": "",
        "Tufan": "",
        "UAZ": "",
        "Ural": "",
        "Vespa": "",
        "VGV": "",
        "Volkswagen": "",
        "Volta": "",
        "Volvo": "",
        "Vosxod": "",
        "Voyah": "",
        "XCMG": "",
        "Xinling": "",
        "XPeng": "",
        "Yamaha": "",
        "Yutong": "",
        "ZAZ": "",
        "Zeekr": "",
        "ZIL": "",
        "Zongshen": "",
        "Zontes": "",
        "ZX Auto": ""
}


var firstCategory = document.getElementById("firstCategory")
var secondCategory = document.getElementById("secondCategory")
var thirdCategory = document.getElementById("thirdCategory")


document.addEventListener("DOMContentLoaded", () => {
    AddCategories();
})
function AddCategories() {
    let selectTag = document.getElementById("TypeSelect");
    let optAll = document.createElement("option")
    optAll.textContent = "All"
    selectTag.appendChild(optAll)
    for (const item of Object.keys(AllCatogories)) {
        let option = document.createElement("option");
        option.textContent = item[0].toUpperCase() + item.slice(1);
        selectTag.appendChild(option);
    }

    selectTag.addEventListener("change", (e) => {
        firstCategory.innerHTML = ''
        secondCategory.innerHTML = ''
        let sel = addNewCategory(AllCatogories[e.target.value.toLowerCase()])
        firstCategory.appendChild(sel);
        sel.addEventListener("change", (event) => {
            if (event.target.value.toLowerCase() == "cars") {
                thirdCategory.innerHTML = ''
                let sl = addNewCategory(Object.keys(cars))
                thirdCategory.appendChild(sl)
            }
            secondCategory.innerHTML = ''
            let selSecond = addNewCategory(subAllCategories[event.target.value.toLowerCase()])
            secondCategory.appendChild(selSecond)
        })
    })
}

function addNewCategory(items) {
    let sel = document.createElement("select")
    sel.classList.add("form-select")
    if (!Array.isArray(items)) return
    let optAll = document.createElement("option")
    optAll.textContent = "All"
    sel.appendChild(optAll)

    for (let it of items) {
        let opt = document.createElement("option")
        opt.textContent = it[0].toUpperCase() + it.slice(1)
        sel.appendChild(opt)
    }

    return sel;
}