using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProcessKiller
{
    internal class Looper
    {
        readonly string processName;
        readonly int maxLifetimeMinutes;
        readonly int frequency;
        List<Process> log;
        

        public Looper(string processName, int maxLifetime, int frequency)
        {
            this.processName = processName;
            this.maxLifetimeMinutes = maxLifetime;
            this.frequency = frequency;
            log = new List<Process>();
        }

        public void Loop()
        {
            //Run loop killing in background
            new Task(KillingLoop).Start();

            //Wait for the user to kill the program.
            Console.WriteLine("Press 'q' key to stop...");

            while (true)
            {
                var key = Console.ReadKey();
                if(key.Key == ConsoleKey.Q)
                {
                    Console.WriteLine($"\n\nProgram is finished.");
                    if(log.Count > 0)
                    {
                        Console.WriteLine($"Killed {log.Count} processes:");
                        foreach (Process process in log)
                        {
                            Console.WriteLine($"Process {process.ProcessName} that started at {process.StartTime} ");
                        }
                    }
                    
                    Environment.Exit(0);
                }
            }       
        }

        private async void KillingLoop()
        {
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(frequency));
            //.NET6.0 addition
            while (await timer.WaitForNextTickAsync())
            {
                Console.WriteLine($"\n\nChecking for process {processName} older than {maxLifetimeMinutes} minutes...");
                KillProcess();
            }
        }

        private void KillProcess()
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                TimeSpan runtime;
                try
                {
                    runtime = DateTime.Now - process.StartTime;
                    if (runtime.TotalMinutes >= maxLifetimeMinutes)
                    {
                        Console.WriteLine($"Found process {process.ProcessName} older than {maxLifetimeMinutes} minute(s), terminating the process...");
                        process.Kill();
                        Console.WriteLine($"The process {process.ProcessName} was terminated.");
                        log.Add(process);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Unable to kill the process {process.ProcessName}.");
                    continue;
                }
            }
        }
    }
}
