using VFSMonitor.Models;
using VFSMonitor.ModelViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VFSMonitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersListView : ContentPage
    {
        public UsersListView()
        {
            InitializeComponent();
            BindingContext = new ListViewModel("all");
        }

        private void sessionsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Session ss = e.Item as Session;
            Navigation.PushAsync(new ListViewPage(ss.UserId));
        }
    }
}