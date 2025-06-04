using SunSet;
using System.Reflection;

Console.Title = "SunSet Bot";

if (!File.Exists("appsettings.json"))
{
    Console.WriteLine("appsettings.json already exists, skipping copy.");
    using var stream = Assembly.GetExecutingAssembly()
        .GetManifestResourceStream("SunSet.Resources.appsettings.json");
    using var fs = new FileStream("appsettings.json", FileMode.Create, FileAccess.Write);
    if (stream is not null)
    {
        stream.CopyTo(fs);
        fs.Flush();
    }
    else
    {
        Console.WriteLine("Failed to copy appsettings.json from resources.");
    }
    Environment.Exit(0);
}

SunSetApp.Create()
    .Start();
