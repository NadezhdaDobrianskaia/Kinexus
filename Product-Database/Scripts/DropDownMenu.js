var timeout = 100;
var closetimer = 0;
var ddmenuitem = 0;

// open drop down menu
function mopen(id) {
    // cancel close timer
    mcancelclosetime();

    // close old layer
    if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';

    // get new layer and show it
    ddmenuitem = document.getElementById(id);
    ddmenuitem.style.visibility = 'visible';

}

// close ddmenu
function mclose() {
    if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';
}

// close timer
function mclosetime() {
    closetimer = window.setTimeout(mclose, timeout);
}

// cancel close timer
function mcancelclosetime() {
    if (closetimer) {
        window.clearTimeout(closetimer);
        closetimer = null;
    }
}

// close menu when click-out
document.onclick = mclose; 