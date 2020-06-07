using System;
using System.Collections.Generic;
using System.Text;

namespace MyExpenseTracker.Model
{
    public class DataForGraph
    {
        public double budget { get; set; }
        public double expenditure { get; set; }
        public double balance { get; set; }

        public string month { get; set; }

        public DataForGraph(double bu, double ex, double ba, string mo)
        {
            budget = bu;
            expenditure = ex;
            balance = ba;
            month = mo;
        }


    }
}
