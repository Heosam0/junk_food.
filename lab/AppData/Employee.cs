using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.AppData
{
    public class Employee
    {
        public int Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime StartDate { get; set; }
        public int PostId { get; set; } // Foreign Key
    }

    public class Order
    {
        public int Id { get; set; }
        public string HowCall { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        public bool ForDelivery { get; set; }
        public string Status { get; set; }
        public decimal Sum { get; set; }
        public int TableNumber { get; set; }
    }

    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } // Will be stored as JSON
        public decimal Price { get; set; }
    }

    public class MenuIngredient
    {
        public int Id { get; set; }
        public string Name { get; set; } // Foreign Key
        public decimal Unit { get; set; }
    }


}
