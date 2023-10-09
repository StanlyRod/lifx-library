using LifxLibrary;

LifxBulbs bed = new("", "Bedroom");
await bed.MultiUseAsync();

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
