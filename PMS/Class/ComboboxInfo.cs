using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Class
{
    internal class ComboboxInfo
    {
        public String Name { get; set; }
        public String ID { get; set; }
        public ComboboxInfo(String name, String id)
        {
            Name = name;
            ID = id;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
