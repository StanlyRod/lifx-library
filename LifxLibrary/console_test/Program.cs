using LifxLibrary;
using System;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Text.RegularExpressions;

string tokenKey = Environment.GetEnvironmentVariable("LIFXKEY");

LifxBulbs bulb = new(tokenKey);
await bulb.SweepToggleAsync();




try
{
    //bulb.SweepToggle();

    
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}




// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
