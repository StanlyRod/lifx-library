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

### Color Methods
The `PutColor` methods can receive a series of string values as parameter to define the color, brightness, saturation, and other attributes.

```csharp
LifxBulbs bedroom = new LifxBulbs("tokenKey", "Bedroom");

//hexadecimal
bedroom.PutColor("#0000FF"); //blue color

//RGB
bedroom.PutColor("rgb:255,0,0"); //red color

//plain text
bedroom.PutColor("white"); //white color

//async
await bedroom.PutColorAsync("hue:120 saturation:1.0 brightness:0.5"); //Deep green 50% brightness
```