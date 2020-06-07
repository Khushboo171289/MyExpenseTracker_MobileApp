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
        string prev_ftext;
        string prev_Htext;
        string prev_Atext;
        string prev_Hthtext;
        string prev_Edutext;
        string prev_Enttext;


        public BudgetPage()
        {
            InitializeComponent();

            DateTime dt = DateTime.Today;

            string thisMonth = dt.ToString("MMMM");

            Month.Text = thisMonth;


        }


        protected async override void OnAppearing()
        {

           



            var budgetslist = await App.Database.GetBudgetByMonth(Month.Text);

            if (budgetslist.Count == 0)
            {
                await DisplayAlert("Alert", "Please Enter New Budget for this month", "Ok");
                Budget budget = new Budget();
                budget.Month = Month.Text;
                budget.Balance = 0;
                budget.BudgetAmount = 0;
                budget.Expense = 0;
                await App.Database.SaveBudgetAsync(budget);

                create_category(0, "Food");
                create_category(0, "Home");
                create_category(0, "Auto");
                create_category(0, "Health");
                create_category(0, "Education");
                create_category(0, "Entertainment");
                set_category_buttons(true);



            }

            if (budgetslist.Count > 0)
            {
                setButton.IsEnabled = false;
                budgetEntry.IsEnabled = false;
                budgetEntry.Text = budgetslist[0].BudgetAmount.ToString();
                set_category_buttons(false);


                set_percent_amount();

            }
        }




        private async void OnSetButtonClicked(object sender, EventArgs e)
        {



            set_category_buttons(true);
            Budget budget = new Budget();
            budget.Month = Month.Text;


            if (string.IsNullOrWhiteSpace(budgetEntry.Text))
            {
                await DisplayAlert("Invalid Input", "Please enter valid amount", "Ok");
                return;


            }


            budget.BudgetAmount = double.Parse(budgetEntry.Text);


            if (budget.BudgetAmount == 0)
            {
                await DisplayAlert("Invalid Input", "Please enter valid amount", "Ok");
                return;
            }


            TE = App.Database.SumExpenseAsync();
            budget.Expense = TE;
            budget.Balance = budget.BudgetAmount - budget.Expense;
            await App.Database.UpdateBudgetAmount(Month.Text, budget.BudgetAmount);
            await App.Database.UpdateBudgetSpentAndBalance(Month.Text, budget.Expense, budget.Balance);
            Manage_Categories();



            setButton.IsEnabled = false;



        }


        private void OnResetButtonClicked(object sender, EventArgs e)
        {
            budgetEntry.Text = string.Empty;
            budgetEntry.IsEnabled = true;
            prev_ftext = FoodPercentage.Text;
            prev_Htext = HomePercentage.Text;
            prev_Atext = AutoPercentage.Text;
            prev_Hthtext = HealthPercentage.Text;
            prev_Edutext = EducationPercentage.Text;
            prev_Enttext = EntertainmentPercentage.Text;


            FoodPercentage.Text = HomePercentage.Text = AutoPercentage.Text = HealthPercentage.Text = EducationPercentage.Text = EntertainmentPercentage.Text = string.Empty;




            set_category_buttons(true);
            setButton.IsEnabled = true;


        }

        private async void Manage_Categories()
        {


            var budgetslist = await App.Database.GetBudgetByMonth(Month.Text);
            if (budgetslist.Count == 0)
            {
                await DisplayAlert("Alert", "Please Set the Budget for the Month", "OK");
                return;
            }





            if (Check_For_ChangeinEntry(FoodPercentage.Text, "Food"))
            {
                double fp = (double.Parse(FoodPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
                create_category(fp, "Food");
                FoodPercentage.Text = fp.ToString();
            }
            else
            {
                FoodPercentage.Text = prev_ftext;
            }



            if (Check_For_ChangeinEntry(HomePercentage.Text, "Home"))
            {
                double hp = (double.Parse(HomePercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
                create_category(hp, "Home");
                HomePercentage.Text = hp.ToString();
            }
            else
            {
                HomePercentage.Text = prev_Htext;
            }



            if (Check_For_ChangeinEntry(AutoPercentage.Text, "Auto"))
            {


                double ap = (double.Parse(AutoPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
                create_category(ap, "Auto");
                AutoPercentage.Text = ap.ToString();
            }
            else
            {
                AutoPercentage.Text = prev_Atext;
            }



            if (Check_For_ChangeinEntry(HealthPercentage.Text, "Health"))
            {


                double hep = (double.Parse(HealthPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;


                create_category(hep, "Health");


                HealthPercentage.Text = hep.ToString();
            }
            else
            {
                HealthPercentage.Text = prev_Hthtext;
            }


            if (Check_For_ChangeinEntry(EducationPercentage.Text, "Education"))
            {


                double edu = (double.Parse(EducationPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
                create_category(edu, "Education");
                EducationPercentage.Text = edu.ToString();
            }
            else
            {
                EducationPercentage.Text = prev_Edutext;

            }
            if (Check_For_ChangeinEntry(EntertainmentPercentage.Text, "Entertainment"))
            {


                double ent = (double.Parse(EntertainmentPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
                create_category(ent, "Entertainment");
                EntertainmentPercentage.Text = ent.ToString();
            }
            else
            {
                EntertainmentPercentage.Text = prev_Enttext;
            }




            set_category_buttons(false);


        }


        private async void create_category(double percent_amount, string Cname)
        {
            List<Categories> categories = new List<Categories>();


            categories = await App.Database.GetCategoriesAsync();

            if (categories.Count == 0 || categories[0].Month != Month.Text)

            {
                var category = new Categories();
                category.Name = Cname;
                category.Balance = 0.0;
                category.Spent = 0;
                category.Budget = 0;
                category.IconSource = $"{Cname}.png";
                category.Month = Month.Text;
                await App.Database.SaveCategoryAsync(category);


            }
            else
            {
                var category = new Categories();


                category = App.Database.GetSpecificCategory(Cname);


                await App.Database.UpdateCategoriesBudgetAmount(Cname, percent_amount);


            }



        }
        private void set_category_buttons(bool value)
        {
            FoodPercentage.IsEnabled = value;
            HomePercentage.IsEnabled = value;
            AutoPercentage.IsEnabled = value;
            HealthPercentage.IsEnabled = value;
            EducationPercentage.IsEnabled = value;
            EntertainmentPercentage.IsEnabled = value;
        }


        private void set_percent_amount()
        {
            Categories category = new Categories();
            category = App.Database.GetSpecificCategory("Food");
            FoodPercentage.Text = category.Budget.ToString();
            category = App.Database.GetSpecificCategory("Home");
            HomePercentage.Text = category.Budget.ToString();
            category = App.Database.GetSpecificCategory("Auto");
            AutoPercentage.Text = category.Budget.ToString();
            category = App.Database.GetSpecificCategory("Health");
            HealthPercentage.Text = category.Budget.ToString();
            category = App.Database.GetSpecificCategory("Education");
            EducationPercentage.Text = category.Budget.ToString();
            category = App.Database.GetSpecificCategory("Entertainment");
            EntertainmentPercentage.Text = category.Budget.ToString();
        }

        private bool Check_For_ChangeinEntry(string val, string category_name)
        {
            Categories category = App.Database.GetSpecificCategory(category_name);
            if (!string.IsNullOrWhiteSpace(val))
            {
                if (double.Parse(val) != category.Budget)
                {
                    return true;
                }
            }


            return false;
        }
    }





}




        //        if (budgetslist.Count > 0)
        //    {
        //        setButton.IsEnabled = false;
        //        budgetEntry.IsEnabled = false;
        //        budgetEntry.Text = budgetslist[0].BudgetAmount.ToString();
        //    }

        //    var food = await App.Database.GetCategoryByName("Food");
        //    if (food.Count > 0)
        //    {
        //        FoodPercentage.Text = food[0].Budget.ToString();
        //        FoodPercentage.IsEnabled = false;
        //    }

        //    var home = await App.Database.GetCategoryByName("Home");
        //    if (home.Count > 0)
        //    {
        //        HomePercentage.Text = home[0].Budget.ToString();
        //        HomePercentage.IsEnabled = false;
        //    }

        //    var Auto = await App.Database.GetCategoryByName("Auto");
        //    if (Auto.Count > 0)
        //    {
        //        AutoPercentage.Text = Auto[0].Budget.ToString();
        //        AutoPercentage.IsEnabled = false;
        //    }

        //    var Health = await App.Database.GetCategoryByName("Health");
        //    if (Health.Count > 0)
        //    {
        //        HealthPercentage.Text = Health[0].Budget.ToString();
        //        HealthPercentage.IsEnabled = false;
        //    }

        //    var Education = await App.Database.GetCategoryByName("Education");
        //    if (Education.Count > 0)
        //    {
        //        EducationPercentage.Text = Education[0].Budget.ToString();
        //        EducationPercentage.IsEnabled = false;
        //    }

        //    var Entertainment = await App.Database.GetCategoryByName("Entertainment");
        //    if (Entertainment.Count > 0)
        //    {
        //        EntertainmentPercentage.Text = Entertainment[0].Budget.ToString();
        //        EntertainmentPercentage.IsEnabled = false;
        //    }





        //}

        //private async void OnSetButtonClicked(object sender, EventArgs e)
        //{
        //    //CategoryManager.GetAllCategories();
        //    //var expenselist = await App.Database.GetExpensesAsync();
        //    //TE = 0;
        //    //expenselist.ForEach(i => TE = TE + i.Spent);

        //    TE =  App.Database.SumExpenseAsync();



        //    Budget budget = new Budget();
        //    budget.Month = Month.Text;
        //    budget.BudgetAmount = double.Parse(budgetEntry.Text);
        //    budget.Balance = (double.Parse(budgetEntry.Text)) - TE;
        //    budget.Expense = TE;
        //    await App.Database.SaveBudgetAsync(budget);


        //    setButton.IsEnabled = false;


        //}

        //private async void OnResetButtonClicked(object sender, EventArgs e)
        //{
        //    budgetEntry.Text = string.Empty;
        //    budgetEntry.IsEnabled = true;
        //    FoodPercentage.Text = HomePercentage.Text = AutoPercentage.Text = HealthPercentage.Text = EducationPercentage.Text = EntertainmentPercentage.Text = string.Empty;


        //    var budgetslist = await App.Database.GetBudgetByMonth(Month.Text);

        //    if (budgetslist.Count > 0)
        //    {
        //        foreach (var item in budgetslist)
        //        {
        //            await App.Database.DeleteBudgetAsync(item);
        //        }

        //    }

        //    categories = await App.Database.GetCategoriesAsync();
        //    foreach (var item in categories)
        //    {
        //        await App.Database.DeleteCategoryAsync(item);
        //    }

        //    setButton.IsEnabled = true;

        //}

        //private async void setPerButton_Clicked(object sender, EventArgs e)
        //{
        //    double fp = (double.Parse(FoodPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
        //    double hp = (double.Parse(HomePercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
        //    double ap = (double.Parse(AutoPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
        //    double hep = (double.Parse(HealthPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
        //    double edp = (double.Parse(EducationPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;
        //    double etrp = (double.Parse(EntertainmentPercentage.Text) * (double.Parse(budgetEntry.Text))) / 100;

        //    List<Categories> categories = new List<Categories>();
        //    categories = await App.Database.GetCategoriesAsync();

        //    var spent_f = App.Database.SumOfExpenseByCategoriesAsync("Food");
        //    var spent_h = App.Database.SumOfExpenseByCategoriesAsync("Home");
        //    var spent_A = App.Database.SumOfExpenseByCategoriesAsync("Auto");
        //    var spent_he = App.Database.SumOfExpenseByCategoriesAsync("Health");
        //    var spent_E = App.Database.SumOfExpenseByCategoriesAsync("Education");
        //    var spent_En = App.Database.SumOfExpenseByCategoriesAsync("Entertainment");

        //    foreach (var item in categories)
        //    {
        //        await App.Database.DeleteCategoryAsync(item);
        //    }

        //    var category1 = new Categories();
        //    var category2 = new Categories();
        //    var category3 = new Categories();
        //    var category4 = new Categories();
        //    var category5 = new Categories();
        //    var category6 = new Categories();

        //    category1.Name = "Home";
        //    category1.Month = Month.Text;
        //    category1.Balance = hp - spent_h;
        //    category1.Spent = spent_h;
        //    category1.Budget = hp;
        //    category1.IconSource = "home.png";
        //    await App.Database.SaveCategoryAsync(category1);

        //    category2.Name = "Food";
        //    category2.Month = Month.Text;
        //    category2.Balance = fp - spent_f;
        //    category2.Spent = spent_f;
        //    category2.Budget = fp;
        //    category2.IconSource = "food.png";
        //    await App.Database.SaveCategoryAsync(category2);

        //    category3.Name = "Health";
        //    category3.Month = Month.Text;
        //    category3.Balance = hep - spent_he;
        //    category3.Spent = spent_he;
        //    category3.Budget = hep;
        //    category3.IconSource = "health.png";
        //    await App.Database.SaveCategoryAsync(category3);

        //    category4.Name = "Auto";
        //    category4.Month = Month.Text;
        //    category4.Balance = ap - spent_A;
        //    category4.Spent = spent_A;
        //    category4.Budget = ap;
        //    category4.IconSource = "auto.png";
        //    await App.Database.SaveCategoryAsync(category4);

        //    category5.Name = "Education";
        //    category5.Month = Month.Text;
        //    category5.Balance = edp - spent_E;
        //    category5.Spent = spent_E;
        //    category5.Budget = edp;
        //    category5.IconSource = "education.png";
        //    await App.Database.SaveCategoryAsync(category5);

        //    category6.Name = "Entertainment";
        //    category6.Month = Month.Text;
        //    category6.Balance = etrp - spent_En;
        //    category6.Spent = spent_En;
        //    category6.Budget = etrp;
        //    category6.IconSource = "entertainment.png";
        //    await App.Database.SaveCategoryAsync(category6);


        //    FoodPercentage.IsEnabled = false;
        //    HomePercentage.IsEnabled = false;
        //    AutoPercentage.IsEnabled = false;
        //    HealthPercentage.IsEnabled = false;
        //    EducationPercentage.IsEnabled = false;
        //    EntertainmentPercentage.IsEnabled = false;

        //    //await App.Database.UpdateCategoriesBudget("Food", fp);
        //    ////var food = await App.Database.GetCategoryByName("Food");
        //    ////await App.Database.SaveCategoryAsync(food[0]);
        //    //await App.Database.UpdateCategoriesBudget("Home", hp);
        //    //await App.Database.UpdateCategoriesBudget("Auto", ap);
        //    //await App.Database.UpdateCategoriesBudget("Health", hep);
        //    //await App.Database.UpdateCategoriesBudget("Education", edp);
        //    //await App.Database.UpdateCategoriesBudget("Entertainment", etrp);

        //    //categories = await App.Database.GetCategoriesAsync();
        //    //foreach (var item in categories)
        //    //{
        //    //    await App.Database.SaveCategoryAsync(item);
        //    //}
        //}
