using LifxLibrary;
using System;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Text.RegularExpressions;

string tokenKey = Environment.GetEnvironmentVariable("LIFXKEY");






try
{
    LifxBulbs bulb = new(tokenKey);
    bulb.SweepToggle();
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}




// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
