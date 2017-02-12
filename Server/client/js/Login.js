var loginBrowser = null;
var width = 380;
var height = 550;
var charSelectMenu = null;

charSelectMenu = API.createMenu("SELECT CHARACTER", 0, 0, 6);
charSelectMenu.ResetKey(menuControl.Back);

charSelectMenu.OnItemSelect.connect(function(sender, item, index)
{
    API.triggerServerEvent("account_selected", index + 1);
    API.sendNotification("You have selected your character ~b~" + item.Text + "~w~.");
    charSelectMenu.Visible = false;
});


API.onServerEventTrigger.connect(function(eventName, args)
{
    if (eventName === "account_charlist")
    {
        var characterNum = args[0];

        charSelectMenu.Clear();

        for (var i = 0; i < characterNum; i++)
        {
            var charname = args[i + 1];
            var charItem = API.createMenuItem(charname,
                "You should select which of your characters you wish to use here.");
            charSelectMenu.AddItem(charItem);
        }

        charSelectMenu.Visible = true;
        API.sendNotification("~r~Please select your character.");
    }
});


API.onUpdate.connect(function(sender, args)
{
    API.drawMenu(charSelectMenu);
});
