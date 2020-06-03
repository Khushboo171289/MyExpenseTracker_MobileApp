using MyExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class OverviewPage : ContentPage
    {
        public OverviewPage()
        {
            InitializeComponent();
            DateTime dt = DateTime.Today;
            string thisMonth = dt.ToString("MMMM");
            Month.Text = $"Expenses for the Month of {thisMonth}";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            DateTime dt = DateTime.Today;
            string thisMonth = dt.ToString("MMMM");
            Month.Text = $"Expenses for the Month of {thisMonth}";

             string _budgetFile = Path.Combine(
             Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "budget.txt");
           
            List<Expense> myExpenses = await App.Database.GetExpensesAsync();
            List<Expense> filteredExpense = new List<Expense>();
            foreach(var item in myExpenses)
            {
                if(item.Date.Month==DateTime.Today.Month)
                {
                    filteredExpense.Add(item);
                }
            }

            MyOverview.ItemsSource = filteredExpense;
            decimal expenditure = (App.Database.SumExpenseAsync());
            Expenses.Text = $"Expenses=US$ {expenditure.ToString()}";
            decimal budget = 0;

            if (!File.Exists(_budgetFile))
            {
                Budget.Text = "Budget=US$ 0.00";
                
            }
            else
            {

                budget = decimal.Parse(File.ReadAllText(_budgetFile));
                Budget.Text = $"Budget=US$ {budget.ToString()}";
               


            }
            decimal balance = budget - expenditure;
            Balance.Text = $"Balance=US$ {balance.ToString()}";        
            



            /*foreach (var item in myExpenses)
              {
                  await App.Database.DeleteExpenseAsync(item);
              }*/
        }


    }

}