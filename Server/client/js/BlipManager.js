API.onServerEventTrigger.connect(function (name, args) {
    if (name === "create_blip") {
        
        var blip = API.createBlip(args[0]);
        API.setBlipSprite(blip, 40);
    }
});