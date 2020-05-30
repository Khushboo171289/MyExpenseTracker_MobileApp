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

    public partial class AddExpensePage : ContentPage
    {

        public string selectedDate;
        public int year;
        public int month;
        public int day;
        public bool load = false;
        public string Category_Text;
        public string Category_ImageSource;
        public AddExpensePage()
        {
            InitializeComponent();
            selectedDate = DateTime.Now.ToString("dd-MM-yyyy");
            datelabel.Text = selectedDate;
            Category_Text = "food";
            Category_ImageSource = "food.png";
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;
        }

        protected override void OnAppearing()
        {
            var _filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "select_category.txt");

            if (load == true)
            {
                var text = File.ReadAllText(_filename);
                Category_Text = text;

                Category_ImageSource = $"{Category_Text}.png";
                Category_image.Source = Category_ImageSource;
                CategoryName.Text = Category_Text;

               
            }


            load = false;

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            DateTime nowDate = new DateTime(year, month, day);

            var nextDate = nowDate.AddDays(+1);

            datelabel.Text = nextDate.ToString("dd-MM-yyyy");

            year = nextDate.Year;
            month = nextDate.Month;
            day = nextDate.Day;

        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            DateTime nowDate = new DateTime(year, month, day);

            var previewDate = nowDate.AddDays(-1);

            datelabel.Text = previewDate.ToString("dd-MM-yyyy");

            year = previewDate.Year;
            month = previewDate.Month;
            day = previewDate.Day;


        }

        private async void Category_image_Clicked(object sender, EventArgs e)
        {
            load = true;
            await Navigation.PushModalAsync(new Expenseform_CategoryView());
        }
    }
}
