using System;
using System.Collections.Generic;
using System.Text;


namespace MyExpenseTracker.Model
{
    public class AllDataForGraph
    {
        public List<DataForGraph> GraphData { get; set; }
        //DateTime dt = DateTime.Today;
        //string thisMonth = dt.ToString("MMMM");
        public AllDataForGraph()
        {
            GraphData = new List<DataForGraph>();
            
            GetDataForGraph();
            //GraphData.Add(new DataForGraph(100, 40, 60, "June"));
        }

        public async void GetDataForGraph()
        {
            var budgetlist = await App.Database.GetBudgetByMonth("June");
            var Budget = budgetlist[0].BudgetAmount;
            var Expenses = budgetlist[0].Expense;
            var Balance = budgetlist[0].Balance;
            GraphData.Add(new DataForGraph(Budget, Expenses, Balance, "June"));
        }


    }
}
