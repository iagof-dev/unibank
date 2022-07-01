using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySqlConnector;
using System.IO;

namespace N3rdyDesigner
{
    public partial class MainPage : ContentPage
    {
        bool ver_saldo = false;

        string db_dados = "server=" + Properties.Resources.db_server
            + ";uid=" + Properties.Resources.db_user + ";" + "pwd=" + Properties.Resources.db_pass + ";database=" +
            Properties.Resources.db_name;

        string db_table = Properties.Resources.db_tabela;

        string db_database = Properties.Resources.db_name;

        bool sair = false;

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new Loading();
            return true;

        }

        async void aquit()
        {
            bool user_sair = await DisplayAlert("UniBank", "Deseja Sair de Sua Conta?", "Não", "Sim");
            if (user_sair == true)
            {
                App.Current.MainPage = new Loading();
                sair = true;
            }
            else
            {
                sair = false;
            }
        }
        public async void user_quit()
        {
            bool user_sair = await DisplayAlert("UniBank", "Deseja Sair de Sua Conta?", "Não", "Sim");
            if (user_sair == true)
            {
                sair = true;
            }
            else
            {

            }

        }

        public MainPage()
        {
            InitializeComponent();
            loader();

        }
        public void loader()
        {
            if (db.pass_sempfp == "1")
            {
                user_profile.Source = "nopfp";
            }
            else
            {
                user_profile.Source = ImageSource.FromStream(() => new MemoryStream(db.pass_foto));
            }

            
            if (db.pass_cargo == "dev")
            {
                devmenu.IsVisible = true;
            }
            else
            {
                devmenu.IsVisible = false;
            }
        }
        private void user_versaldo_Clicked(object sender, EventArgs e)
        {
            if (ver_saldo == false) {
                //Mostrando Saldo
                user_saldo.Text = "R$" + db.pass_saldo;
                user_versaldo.Source = "saldo";
                ver_saldo = true;
            }
            else
            {
                //Saldo Oculto
                user_saldo.Text = "R$**,**";
                user_versaldo.Source = "saldo1";
                ver_saldo = false;
            }
        }

        private void user_profile_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new user_page();
        }

        private void DEVMENU_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new DEVmenu();
        }

        private void menu_transfer_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new uni_transfer();
        }
    }
}
