
using Microsoft.Extensions.Configuration;


//Microsoft.Extensions.Configuration
IConfiguration config = new ConfigurationBuilder()
    //Microsoft.Extensions.Configuration.Json
    .AddJsonFile("Config/config.json")
    //Microsoft.Extensions.Configuration.Xml
    .AddXmlFile("Config/config.xml", optional: true)
    //Microsoft.Extensions.Configuration.Ini
    .AddIniFile("Config/config.ini")
    //NetEscapades.Configuration.Yaml
    .AddYamlFile("Config/config.yaml", optional: false, reloadOnChange: true)

    //ostatnia załadowana konfiguracja zastępuje wartości kluczy załądowanych wcześniej
    .Build();


for (int i = 0; i < int.Parse(config["Count"]); i++)
{

    Console.WriteLine($"Hello {config["HelloJson"]}");
    Console.WriteLine($"Hello {config["HelloXml"]}");
    Console.WriteLine($"Hello {config["HelloIni"]}");
    Console.WriteLine($"Hello {config["HelloYaml"]}");

    Console.ReadLine();
}


Console.WriteLine("__________");

var greetingsSection = config.GetSection("Greetings");
var tragetsSection = greetingsSection.GetSection("Targets");

Console.WriteLine($"{greetingsSection["Value"]} from {tragetsSection["From"]} to {config["Greetings:Targets:To"]}");