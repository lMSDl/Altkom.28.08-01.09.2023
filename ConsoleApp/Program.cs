using Newtonsoft.Json;

//namespace ConsoleApp
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {


System.Console.WriteLine("Hello, World!");
JsonConvert.SerializeObject(new object());

// ? - (jawnie) nullowalny string
string? a = null;
string b = ToUpper(a!);

Console.WriteLine(b);

string ToUpper(string a /*!! - null-checking feature - dodaje podczas kompilacji (niejawnie) kod wyjątku jako poniżej*/)
{
    if (a == null)
        throw new ArgumentNullException(nameof(a));

    return a.ToUpper();
}


//        }
//    }
//}