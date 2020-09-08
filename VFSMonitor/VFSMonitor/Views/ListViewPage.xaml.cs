using System;
using VFSMonitor.Models;
using VFSMonitor.ModelViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VFSMonitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        private ListViewModel model;
        private string UserId;

        public ListViewPage(string userId)
        {
            InitializeComponent();
            UserId = userId;
            model = new ListViewModel(userId);
            BindingContext = model;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new DetailsPage(e.Item as Session));
            //_viewModel.GoToDetails(e.Item as Session);
        }

        private void Average_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AveragePage(UserId));
        }

        private void Export_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Zapisz Dane", model.SaveToCSVControl(), "OK");
        }
    }
}