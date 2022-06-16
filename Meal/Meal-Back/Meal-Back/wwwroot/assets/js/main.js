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

let d = document.querySelectorAll(".d");
let fg = document.querySelectorAll(".fg");

d.forEach(x => {
    x.addEventListener("click", function () {
        fg.forEach(b => {
            b.classList.add("d-none");
            if (x.getAttribute("data-target") == "all") {
                b.classList.remove("d-none");
            }
            else if (x.getAttribute("data-target") == b.getAttribute("data-id")) {
                b.classList.remove("d-none");
            }
        })
    })
})