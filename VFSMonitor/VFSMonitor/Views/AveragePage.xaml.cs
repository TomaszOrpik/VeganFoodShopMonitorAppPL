using System;
using VFSMonitor.ModelViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VFSMonitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AveragePage : ContentPage
    {
        private AverageViewModel Model;
        
        public AveragePage(string userId)
        {
            InitializeComponent();
            Model = new AverageViewModel(userId);
            BindingContext = Model;
        }

        private void Export_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Eksportuj jako Excel", Model.SaveToExcelControl(), "OK");
        }
    }
}