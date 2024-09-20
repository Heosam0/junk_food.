using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;

namespace lab
{
    public class Client
    {
        public string TableName = "clients";
        public string Name;
        public string Surname;
        public string Patronymic;
        public string dateofbirth;
        public string passport;
        public string number;
        public string email;

        public Client(string first_name, string last_name, string patronymic, string dateofbirth, string passport, string number, string email)
        {
            Name = first_name;
            Surname = last_name;
            Patronymic = patronymic;
            this.dateofbirth = dateofbirth;
            this.passport = passport;
            this.number = number;
            this.email = email;
        }
    }
}
