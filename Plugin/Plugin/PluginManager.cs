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
        static Dictionary<string, string> mockDirectory = new Dictionary<string, string>();

        public PlugInManager(List<Costumer> list)
        {
            if (types == null)
            {
                ReadConfig();
            }
            foreach (string s in types)
            {
                Add(s);
                PrepareData(s);
                SetCustomers(list);
            }
        }

        public void SetCustomers(List<Costumer> list)
        {
            foreach(IPlugin p in _Plugins)
            {
                p.Costumers = list;
            }
        }

        public void Save()
        {
            foreach (IPlugin p in _Plugins)
            {
                try
                {
                    mockDirectory[p.GetType().ToString()] = p.WritePluginConfig();
                }
                catch (KeyNotFoundException)
                {
                    mockDirectory.Add(p.GetType().ToString(), p.WritePluginConfig());
                }
            }
        }

        public void PrepareData(string s)
        {
            foreach (IPlugin p in _Plugins)
            {
                string type = s.Substring(0, s.IndexOf(","));
                try
                {
                    p.ReadPluginConfig(mockDirectory[type]);
                }
                catch (KeyNotFoundException)
                {

                }
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