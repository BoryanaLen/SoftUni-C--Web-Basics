using SIS.MvcFramework;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Suls.Web
{
    public class Program
    {
        public static async Task Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            await WebHost.StartAsync(new Startup());
        }
    }
}
