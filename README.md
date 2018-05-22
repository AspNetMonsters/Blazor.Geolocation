# AspNetMonsters.Blazor.Geolocation
This package provides Blazor applications with access to the browser's [Geolocation API](https://developer.mozilla.org/en-US/docs/Web/API/Geolocation)

## Usage
1) In your Blazor app, add the `AspNetMonsters.Blazor.Geolocation` [NuGet package](https://www.nuget.org/packages/AspNetMonsters.Blazor.Geolocation/)

    ```
    Install-Package AspNetMonsters.Blazor.Geolocation -IncludePrerelease
    ```

1) In your Blazor app's `Program.cs`, register the 'LocationService'.

    ```
    var serviceProvider = new BrowserServiceProvider(configure =>
    {
        configure.AddSingleton<LocationService>();
    });
    ```

1) Now you can inject the LocationService into any Blazor page and use it like this:

    ```
    @using AspNetMonsters.Blazor.Geolocation
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
![image](https://user-images.githubusercontent.com/2531875/37178457-c86888a0-22df-11e8-8667-d6f7eba80691.png)
