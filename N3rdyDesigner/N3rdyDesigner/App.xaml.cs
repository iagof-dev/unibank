using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace N3rdyDesigner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new Loading();

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
