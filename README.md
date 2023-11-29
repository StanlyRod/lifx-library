## Lifx-Library

C# .NET Library to control the Lifx smart LED bulbs over the cloud


## Use
### Toggle Methods
Create an instance from the `LifxBulbs` class by providing the TOKENAPIKEY and the label name of the LED bulb as parameters.

```csharp
using LifxLibrary;
LifxBulbs bedroom = new LifxBulbs("tokenKey", "Bedroom");

//synchronous
bedroom.LightToggle(3); //toggle with 3 seconds duration
//async
await bedroom.LightToggleAsync(3); //async toggle with 3 seconds duration
```
### NOTE
To use the `SweepToggle` methods is not mandatory to provide a label name in the class constructor.
The `SweepToggle` methods will perform a general toggle across the connected devices.

```csharp
LifxBulbs devices = new LifxBulbs("tokenKey");
//synchronous
devices.SweepToggle();
//async
await devices.SweepToggleAsync();

```

### Brightness Methods
The `PutBrightness` methods takes an integer parameter ranging from `0` to `100` to set the intensity of the brightness.

```csharp
LifxBulbs bedroom = new LifxBulbs("tokenKey", "Bedroom");

//synchronous
bedroom.PutBrightness(50); //set the brightness at 50%
//async
await PutBrightnessAsync(50);
```