var drawVehicleHUD = false;
var drawAnimationHUD = false;
var currentMoney = null;
var res = API.getScreenResolutionMantainRatio();

API.onUpdate.connect(function (sender, args) {

    if (drawAnimationHUD) {
        API.drawText("Press F to stop", res.Width - 30, res.Height - 100, 0.5, 255, 186, 131, 255, 4, 2, false, true, 0);
    }

    if (currentMoney != null) {
        API.drawText("$" + currentMoney, res.Width - 15, 50, 1, 115, 186, 131, 255, 4, 2, false, true, 0);
    }
    
});

API.onServerEventTrigger.connect(function (eventName, args) {

    if (eventName === "update_money_display") {
        currentMoney = Number(args[0]).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
    }
    else if (eventName == "animation_text") {
        drawAnimationHUD = !drawAnimationHUD;
    }
});