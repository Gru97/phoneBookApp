using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp
{
    [Serializable]
    class PhoneBook
    {
        public static string fileAddress
        {
            get
            { return @"..\..\Test\PhoneBook"; }     //goes back two folders form the primary folder 

            private set { }
        }

        public List<Contact> ContactList { get; set; }
        public PhoneBook()
        {
            ContactList = new List<Contact>();
        }
        public void Save()
        {
            try
            {
                using (var stream = File.Open(@"..\..\Test\PhoneBook.dat", FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, this);        //convert to binary file
                }
            }
            catch 
            {}
           
        }
        public static PhoneBook Open()
        {
            PhoneBook b = null;
            try
            {
                if (File.Exists(fileAddress))
                {
                    using (var stream = File.Open(@"..\..\Test\PhoneBook.dat", FileMode.Open))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        b= (PhoneBook)bf.Deserialize(stream);          //convert file to object                       
                    }              
                } 
            }
            catch { }
            return b;
        }

        public void textSerializer()
        {          
            using (StreamWriter sw = new StreamWriter(fileAddress))
            {
                foreach (var contact in ContactList)
                {
                    string s=contact.Name+","+ contact.FamilyName+ ","+ contact.Address+ ","+contact.Telegram;
                    foreach (var phone in contact.PhoneNumberList)
                    {
                        s += ",";
                        s += phone.phoneType + "," + phone.Number;
                    }
                    sw.WriteLine(s);
                }
                
            }
        }      
        public static PhoneBook textDeserializer()
        {
          
            PhoneBook b = new PhoneBook();
            string strContact;
            string [] str;

            using (StreamReader sr = new StreamReader(fileAddress))
            {
                while(!sr.EndOfStream)
                {
                    strContact = sr.ReadLine();
                    str = strContact.Split(',');
                    Contact contact = new Contact();
                    contact.Name = str[0];
                    contact.FamilyName = str[1];
                    contact.Address = str[2];
                    contact.Telegram = str[3];
                    
                    for(int j=4;j<str.Length-1;j++)
                    {
                        PhoneInfo phone = new PhoneInfo();
                        phone.phoneType = (PhoneType)Enum.Parse(typeof(PhoneType), str[j]);         //converting string to enum
                       
                        phone.Number = str[++j];                    //next item in array is number
                        contact.PhoneNumberList.Add(phone);
                       
                    }                                                 
                    b.ContactList.Add(contact);
                }

            }
            return b;
        }
    }
}
