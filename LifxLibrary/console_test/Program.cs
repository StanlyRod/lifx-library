using LifxLibrary;
using System;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using static System.Reflection.Metadata.BlobBuilder;


try
{
    string tokenKey = Environment.GetEnvironmentVariable("LIFXKEY");

    LifxBulbs bulb = new LifxBulbs(tokenKey, "Bedroom");

    //LightGroup bulbs = new LightGroup(tokenKey, "Room");

    //await bulb.SweepToggleAsync();

    //await bulb.LightToggleAsync(1);

    //await bulb.PutBrightnessAsync(10);

    //await bulb.PutPowerAsync("onn", 3);

    await bulb.PutColorAsync("white");

    //await bulb.MultiUseAsync(power: "on", color: "white", brightness: 100, duration: 10, fast: false);

    //await bulbs.PutBrightnessAsync(30);

    //await bulbs.PutColorAsync("white");

    //await bulbs.MultiUseAsync("on", "white");

    //await bulbs.MultiUseAsync("on");

    //LightSearcher.SetTokenKey(tokenKey);

    /*LightSearcher.SetTokenKey(tokenKey);

    var devices = await LightSearcher.ShowConnectedDevicesAsync();

    foreach (var device in devices)
    {
        Console.WriteLine(device);
    }


    var names = await LightSearcher.GetNamesAsync();
    foreach (var name in names)
    {
        Console.WriteLine(name);
    }*/

    /*LightSearcher.SetTokenKey(tokenKey);

    BulbState bedroom = await LightSearcher.ShowStateAsync("Bedroom");

    Console.WriteLine(bedroom.Power);
    Console.WriteLine(bedroom.Brightness);
    Console.WriteLine(bedroom.Hue);
    Console.WriteLine(bedroom.Saturation);
    Console.WriteLine(bedroom.Connected);*/

    //await bulb.BreatheEffectAsync("label:Bedroom", "orange", period: 3, cycles: 5, peak: 0.8);

    //await bulb.PulseEffectAsync("label:Bedroom", "orange", period: 3, cycles:3);


}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}

