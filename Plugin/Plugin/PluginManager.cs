using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Reflection;
using System;

namespace Plugin
{
    public class PlugInManager
    {
        LinkedList<IPlugin> _Plugins = new LinkedList<IPlugin>();
        static LinkedList<string> types = null;
        static ReaderWriterLock rwl = new ReaderWriterLock();
        LinkedList<string> contains = new LinkedList<string>();

        public PlugInManager()
        {
            if (types == null)
            {
                ReadConfig();
            }
            foreach (string s in types)
            {
                Add(s);
            }
        }

        public IEnumerable<IPlugin> Plugins
        {
            get { return _Plugins; }
        }

        public void Add(IPlugin plugin)
        {
            string type = plugin.GetType().ToString();
            if (contains.Contains(type)) return;
            _Plugins.AddLast(plugin);
            contains.AddFirst(type);
        }

        public void Add(string plugin)
        {
            Type type = Type.GetType(plugin);
            try
            {
                object obj = Activator.CreateInstance(type);
                IPlugin nplugin = (IPlugin)obj;
                this.Add(nplugin);
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

        private void ReadConfig()
        {
            types = new LinkedList<string>();
            string deploypath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configpath = Path.Combine(deploypath, "config", "PlugInManager.txt");

            FileStream file = new FileStream(configpath, FileMode.Open);
            StreamReader sr = new StreamReader(file);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string PluginName = line.Substring(0, line.LastIndexOf(' '));
                string Status = line.Substring(line.IndexOf(' ') + 1);

                if (Status == "active") types.AddFirst(PluginName);
            }
            sr.Close();

        }

    }
}