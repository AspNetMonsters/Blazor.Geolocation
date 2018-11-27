using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetMonsters.Blazor.Geolocation
{
    public class LocationService
    {
        static IDictionary<Guid, TaskCompletionSource<Location>> _pendingRequests = new Dictionary<Guid, TaskCompletionSource<Location>>();
        static IDictionary<Guid, Action<Location>> _watches = new Dictionary<Guid, Action<Location>>();

        public async Task<Location> GetLocationAsync()
        {
            var tcs = new TaskCompletionSource<Location>();
            var requestId = Guid.NewGuid();

            _pendingRequests.Add(requestId, tcs);
            var result = await JSRuntime.Current.InvokeAsync<Location>("AspNetMonsters.Blazor.Geolocation.GetLocation", requestId);
            
            return await tcs.Task;
        }

        public async Task WatchLocation(Action<Location> watchCallback)
        {
            var requestId = Guid.NewGuid();
            _watches.Add(requestId, watchCallback);
            await JSRuntime.Current.InvokeAsync<Location>("AspNetMonsters.Blazor.Geolocation.WatchLocation", requestId);
        }

        [JSInvokable]
        public static void ReceiveResponse(
            string id,
            double latitude,
            double longitude,
            double accuracy)
        {
            TaskCompletionSource<Location> pendingTask;
            var idVal = Guid.Parse(id);
            pendingTask = _pendingRequests.First(x => x.Key == idVal).Value;
            pendingTask.SetResult(new Location
            {
                Latitude = Convert.ToDecimal(latitude),
                Longitude = Convert.ToDecimal(longitude),
                Accuracy = Convert.ToDecimal(accuracy)
            });
        }

        [JSInvokable]
        public static void ReceiveWatchResponse(
            string id,
            double latitude,
            double longitude,
            double accuracy)
        {
            Action<Location> callback;
            var idVal = Guid.Parse(id);
            callback = _watches[idVal];
            callback(new Location
            {
                Latitude = Convert.ToDecimal(latitude),
                Longitude = Convert.ToDecimal(longitude),
                Accuracy = Convert.ToDecimal(accuracy)
            });
        }
    }
}
