using LifxLibrary;
using System;
using System.IO;


string tokenKey = Environment.GetEnvironmentVariable("LIFXKEY");

LifxBulbs bed = new(tokenKey, "Bedroom");




try
{
    LightSearcher li = new LightSearcher(tokenKey);

    var a = await li.ShowConnectedDevicesAsync();

    foreach (var device in a)
    {
        if (device.Equals("Bedroom"))
        {
            BulbState bulb = await li.ShowLightStateAsync(device);
            Console.WriteLine(bulb.Brightness);
        }
    }
   

    /*var la = await ma.GetLightsNamesAsync();
    foreach (var l in la)
    {
        Console.WriteLine(l);
    }*/



   // BulbState bulb = await ma.ShowLightStateAsync("Restroom");
   

 

    //Console.WriteLine(bulb.Power);
    

    
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}




// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
