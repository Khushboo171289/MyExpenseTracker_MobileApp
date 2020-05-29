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
    }
}
