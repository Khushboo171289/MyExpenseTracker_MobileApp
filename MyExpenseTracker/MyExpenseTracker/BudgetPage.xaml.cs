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
    public partial class BudgetPage : ContentPage
    {
        public string _budgetFile = Path.Combine(
             Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "budget.txt");
        public BudgetPage()
        {
            InitializeComponent();

            if (File.Exists(_budgetFile))
            {
                budgetEntry.Text = File.ReadAllText(_budgetFile);
                setButton.IsEnabled = false;
                budgetEntry.IsReadOnly = true;
            }
        }




        private async void OnSetButtonClicked(object sender, EventArgs e)
        {
            File.WriteAllText(_budgetFile, budgetEntry.Text);
            decimal budget = decimal.Parse(budgetEntry.Text);
            decimal expenditure = decimal.Parse(expenditureEntry.Text);
            decimal balance = budget - expenditure;
            calcBalance.Text = balance.ToString();
            setButton.IsEnabled = false;
            budgetEntry.IsReadOnly = true;
            await Navigation.PushModalAsync(new AddExpensePage());
        }

        private void OnResetButtonClicked(object sender, EventArgs e)
        {
            if (File.Exists(_budgetFile))
            {
                File.Delete(_budgetFile);
            }
            budgetEntry.Text = string.Empty;
            setButton.IsEnabled = true;
            budgetEntry.IsReadOnly = false;
        }
    }
}