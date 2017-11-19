using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp
{
    
    [Serializable]
    class Contact
    {
        public Contact()
        {
            PhoneNumberList = new List<PhoneInfo>();
        }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string Telegram { get; set; }
        public List<PhoneInfo> PhoneNumberList { get; set; }
        public string FullInfo
        {
            get
            {
                return Name + " " + FamilyName;
            }
          
        }
    }
}
