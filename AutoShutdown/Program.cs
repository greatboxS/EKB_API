using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoShutdown
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static void TimerCallBack(object state)
        {
            if(DateTime.Now.Hour >= 18)
                System.Diagnostics.Process.Start("Shutdown", "-s -t 10");
        }
        static Timer Timer0 = new Timer(TimerCallBack, null, 0, 1000);
        static void Main(string[] args)
        {
            Timer0.InitializeLifetimeService();

            Console.Title = "AutoShutdown";
            // hide the console window                    
            setConsoleWindowVisibility(false, "AutoShutdown");
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
