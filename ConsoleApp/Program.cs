
using ConsoleApp.Config.Models;
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

    //Microsoft.Extensions.Configuration.Binder
    .AddEnvironmentVariables()
    //ostatnia załadowana konfiguracja zastępuje wartości kluczy załądowanych wcześniej
    .Build();


//for (int i = 0; i < int.Parse(config["Count"]); i++)
//Binder pozwala na pobieranie wartości z konfiguracji innych niż string
  for (int i = 0; i < config.GetValue<int>("Count"); i++)
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

//Microsoft.Extensions.Configuration.Binder
//wytwrzamy obiekt na podstawie konfiguracji
var greetings = greetingsSection.Get<Greetings>();
Console.WriteLine($"{greetings.Value} from {greetings.Targets.From} to {greetings.Targets.To}");

//uzupełniamy obiekt danymi z konfiguracji
greetings = new Greetings();
greetingsSection.Bind(greetings);
Console.WriteLine($"{greetings.Value} from {greetings.Targets.From} to {greetings.Targets.To}");


Console.WriteLine(config["Temp"]);
Console.WriteLine(config["bajka"]);