using Microsoft.AspNetCore.Blazor.Browser.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMonsters.Blazor.Geolocation
{
    public class LocationService
    {
        static IDictionary<Guid, TaskCompletionSource<Location>> _pendingRequests = new Dictionary<Guid, TaskCompletionSource<Location>>();
        public async Task<Location> GetLocationAsync()
        {
            var tcs = new TaskCompletionSource<Location>();
            var requestId = Guid.NewGuid();

            _pendingRequests.Add(requestId, tcs);
            Console.WriteLine("Requesting location"); //TODO: remove
            RegisteredFunction.Invoke<object>("GetLocation", requestId);
            return await tcs.Task;
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
            Console.WriteLine($"Got location ({latitude}, {longitude}) with accuracy {accuracy}"); //TODO: remove
        }
    }
}
