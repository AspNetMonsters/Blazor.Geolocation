# Blazor.Geolocation
This package provides Blazor applications with access to the browser's [Geolocation API](https://developer.mozilla.org/en-US/docs/Web/API/Geolocation)

## Usage
In your Blazor app, add the `AspNetMonsters.Blazor.Geolocation` package

```
Install-Package AspNetMonsters.Blazor.Geolocation
```

In your app's `index.html`, reference `https://blazor.blob.core.windows.net/bjs/Location.js`. Place this reference _after_ the `blazor-boot` script. 

```
<script type="blazor-boot"></script>
<script src="https://blazor.blob.core.windows.net/bjs/Location.js"> </script>
```

*Note:* Eventually, this JS file will be embedded in the nuget package but that feature of Blazor isn't released yet. For now, we are hosting the file in Azure blob storage to make it easy for you to reference the file.

In your Blazor app's `Program.cs`, register the 'LocationService'.

```
var serviceProvider = new BrowserServiceProvider(configure =>
{
    configure.AddSingleton<LocationService>();
});
```

Now you can inject the LocationService into any Blazor page and use it like this:

```
@inject LocationService  LocationService
<h3>You are here</h3>
<div>
Lat: @location?.Latitude <br/>
Long: @location?.Longitude <br />
Accuracy: @location?.Accuracy <br />
</div>

@functions
{
    Location location;

    protected override async Task OnInitAsync()
    {
        location = await LocationService.GetLocationAsync();
    }
}
```

Success!
