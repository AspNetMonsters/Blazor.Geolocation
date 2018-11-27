declare var DotNet: any;

class coordinate {
    public latitude: number = 0;
    public longitude: number = 0;
    public accuracy: number = 0;
}


let assemblyName = "AspNetMonsters.Blazor.Geolocation";
let namespace = "AspNetMonsters.Blazor.Geolocation";
let type = "LocationService";
async function dispatchResponse(id: string, location: coordinate) {
    await DotNet.invokeMethodAsync(namespace, 'ReceiveResponse', id, location.latitude, location.longitude, location.accuracy);
}

async function dispatchWatchResponse(id: string, location: coordinate) {
    await DotNet.invokeMethodAsync(namespace, 'ReceiveWatchResponse', id, location.latitude, location.longitude, location.accuracy);
}

window['AspNetMonsters'] = window['AspNetMonsters'] || {};
window['AspNetMonsters']['Blazor'] = window['AspNetMonsters']['Blazor'] || {};
window['AspNetMonsters']['Blazor']['Geolocation'] = window['AspNetMonsters']['Blazor']['Geolocation'] || {};

window['AspNetMonsters']['Blazor']['Geolocation']['GetLocation'] = (requestId) => {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position) => {
            dispatchResponse(requestId, position.coords);
        });
    }
    else {
        return "No location finding";
    }
};

window['AspNetMonsters']['Blazor']['Geolocation']['WatchLocation'] = (requestId) => {
    if (navigator.geolocation) {
        navigator.geolocation.watchPosition((position) => {
            dispatchWatchResponse(requestId, position.coords);
        });
    }
    else {
        return "No location watching";
    }
};