using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Thermostat
{
    public class TimeStampedTimer : System.Timers.Timer
    {
        PrefixedConsole timeConsole;

        public TimeStampedTimer(double interval, bool utc) : base (interval)
        {
            if (utc)
            {
                timeConsole = new TimestampConsoleUtc();
            }
            else
            {
                timeConsole = new TimestampConsoleLocal();
            }
        }

        public new void Start()
        {
            base.Start();
            this.Elapsed += new System.Timers.ElapsedEventHandler(TimeStampedTimer_Elapsed);
        }

        private void TimeStampedTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (timeConsole != null)
            {
                timeConsole.WriteLine("Timer Elapsed");
            }
            else
            {
                new TimestampConsoleUtc().WriteLine("Timer Elapsed");
            }
        }

    }
}
