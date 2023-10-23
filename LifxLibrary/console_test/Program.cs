using LifxLibrary;
using System;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Text.RegularExpressions;

string tokenKey = Environment.GetEnvironmentVariable("LIFXKEY");


try
{
    LifxBulbs bulb = new LifxBulbs(tokenKey, "Bedroom");
    LightSearcher light = new LightSearcher(tokenKey);

    //bulb.LightToggle();
    //await bulb.LightToggleAsync(5);

    //bulb.PutBrightness(50);
    //await bulb.PutBrightnessAsync(10);

    //bulb.PutColor("blue");
    //await bulb.PutColorAsync("green");

    //bulb.MultiUse(power: "on", color:"red", brightness:70, duration:10, fast:true);
    //await bulb.MultiUseAsync(power:"off", color:"yellow", brightness:100, duration:50, fast:false);

    /*var devices = await light.ShowConnectedDevicesAsync();
    foreach (var device in devices)
    {
        Console.WriteLine(device);
    }*/


    /*var names = await light.GetNamesAsync();
    foreach (var name in names)
    {
        Console.WriteLine(name);
    }*/

    /*BulbState bedroom = await light.ShowLightStateAsync("Bedroom");
    Console.WriteLine(bedroom.Power);*/


        


}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}




// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
