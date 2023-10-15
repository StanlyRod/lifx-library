using LifxLibrary;
using System;
using System.IO;


string tokenKey = Environment.GetEnvironmentVariable("LIFXKEY");

LifxBulbs bed = new(tokenKey, "Bedroom");
//await bed.MultiUseAsync(brightness:100, duration:10, color:"white");


LightsSearcher ma = new LightsSearcher(tokenKey);
try
{
    var lightquantity = await ma.CountDevices();
    foreach (var light in lightquantity)
    {
        Console.WriteLine(light);
    }

    var name = await ma.GetLightsNames();
   

    BulbState bulb = await ma.ShowLightState(name[1]);

  

    Console.WriteLine(bulb.power);
    

    /*var labelnames = await ma.GetLightsNames();
    Console.WriteLine(labelnames[0]);

    foreach (var labelname in labelnames)
    {
        Console.WriteLine(labelname);
    }*/
}
catch(Exception e)
{
    Console.WriteLine(e);
}




// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
