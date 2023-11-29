using LifxLibrary;
using System;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Text.RegularExpressions;





try
{
    string tokenKey = Environment.GetEnvironmentVariable("LIFXKEY");

    LifxBulbs bulb = new LifxBulbs(tokenKey, "Bedroom");
<<<<<<< HEAD

    //LightSearcher.SetTokenKey(tokenKey);
=======
    //LightSearcher light = new LightSearcher(tokenKey);
>>>>>>> a1589ce4e0ef4815b347548c668b637c199521ed

    //bulb.LightToggle();
    //await bulb.LightToggleAsync(5);

    //bulb.SweepToggle(1);

    //await bulb.SweepToggleAsync();

    //bulb.PutBrightness(50);
    //await bulb.PutBrightnessAsync(10);

    //bulb.PutColor("blue");
    //await bulb.PutColorAsync("green");

    //bulb.MultiUse(power: "on", color:"red", brightness:70, duration:10, fast:true);
    //await bulb.MultiUseAsync(power:"off", color:"yellow", brightness:100, duration:50, fast:false);

    /*var devices = await LightSearcher.ShowConnectedDevicesAsync();
    foreach (var device in devices)
    {
        Console.WriteLine(device);
    }*/


    /*var names = await LightSearcher.GetNamesAsync();
    foreach (var name in names)
    {
        Console.WriteLine(name);
    }*/

<<<<<<< HEAD
    /*LightSearcher.SetTokenKey(tokenKey);
=======
    LightSearcher.SetTokenKey(tokenKey);

    BulbState bedroom = await LightSearcher.ShowLightStateAsync("Bedroom");

    Console.WriteLine(bedroom.Power);
    Console.WriteLine(bedroom.Brightness);
>>>>>>> a1589ce4e0ef4815b347548c668b637c199521ed

    BulbState bedroom = await LightSearcher.ShowStateAsync("Bedroom");

    Console.WriteLine(bedroom.Power);
    Console.WriteLine(bedroom.Brightness);*/

}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}

