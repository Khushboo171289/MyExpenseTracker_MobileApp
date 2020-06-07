using MyExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace MyExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Expenseform_CategoryView : ContentPage
    {
      
        public string selecteditem;



        public Expenseform_CategoryView()
        {
            InitializeComponent();

        }


        protected async override void OnAppearing()
        {
            listView.ItemsSource = await App.Database.GetCategoriesAsync();

        }






            private void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            var selitem = (Categories)e.Item;
            Selection(selitem);


        }

        private async void Selection(Categories selitem)
        {

           
            var _filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "select_category.txt");
            File.WriteAllText(_filename, selitem.Name);
            
            await Navigation.PopModalAsync();

        }

    }
}

