## Lifx-Library

C# .NET Standard Library to control the Lifx smart LED bulbs over the cloud


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

For a more detailed guide on defining colors, please visit the official Lifx API documentation at https://api.developer.lifx.com/reference/colors.

### MultiUse Methods
The MultiUse methods accept a set of optional parameters to carry out diverse actions on the LED bulbs, such as changing the `color`, `brightness`, `power`, `duration time`, and activating `fast mode`.

### NOTE
The fast mode execute the query fast, without initial state checks and wait for no results.

```csharp
LifxBulbs bedroom = new LifxBulbs("tokenKey", "Bedroom");

//synchronous
bedroom.MultiUse("on", "blue", 50, 6, true); //power on, color blue, 50% brightness, 6 seconds duration with fast mode activated

//other ways to use it
bedroom.MultiUse(power:"on", color:"blue", brightness:100, duration:6, fast:true);

//duration time is 0 by default and fast mode is false by default
bedroom.MultiUse(power:"on", color:"white", brightness:80); 

//async
await bedroom.MultiUseAsync("off");

await bedroom.MultiUseAsync("on", "orange");
```