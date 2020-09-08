using System;
using System.ComponentModel;
using VFSMonitor.Views;
using Xamarin.Forms;

namespace VFSMonitor
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Global_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AveragePage("all"));
        }

        private void Users_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UsersListView());
        }

        private void Sessions_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListViewPage("all"));
        }
    }
}
