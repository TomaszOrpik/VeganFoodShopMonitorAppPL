using Xamarin.Forms;

namespace VFSMonitor
{
    public partial class App : Application
    {
        public static string url = "https://desolate-harbor-68661.herokuapp.com/";
        public static string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Green
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
