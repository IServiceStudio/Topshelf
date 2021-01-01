using System;
using System.Timers;
using Topshelf;

namespace TopshelfConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(c =>
            {
                c.Service<Job>(job =>
                {
                    job.ConstructUsing(name => new Job());
                    job.WhenStarted(s=>s.Start());
                    job.WhenStopped(s => s.Stop());
                });
                c.RunAsLocalSystem();
                c.SetDescription("Sample TopShelf Host");
                c.SetDisplayName("TopShelf Job");
                c.SetServiceName("TopShelf Job");
            });
        }
    }

    public class Job
    {
        readonly Timer timer;
        public Job()
        {
            timer = new Timer(1000) { AutoReset = true };
            timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start() { timer.Start(); }
        public void Stop() { timer.Stop(); }
    }
}
