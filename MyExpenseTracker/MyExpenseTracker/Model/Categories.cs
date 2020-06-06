using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExpenseTracker.Model
{
    public class Categories
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Month { get; set; }
        public double Spent { get; set; }
        public double Balance { get; set; }
        public double Budget { get; set; }
        public string IconSource { get; set; }
        [Ignore]
        public List<Expense> expenses { get; set; }

    }
}
