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
            List<Costumer> customers = new List<Costumer>();
            customers.Add(new Costumer(1, "name"));

            PlugInManager p = new PlugInManager(customers);
            foreach(IPlugin plugin in p.Plugins)
            {
                Console.WriteLine(plugin.DoSomething());
            }
            p.Save();
            p = new PlugInManager(customers);
            foreach (IPlugin plugin in p.Plugins)
            {
                Console.WriteLine(plugin.DoSomething());
            }

            Console.ReadKey();
        }
    }
}
