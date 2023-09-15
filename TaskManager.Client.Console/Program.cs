// See https://aka.ms/new-console-template for more information
using TaskManager.ClientSDK;
Console.WriteLine("client create");

var client  = new Client("https://localhost:44329", new HttpClient(),new());

Console.WriteLine("client completed");
var temp1 = await client.Account.AuthToken(new TaskManager.Command.Models.AuthRequest { Email = "string",Password ="string"});
Console.WriteLine(temp1.ExpiresToken.ToLocalTime());
Console.ReadLine();
for (int i = 0;; i++)
{
    var temp = await client.My.GetMy();
    Console.WriteLine(temp.FirstName);
    await Task.Delay(500);
}

Console.WriteLine("stop");