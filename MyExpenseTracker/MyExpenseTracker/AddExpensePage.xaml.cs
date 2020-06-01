using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExpenseTracker.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class AddExpensePage : ContentPage
    {

        public DateTime selDate;
        public int year;
        public int month;
        public int day;
        public bool load = false;
        public string Category_Text;
        public string Category_ImageSource;
        public string _budgetFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "budget.txt");
        public AddExpensePage()
        {
            InitializeComponent();
            var selectedDate = DateTime.Now.ToString("dd-MM-yyyy");
            datelabel.Text = selectedDate;
            Category_Text = "Food";
            Category_ImageSource = "Food.png";
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;
            selDate = DateTime.Now;
            if (!File.Exists(_budgetFile))
            {
                // DisplayAlert("Alert", "You have been alerted", "OK");
                goToBudgetPage();
            }

        }

        public async void goToBudgetPage()
        {
            await DisplayAlert("Alert", "Please set the budget first. Redirecting to Budget page", "OK");
            await Navigation.PushModalAsync(new BudgetPage());
        }

        protected async override void OnAppearing()
        {
            var _filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "select_category.txt");

            if (load == true)
            {
                var text = File.ReadAllText(_filename);
                Category_Text = text;
                Category_ImageSource = $"{Category_Text}.png";
                Category_Image.Source = Category_ImageSource;
                Category_Name.Text = Category_Text;


            }


            load = false;
        }

        //Previous day
        public void PrevButtonClicked(object sender, EventArgs args)
        {
            DateTime nowDate = new DateTime(year, month, day);

            var previewDate = nowDate.AddDays(-1);

            datelabel.Text = previewDate.ToString("dd-MM-yyyy");

            year = previewDate.Year;
            month = previewDate.Month;
            day = previewDate.Day;
            selDate = previewDate;

        }
        //Next day
        public void NextButtonClicked(object sender, EventArgs args)
        {

            DateTime nowDate = new DateTime(year, month, day);

            var nextDate = nowDate.AddDays(+1);

            datelabel.Text = nextDate.ToString("dd-MM-yyyy");

            year = nextDate.Year;
            month = nextDate.Month;
            day = nextDate.Day;
            selDate = nextDate;
        }

        private async void Category_Clicked(object sender, EventArgs e)
        {
            load = true;
            await Navigation.PushModalAsync(new Expenseform_CategoryView());

        }

        private async void Save_Clicked(object sender, EventArgs e)
        {

            Expense expense = new Expense();
            expense.Category = Category_Name.Text;
            expense.Spent = double.Parse(Amount_Entry.Text);
            expense.Details = Desc_Entry.Text;
            expense.Date = selDate;
            await App.Database.SaveExpenseAsync(expense);
            await Navigation.PopAsync();

        }

         private void Cancel_Clicked(object sender, EventArgs e)
          {

          }

       
    }
}

