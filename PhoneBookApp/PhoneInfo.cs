using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp
{
    enum PhoneType
    {
        Mobile,Work,Home
    }

    [Serializable]
    class PhoneInfo
    {
        public PhoneType phoneType { get; set; }
        public string Number { get; set; }
    }
}
