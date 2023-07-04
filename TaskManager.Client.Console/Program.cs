// See https://aka.ms/new-console-template for more information
using TaskManager.ClientSDK;
Console.WriteLine("client create");

var client  = new Client("https://localhost:44329", new HttpClient(),new());

Console.WriteLine("client completed");
Console.ReadLine();

Console.WriteLine("Users.GetAllAsync()");
Console.WriteLine(client.Users.GetAllAsync().Result);

Console.WriteLine("client.Users.GetByUserId(3)");
Console.WriteLine(client.Users.GetByUserId(3));

Console.WriteLine("client.Users.CreateAsync(user)");
var user = new UserModel()
{
    FirstName = "FirstName",
    LastName = "LastName",
    Email = "Email",
    LastLoginData = DateTime.Now,
    Password = "password",
    Phone = "phone",
};
Console.WriteLine(client.Users.CreateAsync(user));

Console.WriteLine("client.Users.UpdateAsync(2,user)");

user.FirstName = "name";
Console.WriteLine(client.Users.UpdateAsync(2,user));

Console.WriteLine("client.Users.DeleteAsync(3)");
Console.WriteLine(client.Users.DeleteAsync(3));

Console.WriteLine(client.Users.GetByUserId(3));
Console.WriteLine(client.Users.GetByUserId(3));