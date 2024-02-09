using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ClientSDK;
using TaskManager.Command.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManager.Client.Console
{
    internal class My : IController
    {
        private readonly MyClient _client;

        private readonly Dictionary<string, Func<Task>> keyValues = new();

        public My(MyClient client)
        {
            _client = client;
            keyValues = new()
            {
                ["1"] = Info,
                ["2"] = Edit,
                ["3"] = MyProject,
                ["4"] = Delete,
            };

            
        }

        public async Task ShowContents()
        {
            while (true)
            {
                System.Console.WriteLine("Что вы хотите сделать?");
                System.Console.WriteLine("1. Получить информацию о себе");
                System.Console.WriteLine("2. Изменить информацию о себе");
                System.Console.WriteLine("3. Проекты в которых я участвую");
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("4. Удалить аккаунт");
                System.Console.ResetColor();
                System.Console.WriteLine("0. Вернутся назад");
                var str = System.Console.ReadLine();
                System.Console.Clear();
                if (str != null && keyValues.ContainsKey(str))
                   await keyValues[str]();
            }

        }

        private async Task Info()
        {
            System.Console.WriteLine("Вся информация о вас");
            var user = await _client.Get();

            if (user is null)
            {
                System.Console.WriteLine("Что-то пошло не так попробуйте позже");
                return;
            }
            System.Console.WriteLine("\n"+
$"\tLastName: {user.LastName}\n" +
$"\tFirstName: {user.FirstName}\n" +
$"\tEmail: {user.Email}\n" +
$"\tPhone: {user.Phone} \n" +
$"\tLastLoginData: {user.LastLoginData}\n" +
$"\tRegistrationDate: {user.RegistrationDate}\n" +
$"\n" +
$"Нажмите Enter чтобы вернутся");
            System.Console.ReadLine();
            System.Console.Clear();
        }

        private async Task Edit()
        {
            System.Console.WriteLine("Изменить информацию о себе");
            var user = await _client.Get();

            if (user is null)
            {
                System.Console.WriteLine("Что-то пошло не так попробуйте позже");
                return;
            }

            while (true)
            {
                var str = "0";

                    Show(1,
                $"FirstName: {user.FirstName}",
                $"LastName: {user.LastName}",
                $"Email: {user.Email}",
                $"Phone: {user.Phone}",
                $"Все");
                    System.Console.WriteLine($"0. Вернутся назад");
                    str = System.Console.ReadLine();
                
                switch (str)
                {
                    case "0":
                        System.Console.Clear();
                        return;
                    case "1":
                        System.Console.WriteLine($"Ведите новое имя, если хотите оставить старое то не чего не пишите");
                        str = System.Console.ReadLine();
                        if (string.IsNullOrEmpty(str))
                            break;
                        user.FirstName = str;
                        break;

                    case "2":
                        System.Console.WriteLine($"Ведите новую фамилию, если хотите оставить старое то не чего не пишите");
                        str = System.Console.ReadLine();
                        if (string.IsNullOrEmpty(str))
                            break;
                        user.LastName = str;
                        break;

                    case "3":
                        System.Console.WriteLine($"Ведите новый email, если хотите оставить старое то не чего не пишите");
                        str = System.Console.ReadLine();
                        if (string.IsNullOrEmpty(str))
                            break;
                        user.Email = str;
                        break;

                    case "4":
                        System.Console.WriteLine($"Ведите новый номер телефона, если хотите оставить старое то не чего не пишите");
                        str = System.Console.ReadLine();
                        if (string.IsNullOrEmpty(str))
                            break;
                        user.Phone = str;
                        break;

                    case "5":
                        System.Console.WriteLine($"Ведите новое имя, если хотите оставить старое то не чего не пишите");
                        str = System.Console.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                            user.FirstName = str;

                        System.Console.WriteLine($"Ведите новую фамилию, если хотите оставить старое то не чего не пишите");
                        str = System.Console.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                            user.LastName = str;

                        System.Console.WriteLine($"Ведите новый email, если хотите оставить старое то не чего не пишите");
                        str = System.Console.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                            user.Email = str;

                        System.Console.WriteLine($"Ведите новый номер телефона, если хотите оставить старое то не чего не пишите");
                        str = System.Console.ReadLine();
                        if (!string.IsNullOrEmpty(str))
                            user.Phone = str;
                        break;
                    default:
                        break;
                }
                System.Console.Clear();
                if (await _client.Update(user))
                    System.Console.WriteLine("не удалось сохранить изменения");
            }
            
        }

        private async Task MyProject()
        {
            System.Console.WriteLine("Все проекты в которых вы участвуете");
            var projects = await _client.GetMyProject();
            Dictionary<string,ProjectModel> keyValuePairs = new();
            for (int i = 0; i < projects.Count; i++)
            {
                keyValuePairs.Add(i.ToString(), projects[i]);
                System.Console.WriteLine($"{i}. {projects[i].Name}\n\t{projects[i].Description}");
            }
            System.Console.WriteLine("Нажмите Enter чтобы вернутся");
            System.Console.ReadLine();
            System.Console.Clear();
        }

        private async Task Delete()
        {
            System.Console.WriteLine("Вы точно хотите удалить аккаунт?\n[y/n]");
            var str = System.Console.ReadLine();
            if (str is not null && str.ToLower() =="y")
            {
                var isDelete = await _client.DeleteMy(true);
                if (isDelete)
                {
                    System.Console.WriteLine("Аккаунт удален");
                }
            }
        }

            private void Show(int start=0,params string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
            {
                System.Console.WriteLine($"{start}. {strings[i]}");
                start++;
            }
        }
    }
}
