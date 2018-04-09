using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public class MyFirstPlugin : IPlugin
    {
        Dictionary<int, Costumer> _customers = null;
        Dictionary<int, double> _amounts = new Dictionary<int, double>();
        private int? _currentCustomer = null;

        public void SetCustomer()
        {
            while (_currentCustomer == null)
            {
                Console.WriteLine("Please insert name of customer: ");
                _currentCustomer = Int32.Parse(Console.ReadLine());
                if (!_customers.Keys.Contains((int)_currentCustomer))
                {
                    Console.WriteLine("Customer could not be found!");
                    _currentCustomer = null;
                }
            }
        }

        public string DoSomething()
        {
            SetCustomer();
            if (!_amounts.ContainsKey((int)_currentCustomer)) _amounts.Add((int)_currentCustomer, 0);
            Console.WriteLine("{0} has to pay: {1}", _currentCustomer, _amounts[(int)_currentCustomer]);

            Console.WriteLine("New Amount to pay: ");
            
            _amounts[(int)_currentCustomer] = double.Parse(Console.ReadLine());

            Console.WriteLine("{0} has to pay: {1}", _currentCustomer, _amounts[(int)_currentCustomer]);
            WritePluginConfig();

            return "--------------------------------------\nFirst Plugin closed.\n--------------------------------------";
        }

        public List<Costumer> Costumers
        {
            set
            {
                _customers = new Dictionary<int, Costumer>();
                foreach(Costumer c in value)
                {
                    _customers.Add(c.ID, c);
                }
            }
        }

        public string WritePluginConfig()
        {
            string save = "";
            foreach(KeyValuePair<int, double> kvp in _amounts)
            {
                save += "<customer>" + "<ID>" + kvp.Key + "</ID>" + "<amount>" + kvp.Value + "</amount>" + "</customer>";
            }
            return save;
        }

        public void ReadPluginConfig(string xml)
        {
            string[] customers = xml.Split(new string[] { "</customer>" }, StringSplitOptions.None);
            foreach(string s in customers)
            {
                if (s == "") continue;
                string tmp;
                tmp = s.TrimStart("<customer>".ToArray<char>());
                tmp = tmp.TrimStart("<ID>".ToArray<char>());
                int id = Int32.Parse(tmp.Substring(0, tmp.IndexOf("</ID>")));
                tmp = tmp.TrimStart(id.ToString().ToArray());
                tmp = tmp.TrimStart("</ID>".ToArray<char>());
                tmp = tmp.TrimStart("<amount>".ToArray<char>());
                double amount = Double.Parse(tmp.Substring(0, tmp.IndexOf("</amount>")));
                _amounts.Add(id, amount);
            }
        }
    }
}
