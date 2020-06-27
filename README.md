# AspNetMonsters.Blazor.Geolocation
This package provides Blazor applications with access to the browser's [Geolocation API](https://developer.mozilla.org/en-US/docs/Web/API/Geolocation)

[![Build Status](https://dev.azure.com/aspnetmonsters/Blazor%20Geolocation/_apis/build/status/Blazor%20Geolocation-ASP.NET%20Core%20(.NET%20Framework)-CI)](https://dev.azure.com/aspnetmonsters/Blazor%20Geolocation/_build/latest?definitionId=4)

## Usage
1) In your Blazor app, add the `AspNetMonsters.Blazor.Geolocation` [NuGet package](https://www.nuget.org/packages/AspNetMonsters.Blazor.Geolocation/)

    ```
    Install-Package AspNetMonsters.Blazor.Geolocation -IncludePrerelease
    ```

1) In your Blazor app's `Startup.cs`, register the 'LocationService'.

    ```
    public void ConfigureServices(IServiceCollection services)
    {
        ...
        services.AddScoped<LocationService>();
        ...
    }
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
      Location location = null;

      protected async Task GetLocation()
      {
          location = await LocationService.GetLocationAsync();
          this.StateHasChanged();
      }

      protected override async Task OnAfterRenderAsync(bool first)
      {
          if (first)
          {
              base.OnAfterRender(first);
              await GetLocation();
          }
      }
    }
    ```

Success!
![image](https://user-images.githubusercontent.com/2531875/37178457-c86888a0-22df-11e8-8667-d6f7eba80691.png)
