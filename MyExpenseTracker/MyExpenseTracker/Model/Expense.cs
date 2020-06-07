using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExpenseTracker.Model
{
    public enum CategoryPicker

    {
        home,
        entertainment,
        food,
        auto,
        education,
        health
    };
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Details { get; set; }
        public string Category { get; set; }
        public decimal Spent { get; set; }
        public DateTime Date { get; set; }


    }
}
