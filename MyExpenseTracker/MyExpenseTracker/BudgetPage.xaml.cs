using MyExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public string _budgetFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "budget.txt");


        public BudgetPage()
        {
            InitializeComponent();
         
            DateTime dt = DateTime.Today;
            string thisMonth = dt.ToString("MMMM");
            Month.Text = thisMonth;
        

            if (File.Exists(_budgetFile))
            {            
                setButton.IsEnabled = false;
                budgetEntry.IsEnabled = false;
            }
        }

        protected override void OnAppearing()
        {
            
            if (File.Exists(_budgetFile))
            {
                string budget=File.ReadAllText(_budgetFile);
                budgetEntry.Text = budget;
                budgetEntry.IsEnabled = false;
            }
        }
           
        




        private  void OnSetButtonClicked(object sender, EventArgs e)
        {
            File.WriteAllText(_budgetFile, budgetEntry.Text);
            decimal budget = decimal.Parse(budgetEntry.Text);         
               
            setButton.IsEnabled = false;
            budgetEntry.IsEnabled = false;
           
        }

        private void OnResetButtonClicked(object sender, EventArgs e)
        {
           budgetEntry.Text = string.Empty;

           /* if (File.Exists(_budgetFile))
            {
                File.Delete(_budgetFile);
            }
            budgetEntry.Text = String.Empty;
            setButton.IsEnabled = true;
            budgetEntry.IsEnabled = true; */       
        }
    }
}