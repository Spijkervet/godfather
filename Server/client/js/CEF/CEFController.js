
class WebBrowser {
    constructor(resourcePath) {
        this.path = resourcePath
        this.open = false
    }

    show(local, opt_path, chatstate = false, cursorstate = true) {
        if (this.open === false) {
            this.open = true

            var resolution = API.getScreenResolution();

            this.browser = API.createCefBrowser(resolution.Width, resolution.Height, local)
            API.waitUntilCefBrowserInit(this.browser)
            API.setCefBrowserPosition(this.browser, 0, 0)

            if (opt_path) {
                API.loadPageCefBrowser(this.browser, opt_path)
            } else {
                API.loadPageCefBrowser(this.browser, this.path)
            }

            API.setCanOpenChat(chatstate)
            API.showCursor(cursorstate)
        }
    }

    destroy() {
        this.open = false
        API.destroyCefBrowser(this.browser)
        API.setCanOpenChat(true)
        API.showCursor(false)
    }

    eval(string) {
        this.browser.eval(string)
    }
}

const CEF = new WebBrowser('')

API.onServerEventTrigger.connect(function (eventName, args) {

    if (eventName == "CEFDestroy") {

        CEF.destroy();
    }
    else if (eventName === "CEFController") {

        CEF.show(false, args[0]);
        API.sendNotification("~r~Press F4 to hide the interface.");
    }
    
    else if (eventName === "CEFController_CharMenu") {

        CEF.show(false, args[0], true, false);
        API.sendNotification("Press F1 to toggle the menu.\nPress F2 to see your mouse.\n~r~Press F4 to hide the interface.");
    }
});

API.onKeyDown.connect(function (sender, args) {

    if (args.KeyCode == Keys.F4) {
        CEF.destroy();
    }
});

API.onResourceStop.connect(function () {
    if (CEF != null) {
        CEF.destroy();
    }
});