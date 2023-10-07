using LifxLibrary;

try
{ 

LifxBulbs bed = new("", "Bedroom");

    bed.PutColor("#F2F3F4");
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
