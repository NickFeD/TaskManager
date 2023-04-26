namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => {
                await Task.Delay(5000);
                File.Create("C:\\Users\\sokol\\Desktop\\First.txt");    
            });
            Console.WriteLine("1");
            File.Create("C:\\Users\\sokol\\Desktop\\Second.txt");
        }
    }
}