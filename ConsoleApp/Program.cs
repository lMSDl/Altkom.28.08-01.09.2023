
using ConsoleApp.Config.Models;
using ConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


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



//obiekt do konfiguracji wstzykiwania zależności
var serviceCollection = new ServiceCollection();
//rejestracja typu DebugOutputService
serviceCollection.AddTransient<DebugOutputService>();

//rejestracja typów SubzeroFontService, MuzzleFontService, ShadowSmallFontService na interfejs IFontService
serviceCollection.AddTransient<IFontService, SubzeroFontService>();
serviceCollection.AddTransient<IFontService, MuzzleFontService>();
serviceCollection.AddTransient<IFontService, ShadowSmallFontService>();

//ręczna rejestracja na interfejs IOutputService
serviceCollection.AddTransient<IOutputService>(x => new ConsoleOutputService(new StandardFontService()));

//transient - zawsze nowa instancja
serviceCollection.AddTransient<IOutputService, ConsoleRandomFontOutputService>();
//singleton - zawsze ta sama instancja
serviceCollection.AddSingleton<IOutputService, ConsoleRandomFontOutputService>();
//scoped - instanca tworzona przy zmianie scope
serviceCollection.AddScoped<IOutputService, ConsoleRandomFontOutputService>();

serviceCollection.AddSingleton<IConfiguration>(x => config);

//zbudowanie dostawcy usług
var serviceProvider = serviceCollection.BuildServiceProvider();

//pobranie pojedynczej usługi
var consoleService = serviceProvider.GetService<ConsoleOutputService>();

consoleService?.WriteText("Hello from console");


//pobranie pojedynczej usługi powoduje wybranie tej ostatnio zarejestrowanej
var debugService = serviceProvider.GetService<IOutputService>();
debugService.WriteText("Hello from debug");


var configService = serviceProvider.GetService<IConfiguration>();
Console.WriteLine($"Hello {configService["HelloYaml"]}");

//wytworzenie scope
var scope = serviceProvider.CreateScope();

while (true)
{
    if(DateTime.Now.Second % 2 == 0)
    {
        scope.Dispose();
        scope = serviceProvider.CreateScope();
    }

    //pobranie wszystkich usług zarejestrowanych pod wskazanym interfejsem
    var outputServices = scope.ServiceProvider.GetServices<IOutputService>();
    foreach (var service in outputServices)
    {
        service.WriteText("Hello");
    }
    Console.ReadLine();
}







static void ConfigurationDemo(IConfiguration config)
{
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
}