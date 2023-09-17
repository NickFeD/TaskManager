// See https://aka.ms/new-console-template for more information
using TaskManager.Client.Console;
using TaskManager.ClientSDK;
Console.WriteLine("client create");
var client = new Client("https://localhost:44329", new HttpClient(), new());
Console.WriteLine("Добро пожаловать!");
var task = Authorization(client);

Dictionary<string, IController> dict = new();
dict.Add("1", new My(client.My));

await task;
Console.WriteLine("Выбрать контролер");
var str = Console.ReadLine();
Console.Clear();
await dict[str].ShowContents();

static async Task Authorization(Client client)
{
    Console.WriteLine("Зайдите на аккаунт");
    while (true)
    {
        Console.Write("Email: ");
        var email = Console.ReadLine();
        Console.Write("Password: ");
        var password = Console.ReadLine();
        var authResponse = await client.Account.AuthToken(new TaskManager.Command.Models.AuthRequest { Email = email, Password = password });
        if (authResponse != null)
        {
            Console.Clear();
            Console.WriteLine($"Добро пожаловать {(await client.My.Get()).FirstName}");
            return;
        }
        Console.Clear();
        Console.WriteLine("Неправильный пароль или email");
    }
}