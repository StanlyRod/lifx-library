using LifxLibrary;
using System;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using static System.Reflection.Metadata.BlobBuilder;


try
{
    string tokenKey = Environment.GetEnvironmentVariable("LIFXKEY");

    LifxBulbs bulb = new LifxBulbs(tokenKey, "group:Room");

    LifxBulbs bulbs = new LifxBulbs(tokenKey, "label:Bedroom");




    //LightGroup bulbs = new LightGroup(tokenKey, "Room");

    //await bulb.SweepToggleAsync(0.9);

    //await bulb.LightToggleAsync(0.5);

    //await bulb.PutBrightnessAsync(100);

    //await bulb.PutPowerAsync("on", 3);

    //await bulb.PutColorAsync("white");

    //await bulb.MultiUseAsync(power: "on", color: "white", brightness: 100, duration: 5, fast: false);

    /*;

    LightSearcher.SetTokenKey(tokenKey);

    var allUpDevices = await LightSearcher.ShowConnectedDevicesAsync();

    foreach (var device in allUpDevices)
    {
        Console.WriteLine(device.ToString());
    }


    var names = await LightSearcher.GetNamesAsync();
    foreach (var name in names)
    {
        Console.WriteLine(name);
    }*/

    /*LightSearcher.SetTokenKey(tokenKey);

    BulbState bedroom = await LightSearcher.ShowStateAsync("Bedroom");
    Console.WriteLine(bedroom.Id);
    Console.WriteLine(bedroom.UUID);
    Console.WriteLine(bedroom.Label);
    Console.WriteLine(bedroom.Connected);
    Console.WriteLine(bedroom.Power);
    Console.WriteLine(bedroom.Brightness);
    Console.WriteLine(bedroom.Hue);
    Console.WriteLine(bedroom.Saturation);*/

    //await bulb.BreatheEffectAsync("pink", period: 3, cycles: 40, peak: 0.8);

    //await bulb.PulseEffectAsync("blue", period: 3, cycles:40);

    //await bulbs.EffectsOffAsync("group:Room");


}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

