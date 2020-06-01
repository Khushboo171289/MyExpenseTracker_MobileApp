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
        public ObservableCollection<Categories> categories_oc { get; set; }
        public string _budgetFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "budget.txt");
        public CategoriesPage()
        {
            InitializeComponent();
            categories_oc = new ObservableCollection<Categories>();
            CategoryManager.GetAllCategories(categories_oc);
            listView.ItemsSource = categories_oc;

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
    }
}