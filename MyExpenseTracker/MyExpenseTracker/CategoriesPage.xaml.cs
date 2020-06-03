using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


    public partial class CategoriesPage : ContentPage
    {
       
        public string _budgetFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "budget.txt");
        public CategoriesPage()
        {
            InitializeComponent();
           

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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            HomeListview.ItemsSource = await App.Database.GetExpenseByCategory("Home");
            FoodListview.ItemsSource = await App.Database.GetExpenseByCategory("Food");
            HealthListview.ItemsSource = await App.Database.GetExpenseByCategory("Health");
            AutoListview.ItemsSource = await App.Database.GetExpenseByCategory("Auto");
            EduListview.ItemsSource = await App.Database.GetExpenseByCategory("Education");
            EntrListview.ItemsSource = await App.Database.GetExpenseByCategory("Entertainment");
            HomeListview.IsVisible = false;
            FoodListview.IsVisible = false;
            HealthListview.IsVisible = false;
            AutoListview.IsVisible = false;
            EduListview.IsVisible = false;
            EntrListview.IsVisible = false;

        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            HomeListview.IsVisible = !HomeListview.IsVisible;
        }

        private void Food_Clicked(object sender, EventArgs e)
        {
            FoodListview.IsVisible = !FoodListview.IsVisible;
        }

        private void Health_Clicked(object sender, EventArgs e)
        {
            HealthListview.IsVisible = !HealthListview.IsVisible;
        }

        private void Auto_Clicked(object sender, EventArgs e)
        {
            AutoListview.IsVisible = !AutoListview.IsVisible;
        }

        private void Edu_Clicked(object sender, EventArgs e)
        {
            EduListview.IsVisible = !EduListview.IsVisible;
        }

        private void Entr_Clicked(object sender, EventArgs e)
        {
            EntrListview.IsVisible = !EntrListview.IsVisible;
        }
    }
}
    