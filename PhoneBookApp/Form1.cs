using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBookApp
{
    public partial class Form1 : Form
    {
        PhoneBook phoneBook;
        public Form1()
        {
            InitializeComponent();
            phoneBook = new PhoneBook();
            
            lstContactList.DisplayMember = "FullInfo";
            lstContactList.DataSource = contactBindingSource;
            contactBindingSource.DataSource = phoneBook.ContactList;
            phoneTypeComboBox.DataSource = Enum.GetValues(typeof(PhoneType));           //set items of combobox to items in enum


        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            phoneBook.Save();
            MessageBox.Show("File Saved Successfully.");
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            phoneBook=PhoneBook.Open();
           
            if (phoneBook == null)
            {
                MessageBox.Show("Loading File Was Not Successful!");
                phoneBook = new PhoneBook();
            }
            contactBindingSource.DataSource = phoneBook.ContactList;        //loads phonebook into controls wich it's bound to
           
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            lstSearch.Items.Clear();
            string requested = txtSerach.Text;
            foreach (var contact in phoneBook.ContactList)
            {
                foreach (var phone in contact.PhoneNumberList)
                {
                    if (phone.Number.Contains(requested) ) 
                    {
                        lstSearch.DisplayMember = "FullInfo";
                        lstSearch.Items.Add(contact);
                        
                    }
                }
                if (contact.Name.Contains(requested) || contact.FamilyName.Contains(requested) || contact.Address.Contains(requested))
                {
                    lstSearch.DisplayMember = "FullInfo";
                    lstSearch.Items.Add(contact);
                }

            }
            if(lstSearch.Items.Count==0)
                MessageBox.Show("Not Found!");         
        }

        private void lstSearch_SelectedValueChanged(object sender, EventArgs e)
        {
            lstContactList.SelectedItem = lstSearch.SelectedItem;       //syncing selected items of both listboxes
        }

        private void button1_Click(object sender, EventArgs e)
        {
            phoneBook.textSerializer();            
        }

        private void btnDeserialize_Click(object sender, EventArgs e)
        {
            phoneBook = PhoneBook.textDeserializer();
            if (phoneBook == null)
            {
                MessageBox.Show("Loading File Was Not Successful!");
            }
            contactBindingSource.DataSource = phoneBook.ContactList;        //loads phonebook into controls wich it's bound to
        }
    }
}
