using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public class MyFirstPlugin : IPlugin
    {
        public string DoSomething()
        {
           return "This is my first Plugin";
        }
    }
}
