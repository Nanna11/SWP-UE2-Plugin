﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public interface IPlugin
    {
        List<Costumer> Costumers {set;}
        string DoSomething();
        string WritePluginConfig();
        void ReadPluginConfig(string xml);
    }
}
