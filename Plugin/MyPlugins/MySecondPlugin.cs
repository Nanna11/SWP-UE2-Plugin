//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Plugin
//{
//    public class MySecondPlugin : IPlugin
//    {
//        private Dictionary<string, bool> _customers = new Dictionary<string, bool>();
//        private bool isAvailable = false;
//        private string _currentCustomer = null;

//        public void SetCustomer()
//        {
//            while (_currentCustomer == null)
//            {
//                Console.WriteLine("Please insert name of customer: ");
//                _currentCustomer = Console.ReadLine();
//                if (!_customers.Keys.Contains(_currentCustomer))
//                {
//                    Console.WriteLine("Customer could not be found!");
//                    _currentCustomer = null;
//                }
//            }
//        }

//        public string DoSomething()
//        {
//            SetCustomer();
            
//            Console.WriteLine("{0} is available: {1}", _currentCustomer, isAvailable.ToString());

//            Console.WriteLine("Change availability: ");
//            isAvailable = bool.Parse(Console.ReadLine());

//            Console.WriteLine("{0} is available: {1}", _currentCustomer, isAvailable.ToString());
//            WritePluginConfig();

//            return "--------------------------------------\nSecond Plugin closed.\n--------------------------------------";
//        }

//        public string WritePluginConfig()
//        {
//            string save = "";
            
//            foreach

//            string content = "<customer>"
//                + _currentCustomer + "</customer>" + "<isAvailable>" + isAvailable.ToString() + "</isAvailable>";
//            return content;
//        }

//        public void ReadPluginConfig(string xml)
//        {
//            string[] content = xml.Split(new string[] { "<", ">" }, StringSplitOptions.None);
//            _customers.Add(content[2], bool.Parse(content[6]));
//        }
//    }
//}
