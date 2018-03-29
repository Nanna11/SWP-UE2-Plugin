using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    class Program
    {
        static void Main(string[] args)
        {
            PlugInManager p = new PlugInManager();
            foreach(IPlugin plugin in p.Plugins)
            {
                Console.WriteLine(plugin.DoSomething());
            }
            Console.ReadKey();
        }
    }
}
