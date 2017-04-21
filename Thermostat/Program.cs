using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thermostat
{
    static class Program
    {
        public static TimestampConsoleLocal localConsole = new TimestampConsoleLocal();
        public static TimestampConsoleUtc utcConsole = new TimestampConsoleUtc();
        public static Form1 form1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form1 = new Form1();
            SettingCollection.LoadSettings();
            Application.Run(form1);
        }

        public static void SetStatus(string status, Color color)
        {
            if (form1 != null)
            {
                form1.SetStatus(status, color);
            }
        }
    }
}
