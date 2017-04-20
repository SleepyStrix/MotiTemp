using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermostat
{
    /// <summary>
    /// Prints to console with a prefix string defined by child
    /// </summary>
    public abstract class PrefixedConsole
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(GetPrefix() + text);
        }

        public abstract string GetPrefix();
    }

    /// <summary>
    /// Prints to console with a utc timestamp prefix
    /// </summary>
    public class TimestampConsoleUtc : PrefixedConsole
    {
        public override string GetPrefix()
        {
            return DateTimeOffset.UtcNow.ToString() + ": ";
        }
    }

    /// <summary>
    /// Prints to console with a local timestamp prefix
    /// </summary>
    public class TimestampConsoleLocal : PrefixedConsole
    {
        public override string GetPrefix()
        {
            return DateTimeOffset.Now.ToString() + ": ";
        }
    }
}
