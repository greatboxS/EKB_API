using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

namespace _AUTO_UPADATE_DB
{
    class Program
    {
        static IDbName database = new RealDb();
        static int Tick = 0;
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static void TimerCallBack(object state)
        {
            Tick++;
            Console.WriteLine($"Tick value: {Tick}");
            if (Tick == 60 * 15)
            {
                Console.WriteLine("Start checking...");
                Tick = 0;
                AutoScheule AutoSchedule = new AutoScheule(database);
                //AutoSchedule.Update();

                

                Console.WriteLine("Stop checking...");
            }
        }
        static Timer Timer0 = new Timer(TimerCallBack, null, 0, 1000);
        static void Main(string[] args)
        {
            Timer0.InitializeLifetimeService();

            Console.Title = "AutoUpdateScheduleTask";
            // hide the console window                    
            setConsoleWindowVisibility(true, "AutoUpdateScheduleTask");

            Console.WriteLine("Start checking...");

            AutoScheule AutoSchedule = new AutoScheule(database);
            AutoSchedule.Update();

            //Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            //var files = Directory.EnumerateFiles(@"\\svproxy\Department\CP\Planning Team\生管進度 - Production Schedule - Tiến độ của sinh quản\2020\202003");
            //foreach (var item in files)
            //{
            //    Console.WriteLine(item);
            //}

            Console.WriteLine("Stop checking...");

            Console.ReadLine();
        }

        public static void setConsoleWindowVisibility(bool visible, string title)
        {
            // below is Brandon's code            
            //Sometimes System.Windows.Forms.Application.ExecutablePath works for the caption depending on the system you are running under.           
            IntPtr hWnd = FindWindow(null, title);

            if (hWnd != IntPtr.Zero)
            {
                if (!visible)
                    //Hide the window                    
                    ShowWindow(hWnd, 0); // 0 = SW_HIDE                
                else
                    //Show window again                    
                    ShowWindow(hWnd, 1); //1 = SW_SHOWNORMA           
            }
        }
    }
}
