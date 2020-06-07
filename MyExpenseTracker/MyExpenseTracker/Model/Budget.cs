using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExpenseTracker.Model
{
    public class Budget
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string Month { get; set; }
        public double BudgetAmount { get; set; }
        public double Expense { get; set; }
        public double Balance { get; set; }
        [Ignore]
        public List<Categories> categories { get; set; }
    }
}
