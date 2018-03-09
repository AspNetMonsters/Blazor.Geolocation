declare var Blazor: any;

var registerFunction: (identifier: string, implementation: Function) => void = Blazor.registerFunction;
var platform = Blazor.platform;

class coordinate {
    public latitude: number = 0;
    public longitude: number = 0;
    public accuracy: number = 0;
}

registerFunction('GetLocation', (requestId) => {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position) => {
            dispatchResponse(requestId, position.coords);
        });
    }
    else {
        return "No location finding";
    }
});

registerFunction('WatchLocation', (requestId) => {
    if (navigator.geolocation) {
        navigator.geolocation.watchPosition((position) => {
            dispatchWatchResponse(requestId, position.coords);
        });
    }
    else {
        return "No location watching";
    }
});

let assemblyName = "AspNetMonsters.Blazor.Geolocation";
let namespace = "AspNetMonsters.Blazor.Geolocation";
let type = "LocationService";
function dispatchResponse(id: string, location: coordinate) {
    let receiveResponseMethod = platform.findMethod(
        assemblyName,
        namespace,
        type,
        "ReceiveResponse"
    );


    platform.callMethod(receiveResponseMethod, null, [
        platform.toDotNetString(id),
        platform.toDotNetString(location.latitude.toString()),
        platform.toDotNetString(location.longitude.toString()),
        platform.toDotNetString(location.accuracy.toString()),
    ]);
}

function dispatchWatchResponse(id: string, location: coordinate) {
    let receiveResponseMethod = platform.findMethod(
        assemblyName,
        namespace,
        type,
        "ReceiveWatchResponse"
    );


    platform.callMethod(receiveResponseMethod, null, [
        platform.toDotNetString(id),
        platform.toDotNetString(location.latitude.toString()),
        platform.toDotNetString(location.longitude.toString()),
        platform.toDotNetString(location.accuracy.toString()),
    ]);
}