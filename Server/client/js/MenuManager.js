var menuPool = null;
var draw = false;


API.onResourceStart.connect(function() {
    
});

API.onServerEventTrigger.connect(function (name, args) {

    if (name == "create_menu") {

        var callbackId = args[0];
        var banner = args[1];
        var subtitle = args[2];
        var noExit = args[3];

        var menu = null;
        if (banner == null)
            menu = API.createMenu(subtitle, 0, 0, 6);
        else menu = API.createMenu(banner, subtitle, 0, 0, 6);

        if (noExit) {
            menu.ResetKey(menuControl.Back);
        }

        var items = args[4];
        for (var i = 0; i < items.Count; i++) {
            var listItem = API.createMenuItem(items[i], "");
            menu.AddItem(listItem);
        }

        menu.RefreshIndex();

        menu.OnItemSelect.connect(function (sender, item, index) {
            API.triggerServerEvent("menu_handler_select_item", callbackId, index, items.Count);
            menu.Visible = false;
        });

        API.onUpdate.connect(function () {
            API.drawMenu(menu);
        });

        menu.Visible = true;

    }
});


API.onUpdate.connect(function (sender, args) {

    if(menuPool != null) menuPool.ProcessMenus();
});