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
    public partial class AboutUs : ContentPage
    {
        //public string _budgetFile = Path.Combine(
        //    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "budget.txt");
        public AboutUs()
        {
            InitializeComponent();
            //if (!File.Exists(_budgetFile))
            //{
            //    
            //    goToBudgetPage();
            //}
        }

        //public async void goToBudgetPage()
        //{
        //    await DisplayAlert("Alert", "Please set the budget first. Redirecting to Budget page", "OK");
        //    await Navigation.PushModalAsync(new BudgetPage());
        //}
    }
}