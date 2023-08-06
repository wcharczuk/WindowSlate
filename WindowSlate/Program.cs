using System.Runtime.InteropServices;
using System;
using System.Text;
using System.Diagnostics.CodeAnalysis;


namespace WindowSlate
{
    internal static class Program
    {
        private static string _appGuid = "B4BE9704-485F-4F43-BC77-3C124103FD61";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + _appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Window Slate already running");
                    return;
                }
                ApplicationConfiguration.Initialize();
                Application.Run(new Settings());
            }
        }
    }
}

