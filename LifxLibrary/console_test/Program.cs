using LifxLibrary;
using System.IO;

string? tokenKey = Environment.GetEnvironmentVariable("LIFXKEY");

LifxBulbs bed = new(tokenKey, "Bedroom");
await bed.MultiUseAsync(brightness:100, duration:10, color:"white");

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
