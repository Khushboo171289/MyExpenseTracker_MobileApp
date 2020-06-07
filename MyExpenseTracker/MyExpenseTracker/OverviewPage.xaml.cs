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
        public List<Categories> categories;
        public OverviewPage()
        {
            InitializeComponent();
            DateTime dt = DateTime.Today;
            string thisMonth = dt.ToString("MMMM"); 
        }



        protected async override void OnAppearing()
        {
            base.OnAppearing();

           

            DateTime dt = DateTime.Today;
            string thisMonth = dt.ToString("MMMM");
            BudgetField.Text = $"{thisMonth} Budget";
            var budgetlist = await App.Database.GetBudgetByMonth(thisMonth);
            if (budgetlist.Count > 0)
            {
                Budget.Text = $"US ${budgetlist[0].BudgetAmount}";
                Expenses.Text = $"US ${budgetlist[0].Expense}";
                Balance.Text = $"US ${budgetlist[0].Balance}";

                if(budgetlist[0].Balance <= 0)
                {
                    Balance.BackgroundColor = Color.Red;
                    Balance.TextColor = Color.Black;
                }
                else
                {
                    if ((budgetlist[0].Balance < budgetlist[0].Expense) && (budgetlist[0].Balance > 0))
                    {
                        Balance.BackgroundColor = Color.Orange;
                        Balance.TextColor = Color.Black;
                    }
                    else
                    {
                        if(budgetlist[0].Expense == 0 && budgetlist[0].Balance ==0 )
                        {
                            Balance.BackgroundColor = Color.FromHex("#3F539F");
                            Balance.TextColor = Color.Black;
                        }
                        else
                        { 
                        Balance.BackgroundColor = Color.FromHex("#3F539F");
                        Balance.TextColor = Color.Black;
}
                    }
                }

            }
            else
            {
                Budget.Text = "Budget=US$ 0.00";
                Expenses.Text = "Budget=US$ 0.00";
                Balance.Text = "Budget=US$ 0.00";
            }

            
            CategoryListView.ItemsSource = await App.Database.GetCategoriesAsync();
        }



       
    }
}