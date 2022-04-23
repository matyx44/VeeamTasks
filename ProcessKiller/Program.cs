using System;

namespace ProcessKiller
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 3)
            {
                Console.WriteLine("Invalid number of arguments");
                Environment.Exit(0);
            }

            var processName = args[0];

            if (!int.TryParse(args[1], out int maxLifetime))
            {
                Console.WriteLine("Invalid argument for maximum lifetime.");
                Environment.Exit(0);
            }

            if (!int.TryParse(args[1], out int frequency))
            {
                Console.WriteLine("Invalid argument for frequency.");
                Environment.Exit(0);
            }
    
            new Looper(processName, maxLifetime, frequency).Loop();
        }
    }
}
