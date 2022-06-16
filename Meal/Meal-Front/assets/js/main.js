window.onscroll = () => {
    let nav = document.querySelector("nav");
    if (window.pageYOffset >= 100) {
        nav.classList.add("bg-nav");
    }
    else {
        nav.classList.remove("bg-nav");
    }
}
// let icon = document.querySelector(".hamburger-icon");
// let aside = document.querySelector("aside");

// icon.addEventListener("click", function () {
//     aside.classList.toggle("active");
//     icon.classList.toggle("active");
// })