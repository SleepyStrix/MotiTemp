using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thermostat
{
    static class Program
    {
        public static TimestampConsoleLocal localConsole = new TimestampConsoleLocal();
        public static TimestampConsoleUtc utcConsole = new TimestampConsoleUtc();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SettingCollection.LoadSettings();
            Application.Run(new Form1());
        }
    }
}
