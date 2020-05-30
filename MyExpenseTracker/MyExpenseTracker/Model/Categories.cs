using System;
using System.Collections.Generic;
using System.Text;

namespace MyExpenseTracker.Model
{
    public class Categories
    {
        public string Name { get; set; }
        public double Spent { get; set; }
        public double Balance { get; set; }
        public double Budget { get; set; }
        public string IconSource { get; set; }

        public List<Expense> expenses;


        public Categories(string name, double spent, double balance, double budget, string Iconsource)
        {
            this.Name = name;
            this.Spent = spent;
            this.Balance = balance;
            this.Budget = budget;
            this.IconSource = Iconsource;
        }

    }
}
