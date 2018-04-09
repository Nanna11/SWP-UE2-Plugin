using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public class Costumer
    {
        int _ID;
        string _Name;

        public Costumer(int ID, string Name)
        {
            _ID = ID;
            _Name = Name;
        }

        public int ID
        {
            get
            {
                return _ID;
            }
        }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
    }
}
