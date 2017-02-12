
API.onKeyDown.connect(function (sender, args) {

    if (args.KeyCode == Keys.F1) {
        API.triggerServerEvent("onKeyDown", 0);
    }
    else if (args.KeyCode == Keys.F2) {

        if (API.isCursorShown()) {
            API.showCursor(false)
        }
        else API.showCursor(true);
    }
    else if (args.KeyCode == Keys.N) {  
        API.triggerServerEvent("onKeyDown", 2);
    }
    else if (args.KeyCode == Keys.Q) {
        API.triggerServerEvent("onKeyDown", 3);
    }
    else if (args.KeyCode == Keys.Y) {
        API.triggerServerEvent("onKeyDown", 4);
    }
    else if (args.KeyCode == Keys.I) {
        API.triggerServerEvent("onKeyDown", 5);
    }
    else if (args.KeyCode == Keys.K) {
        API.triggerServerEvent("onKeyDown", 6);
    }
    else if (args.KeyCode == Keys.L) {
        API.triggerServerEvent("onKeyDown", 7);
    }
    else if (args.KeyCode == Keys.F) {
        if (resource.hud.drawAnimationHUD) {
            API.triggerServerEvent("onKeyDown", 8);
        }
    }
});