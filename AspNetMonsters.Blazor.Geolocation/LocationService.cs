using Microsoft.AspNetCore.Blazor.Browser.Interop;
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
            RegisteredFunction.Invoke<object>("GetLocation", requestId);
            return await tcs.Task;
        }

        public void WatchLocation(Action<Location> watchCallback)
        {
            var requestId = Guid.NewGuid();
            _watches.Add(requestId, watchCallback);
            RegisteredFunction.Invoke<object>("WatchLocation", requestId);
        }

        private static void ReceiveResponse(
            string id,
            string latitude,
            string longitude,
            string accuracy)
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

        private static void ReceiveWatchResponse(
            string id,
            string latitude,
            string longitude,
            string accuracy)
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
