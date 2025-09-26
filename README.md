# Lifx-Library


![LIFX Logo](https://cdn.shopify.com/s/files/1/0024/9803/5810/t/18/assets/jb-au-20190517-lifx-header-mob.jpg?96248)

C# .NET Standard library to control LIFX smart LED bulbs over the cloud.

This project wraps the official LIFX Cloud API. See the API reference for full parameter semantics.

# Nuget.org
## https://www.nuget.org/packages/LifxLibrary


## What’s new (breaking changes) v2.0.0

- **All synchronous methods were removed.** Use the `Async` counterparts (e.g., `LightToggleAsync`, `PutPowerAsync`, etc.).
- **`LightGroup` class was deleted.** Group operations should be performed via selectors (e.g., `group:Kitchen`, `all`) using the `LifxBulbs` methods.
- **Expanded effects support:** `BreatheEffectAsync`, `PulseEffectAsync`, and `EffectsOffAsync` were added.
- **Improved error handling** with clear exceptions for common HTTP status codes.
  


## Get your API token

Create/retrieve your token at the LIFX Cloud: https://cloud.lifx.com/settings


## Selectors (important)

Most `LifxBulbs` methods require a **selector**. Common selectors:

- A specific bulb by label: `label:Bedroom`
- A group: `group:Kitchen`
- All devices: `all`

> If your label/group contains spaces, URL-encode them (e.g., `label:Living%20Room`).


## Usage


### Selectors
```csharp
using LifxLibrary;

// Using label selector - targets light with label "Bedroom"
LifxBulbs bedroom = new LifxBulbs("YOUR_TOKEN", "label:Bedroom");

// Async toggle with 3 seconds duration
await bedroom.LightToggleAsync(3);

// You can also use other selector types:
// Using ID selector
LifxBulbs specificLight = new LifxBulbs("YOUR_TOKEN", "id:d3b2f2d97452");

// Using group selector
LifxBulbs kitchenLights = new LifxBulbs("YOUR_TOKEN", "group:Kitchen");
```

### Create a controller for one bulb (or a group)
```csharp
// token and selector (use selector syntax like label:Bedroom or group:Kitchen)
var bedroom = new LifxBulbs("YOUR_TOKEN", "label:Bedroom");
```
All examples below assume an instance like the above.


## Toggle

NOTE
It is not obligatory to specify a selector in the class constructor when using the SweepToggle methods. These methods will execute a general toggle operation across all connected devices using the all selector.

```csharp
using LifxLibrary;

//constructor
LifxBulbs bedroom = new LifxBulbs("YOUR_TOKEN", "label:Bedroom");

// Toggle a specific selector with optional duration (0–100s)
await bedroom.LightToggleAsync(duration: 3); // 3 seconds duration

// Toggle all connected devices
LifxBulbs allDevices = new LifxBulbs("YOUR_TOKEN"); // token-only overload

// Async sweep toggle - affects all lights
await allDevices.SweepToggleAsync(); //duration is optional          
```



## Power

```csharp
using LifxLibrary;

//constructor
LifxBulbs bedroom = new LifxBulbs("tokenKey", "label:Bedroom");

// Turn on or off. Optional duration (0–100s) will fade the change.
await bedroom.PutPowerAsync("on", duration: 5); // 5 seconds duration
await bedroom.PutPowerAsync("off");
```



## Brightness
The PutBrightnessAsync method takes an integer parameter ranging from 0 to 100 to set the intensity of the brightness.

```csharp
using LifxLibrary;

//constructor
LifxBulbs bedroom = new LifxBulbs("tokenKey", "label:Bedroom"); 

//The library converts it to 0.0–1.0 for the LIFX API.
await bedroom.PutBrightnessAsync(50); // 50% brightness
```



## Color
The PutColorAsync method can receive a series of string values as parameter to define the color, brightness, saturation, and other attributes.

For a more detailed guide on defining colors, please visit the official Lifx API documentation at https://api.developer.lifx.com/reference/colors.
```csharp
using LifxLibrary;

//constructor
LifxBulbs bedroom = new LifxBulbs("tokenKey", "label:Bedroom");

// Hexadecimal
await bedroom.PutColorAsync("#0000FF"); // blue color

// RGB
await bedroom.PutColorAsync("rgb:255,0,0"); // red color

// Plain text
await bedroom.PutColorAsync("white"); // white color

// HSB with brightness
await bedroom.PutColorAsync("hue:120 saturation:1.0 brightness:0.5"); // Deep green 50% brightness
```



## Multi-use (set several fields at once)
The MultiUseAsync method accepts a set of optional parameters to carry out diverse actions on the LED bulbs, such as changing the color, brightness, power, duration time, and activating fast mode.

```csharp
using LifxLibrary;

LifxBulbs bedroom = new LifxBulbs("tokenKey", "label:Bedroom");

// Power on, color blue, 50% brightness, 6 seconds duration with fast mode activated
await bedroom.MultiUseAsync("on", "blue", 50, 6, true);

// Other ways to use it with named parameters
await bedroom.MultiUseAsync(power:"on", color:"blue", brightness:100, duration:6, fast:true);

// Duration time is 0 by default and fast mode is false by default
await bedroom.MultiUseAsync(power:"on", color:"white", brightness:80); 

// Simple power control
await bedroom.MultiUseAsync("off");

// Power and color
await bedroom.MultiUseAsync("on", "orange");
```



# Effects
The library now includes advanced lighting effects for creating dynamic lighting experiences.
Breathe Effect
Creates a smooth breathing effect that gradually transitions between colors.

## Breathe Effect
```csharp

using LifxLibrary;

LifxBulbs kitchen = new LifxBulbs("tokenKey", "group:kitchen");

// Basic breathing effect with blue color
await kitchen.BreatheEffectAsync("blue");

// Advanced breathing effect with custom parameters
await kitchen.BreatheEffectAsync(
    color: "red",
    from_color: "blue",    // Starting color (optional)
    period: 2.0,           // Seconds per cycle
    cycles: 5,             // Number of cycles
    persist: true,         // Keep final color
    powerOn: true,         // Turn on if needed
    peak: 0.8              // Peak brightness (0.0-1.0)
);
```

## Pulse Effect
Creates a flashing effect between two colors.
```csharp
using LifxLibrary;

LifxBulbs kitchen = new LifxBulbs("tokenKey", "group:kitchen");

// Basic pulse effect
await kitchen.PulseEffectAsync("green");

// Advanced pulse with custom settings
await kitchen.PulseEffectAsync(
    color: "purple",
    from_color: "white",   // Starting color (optional)
    period: 1.5,           // Seconds per pulse
    cycles: 10,            // Number of pulses
    persist: false,        // Return to original color
    power_on: true         // Power on before effect
);
```

## Turn off effect
The EffectsOffAsync method stops any running effects on the specified lights. This method requires you to specify a selector to target which lights should have their effects stopped.

```csharp
// Stop effects on specific bulb by label, keep power state
await bedroom.EffectsOffAsync("label:Bedroom");

// Stop effects and turn off the bulb
await bedroom.EffectsOffAsync("label:Bedroom", powerOff: true);

// You can also use other selectors:
// Stop effects on all lights in a group
await kitchen.EffectsOffAsync("group:kitchen");

// Stop effects on all lights
await kitchen.EffectsOffAsync("all");
```



# LightSearcher class


The LightSearcher class is a static class that contains static async methods that help discover the LED bulbs and retrieve their properties such as LED bulb name, power status, connection status, brightness level, saturation level and more.

## SetTokenKey()
Before using any LightSearcher methods, you must set the API token key.

```csharp
using LifxLibrary;

// Set the token key (required before using other methods)
LightSearcher.SetTokenKey("your_token_key_here");
```

## ShowConnectedDevicesAsync()
The ShowConnectedDevicesAsync method returns a list of string object with the label names of all connected devices.

```csharp
using LifxLibrary;

// Set the token key
LightSearcher.SetTokenKey("tokenkey");

var devices = await LightSearcher.ShowConnectedDevicesAsync();

foreach(var device in devices)
{
    Console.WriteLine(device);
}
```

## GetNamesAsync()
The GetNamesAsync method returns a list of string object with the label names of all devices linked to your account, regardless of their connection status or power status.

```csharp
using LifxLibrary;

// Set the token key
LightSearcher.SetTokenKey("tokenkey");

var devices = await LightSearcher.GetNamesAsync();

foreach(var device in devices)
{
    Console.WriteLine(device);
}
```

## ShowStateAsync()
The ShowStateAsync method returns a BulbState object with the power status, connection status, LED label name, brightness level, saturation level, UUID, ID, and HUE level. This method requires a specific label name as it retrieves state for individual bulbs.

```csharp
using LifxLibrary;

// Set the token key
LightSearcher.SetTokenKey("tokenkey");

// Get state for a specific bulb by label name
BulbState bedroom = await LightSearcher.ShowStateAsync("Bedroom");

Console.WriteLine(bedroom.Power);       // "off" or "on"
Console.WriteLine(bedroom.Connected);   // true or false
Console.WriteLine(bedroom.Brightness);  // brightness level (0.0-1.0)
Console.WriteLine(bedroom.Saturation);  // saturation level (0.0-1.0)
Console.WriteLine(bedroom.Hue);         // hue level (0.0-360.0)
Console.WriteLine(bedroom.Id);          // bulb ID
Console.WriteLine(bedroom.UUID);        // bulb UUID
Console.WriteLine(bedroom.Label);       // bulb label name
```

## Error handling

The library includes comprehensive error handling for common HTTP status codes:

- 401: Invalid or missing token key
- 404: Label name missing or doesn't match bulb/group name
- 400: Invalid request
- 422: Missing or malformed arguments
- 429: Too many requests (rate limiting)
- 403: Bad OAuth scope
- 500/502/503/523: Server errors

```csharp
try
{
    await bedroom.PutColorAsync("orange");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Request failed: {ex.Message}");
}
```



## Notes & tips

- All methods are now async only - synchronous versions have been removed
- The fast mode in MultiUseAsync executes queries without initial state checks for faster performance
- Duration parameters are validated to be **0–100** seconds.
- Brightness setters take **0–100 %**, internally converted to 0.0–1.0.
- Always pass a **selector** (`label:Name`, `group:Name`, or `all`) when constructing `LifxBulbs` with a target; use the token-only overload for global operations.
- If a label or group contains spaces, **URL-encode** them (e.g., `label:Living%20Room`).  


## Removed: `LightGroup`

The previous `LightGroup` class has been removed. Use selectors (e.g., `group:Kitchen`) with `LifxBulbs` methods to control groups.


---
Please report any bugs to stanlywgr@outlook.com
### License

This project is licensed under the [MIT License](LICENSE).
