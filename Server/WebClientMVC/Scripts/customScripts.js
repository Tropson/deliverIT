var yScroll;
var nav;
var fixed;
var cont;
var cont2;
var shown = false;
function loading() {
    nav = document.getElementById('nav');
    cont = document.getElementById('topCont');
    cont2 = document.getElementById('bigTopCont');
    fixed = document.getElementById('fixednav');
}
function navScroll() {
    yScroll = window.scrollY;
    console.log(yScroll);
    console.log(cont.offsetHeight);
    if (cont != null) {
        if (yScroll >= cont.offsetHeight / 3) {
            nav.style.backgroundColor = "black";
        }
        else {
            nav.style.backgroundColor = "rgba(0,0,0,0.1)";
        }
    }
    else if (cont2 != null) {
        if (yScroll >= cont2.offsetHeight / 3) {
            nav.style.backgroundColor = "black";
        }
        else {
            nav.style.backgroundColor = "rgba(0,0,0,0.1)";
        }
    }
}
function showMenu() {
    if (!shown) {
        document.getElementById("hidden").style.display = "block";
        shown = true;
    }
    else {
        document.getElementById("hidden").style.display = "none";
        shown = false;
    }

}
function goToAbout() {
    window.open("https://www.facebook.com", "_self")
}