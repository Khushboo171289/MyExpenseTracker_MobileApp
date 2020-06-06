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
        public double TE;
        public List<Categories> categories;


        public BudgetPage()
        {
            InitializeComponent();

            DateTime dt = DateTime.Today;

            string thisMonth = dt.ToString("MMMM");

            Month.Text = thisMonth;


        }


        protected async override void OnAppearing()
        {

            //  CategoryListView.ItemsSource = await App.Database.GetCategoriesAsync();

            var budgetslist = await App.Database.GetBudgetByMonth(Month.Text);
            if (budgetslist.Count > 0)
            {
                setButton.IsEnabled = false;
                budgetEntry.IsEnabled = false;
                budgetEntry.Text = budgetslist[0].BudgetAmount.ToString();
            }

            var food = await App.Database.GetCategoryByName("Food");
            if (food.Count > 0)
            {
                FoodPercentage.Text = food[0].Budget.ToString();
                FoodPercentage.IsEnabled = false;
            }

            var home = await App.Database.GetCategoryByName("Home");
            if (home.Count > 0)
            {
                HomePercentage.Text = home[0].Budget.ToString();
                HomePercentage.IsEnabled = false;
            }

            var Auto = await App.Database.GetCategoryByName("Auto");
            if (Auto.Count > 0)
            {
                AutoPercentage.Text = Auto[0].Budget.ToString();
                AutoPercentage.IsEnabled = false;
            }

            var Health = await App.Database.GetCategoryByName("Health");
            if (Health.Count > 0)
            {
                HealthPercentage.Text = Health[0].Budget.ToString();
                HealthPercentage.IsEnabled = false;
            }

            var Education = await App.Database.GetCategoryByName("Education");
            if (Education.Count > 0)
            {
                EducationPercentage.Text = Education[0].Budget.ToString();
                EducationPercentage.IsEnabled = false;
            }

            var Entertainment = await App.Database.GetCategoryByName("Entertainment");
            if (Entertainment.Count > 0)
            {
                EntertainmentPercentage.Text = Entertainment[0].Budget.ToString();
                EntertainmentPercentage.IsEnabled = false;
            }





        }

        private async void OnSetButtonClicked(object sender, EventArgs e)
        {
            //CategoryManager.GetAllCategories();
            //var expenselist = await App.Database.GetExpensesAsync();
            //TE = 0;
            //expenselist.ForEach(i => TE = TE + i.Spent);

            TE =  App.Database.SumExpenseAsync();



            Budget budget = new Budget();
            budget.Month = Month.Text;
            budget.BudgetAmount = double.Parse(budgetEntry.Text);
            budget.Balance = (double.Parse(budgetEntry.Text)) - TE;
            budget.Expense = TE;
            await App.Database.SaveBudgetAsync(budget);


            setButton.IsEnabled = false;


        }

        private async void OnResetButtonClicked(object sender, EventArgs e)
        {
            budgetEntry.Text = string.Empty;
            budgetEntry.IsEnabled = true;
            FoodPercentage.Text = HomePercentage.Text = AutoPercentage.Text = HealthPercentage.Text = EducationPercentage.Text = EntertainmentPercentage.Text = string.Empty;


            var budgetslist = await App.Database.GetBudgetByMonth(Month.Text);

            if (budgetslist.Count > 0)
            {
                foreach (var item in budgetslist)
                {
                    await App.Database.DeleteBudgetAsync(item);
                }

            }

            categories = await App.Database.GetCategoriesAsync();
            foreach (var item in categories)
            {
                await App.Database.DeleteCategoryAsync(item);
            }

            setButton.IsEnabled = true;

        }

        private async void setPerButton_Clicked(object sender, EventArgs e)
        {
            double fp = (double.Parse(FoodPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
            double hp = (double.Parse(HomePercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
            double ap = (double.Parse(AutoPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
            double hep = (double.Parse(HealthPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
            double edp = (double.Parse(EducationPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
            double etrp = (double.Parse(EntertainmentPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;

            List<Categories> categories = new List<Categories>();
            categories = await App.Database.GetCategoriesAsync();

            var spent_f = App.Database.SumOfExpenseByCategoriesAsync("Food");
            var spent_h = App.Database.SumOfExpenseByCategoriesAsync("Home");
            var spent_A = App.Database.SumOfExpenseByCategoriesAsync("Auto");
            var spent_he = App.Database.SumOfExpenseByCategoriesAsync("Health");
            var spent_E = App.Database.SumOfExpenseByCategoriesAsync("Education");
            var spent_En = App.Database.SumOfExpenseByCategoriesAsync("Entertainment");

            foreach (var item in categories)
            {
                await App.Database.DeleteCategoryAsync(item);
            }

            var category1 = new Categories();
            var category2 = new Categories();
            var category3 = new Categories();
            var category4 = new Categories();
            var category5 = new Categories();
            var category6 = new Categories();

            category1.Name = "Home";
            category1.Balance = hp - spent_h;
            category1.Spent = spent_h;
            category1.Budget = hp;
            category1.IconSource = "home.png";
            await App.Database.SaveCategoryAsync(category1);

            category2.Name = "Food";
            category2.Balance = fp - spent_f;
            category2.Spent = spent_f;
            category2.Budget = fp;
            category2.IconSource = "food.png";
            await App.Database.SaveCategoryAsync(category2);

            category3.Name = "Health";
            category3.Balance = hep - spent_he;
            category3.Spent = spent_he;
            category3.Budget = hep;
            category3.IconSource = "health.png";
            await App.Database.SaveCategoryAsync(category3);

            category4.Name = "Auto";
            category4.Balance = ap - spent_A;
            category4.Spent = spent_A;
            category4.Budget = ap;
            category4.IconSource = "auto.png";
            await App.Database.SaveCategoryAsync(category4);

            category5.Name = "Education";
            category5.Balance = edp - spent_E;
            category5.Spent = spent_E;
            category5.Budget = edp;
            category5.IconSource = "education.png";
            await App.Database.SaveCategoryAsync(category5);

            category6.Name = "Entertainment";
            category6.Balance = etrp - spent_En;
            category6.Spent = spent_En;
            category6.Budget = etrp;
            category6.IconSource = "entertainment.png";
            await App.Database.SaveCategoryAsync(category6);


            FoodPercentage.IsEnabled = false;
            HomePercentage.IsEnabled = false;
            AutoPercentage.IsEnabled = false;
            HealthPercentage.IsEnabled = false;
            EducationPercentage.IsEnabled = false;
            EntertainmentPercentage.IsEnabled = false;

            //await App.Database.UpdateCategoriesBudget("Food", fp);
            ////var food = await App.Database.GetCategoryByName("Food");
            ////await App.Database.SaveCategoryAsync(food[0]);
            //await App.Database.UpdateCategoriesBudget("Home", hp);
            //await App.Database.UpdateCategoriesBudget("Auto", ap);
            //await App.Database.UpdateCategoriesBudget("Health", hep);
            //await App.Database.UpdateCategoriesBudget("Education", edp);
            //await App.Database.UpdateCategoriesBudget("Entertainment", etrp);

            //categories = await App.Database.GetCategoriesAsync();
            //foreach (var item in categories)
            //{
            //    await App.Database.SaveCategoryAsync(item);
            //}
        }
    }
}