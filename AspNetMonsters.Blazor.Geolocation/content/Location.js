"use strict";
var registerFunction = Blazor.registerFunction;
var platform = Blazor.platform;
var coordinate = /** @class */ (function () {
    function coordinate() {
        this.latitude = 0;
        this.longitude = 0;
        this.accuracy = 0;
    }
    return coordinate;
}());
registerFunction('GetLocation', function (requestId) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            dispatchResponse(requestId, position.coords);
        });
    }
    else {
        return "No location finding";
    }
});
registerFunction('WatchLocation', function (requestId) {
    if (navigator.geolocation) {
        navigator.geolocation.watchPosition(function (position) {
            dispatchWatchResponse(requestId, position.coords);
        });
    }
    else {
        return "No location watching";
    }
});
var assemblyName = "AspNetMonsters.Blazor.Geolocation";
var namespace = "AspNetMonsters.Blazor.Geolocation";
var type = "LocationService";
function dispatchResponse(id, location) {
    var receiveResponseMethod = platform.findMethod(assemblyName, namespace, type, "ReceiveResponse");
    platform.callMethod(receiveResponseMethod, null, [
        platform.toDotNetString(id),
        platform.toDotNetString(location.latitude.toString()),
        platform.toDotNetString(location.longitude.toString()),
        platform.toDotNetString(location.accuracy.toString()),
    ]);
}
function dispatchWatchResponse(id, location) {
    var receiveResponseMethod = platform.findMethod(assemblyName, namespace, type, "ReceiveWatchResponse");
    platform.callMethod(receiveResponseMethod, null, [
        platform.toDotNetString(id),
        platform.toDotNetString(location.latitude.toString()),
        platform.toDotNetString(location.longitude.toString()),
        platform.toDotNetString(location.accuracy.toString()),
    ]);
}
//# sourceMappingURL=location.js.map