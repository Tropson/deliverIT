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
    console.log(cont);
    console.log(cont2);
    if (cont!=null) {
        if (yScroll >= cont.offsetHeight / 3) {
            nav.style.backgroundColor = "black";
        }
        else {
            nav.style.backgroundColor = "rgba(0,0,0,0.25)";
        }
    }
    else if (cont2 != null) {
        if (yScroll >= cont2.offsetHeight / 3) {
            nav.style.backgroundColor = "black";
        }
        else {
            nav.style.backgroundColor = "rgba(0,0,0,0.25)";
        }
    }
}
function showMenu() {
    if (!shown) {
        document.getElementById("hidden").style.zIndex = "7";
        document.getElementById("hidden").style.left = "0px";
        console.log(document.getElementById("hidden").style.left);
        shown = true;
    }
    else {
        document.getElementById("hidden").style.zIndex = "-100";
        document.getElementById("hidden").style.left = "-200px";
        console.log(document.getElementById("hidden").style.left);
        shown = false;
    }

}