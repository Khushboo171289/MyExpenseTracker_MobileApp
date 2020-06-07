using System;
using System.Collections.Generic;
using System.Text;


namespace MyExpenseTracker.Model
{
    public class AllDataForGraph
    {
        public List<DataForGraph> GraphData { get; set; }
        
        public AllDataForGraph()
        {
            GraphData = new List<DataForGraph>();
            
            GetDataForGraph();
           
        }

        public async void GetDataForGraph()
        {
            DateTime dt = DateTime.Today;
            string thisMonth = dt.ToString("MMMM");
           
            var budgetlist = await App.Database.GetBudgetsAsync();
            for (int i = 0; i < budgetlist.Count; i++)
            {
                var Budget = budgetlist[i].BudgetAmount;
                var Expenses = budgetlist[i].Expense;
                var Balance = budgetlist[i].Balance;
                var Month = budgetlist[i].Month;
                GraphData.Add(new DataForGraph(Budget, Expenses, Balance,Month));
            }

        }


    }
}
