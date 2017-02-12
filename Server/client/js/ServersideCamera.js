API.onServerEventTrigger.connect(function (name, args) {
    if (name == "createCamera") {
        var pos = args[0];
        var target = args[1];

        var newCam = API.createCamera(pos, new Vector3());
        API.pointCameraAtPosition(newCam, target);
        API.setActiveCamera(newCam);
    }
    else if (name == "interpolateCamera")
    {
        var newCam = API.createCamera(args[1], args[3]);
        var newCam2 = API.createCamera(args[2], args[4]);

        API.interpolateCameras(newCam, newCam2, args[0], true, true);
    }
    else if (name == "destroyCamera") {
        API.setActiveCamera(null);
    }
});