using MyExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
            Save.IsEnabled = true;

        }

        public async void goToBudgetPage()
        {
            await DisplayAlert("Alert", "Please set the budget first. Redirecting to Budget page", "OK");
            await Navigation.PushModalAsync(new BudgetPage());
        }

        protected async override void OnAppearing()
        {
            DateTime dt = DateTime.Today;

            string thisMonth = dt.ToString("MMMM");
            var budgetlist = await App.Database.GetBudgetByMonth(thisMonth);
            if (budgetlist.Count == 0)
            {
                await DisplayAlert("Alert", "Set the monthly budget", "OK");
                goToBudgetPage();
            }

            var _filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "select_category.txt");

            if (load == true)
            {
                var text = File.ReadAllText(_filename);
                Category_Text = text;
                Categories category = new Categories();
                category = App.Database.GetSpecificCategory(text);
                Category_ImageSource = category.IconSource;
                Category_Image.Source = Category_ImageSource;
                Category_Name.Text = Category_Text;



            }


            load = false;
        }

        //Previous day
        public void PrevButtonClicked(object sender, EventArgs args)
        {
            DateTime nowDate = new DateTime(year,month,day,DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second);


            var previewDate = nowDate.AddDays(-1);
            
            if(previewDate.Month!=nowDate.Month)
            {
                previewDate = nowDate;
            }

            datelabel.Text = previewDate.ToString("dd-MM-yyyy");

            year = previewDate.Year;
            month = previewDate.Month;
            day = previewDate.Day;
            
            selDate = previewDate;
              


        }
        //Next day
        public void NextButtonClicked(object sender, EventArgs args)
        {

            DateTime nowDate = new DateTime(year, month, day ,DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            var nextDate = nowDate.AddDays(+1);
            if (nextDate.Month != nowDate.Month)
            {
                nextDate = nowDate;
            }

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
            expense.Spent = decimal.Parse(Amount_Entry.Text);           
            expense.Details = Desc_Entry.Text;
            //BudgetManager.Update_ExpenseDetails(expense.Spent,_budgetFile);
            expense.Date = selDate;
            await App.Database.SaveExpenseAsync(expense);
            DateTime dt = DateTime.Today;

            string thisMonth = dt.ToString("MMMM");
            var budgetlist = await App.Database.GetBudgetByMonth(thisMonth);

            double BudgetExpense =  App.Database.SumExpenseAsync();
            double TotalBudget = budgetlist[0].BudgetAmount;
            double Totalbalance = TotalBudget - BudgetExpense;

            await App.Database.UpdateBudgetSpentAndBalance(thisMonth, BudgetExpense, Totalbalance);

            var category = await App.Database.GetCategoryByName(Category_Name.Text);
            double CExpense = App.Database.SumOfExpenseByCategoriesAsync(Category_Name.Text);
            double CBudget = category[0].Budget;
            double Cbalance = CBudget - CExpense;

            await App.Database.UpdateCategoriesSpentAndBalance(Category_Name.Text, CExpense, Cbalance);
            //Save.IsEnabled = false;

            Amount_Entry.Text = Desc_Entry.Text = null;
            await Navigation.PopAsync();
           

        }

         private async  void Cancel_Clicked(object sender, EventArgs e)
          {
            Amount_Entry.Text = Desc_Entry.Text = null;
            await Navigation.PopAsync();
        }

       

    }
}

