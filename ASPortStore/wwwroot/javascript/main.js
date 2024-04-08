const mainAside = document.querySelector("#main-aside"),
    navigationMenuToggleButton = document.querySelector("#navigation-menu-toggle-button");

navigationMenuToggleButton.addEventListener("click", _e => {
    mainAside.toggleAttribute("data-is-open");
    document.addEventListener("click", CloseMainAside);
});

function CloseMainAside(e) {
    if (e.target.closest("#main-aside") != null || e.target.closest("#navigation-menu-toggle-button") != null) { return; }
    e.preventDefault();

    mainAside.removeAttribute("data-is-open");
    document.removeEventListener("click", CloseMainAside);
}