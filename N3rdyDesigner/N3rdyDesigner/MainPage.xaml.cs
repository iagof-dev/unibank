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


        public static string db_server = db.db_server;
        public static string db_user = db.db_user;
        public static string db_pass = db.db_pass;
        public static string db_table = db.db_table;
        public static string db_database = db.db_database;



        public static string db_dados = "server=" + db_server + ";uid=" + db_user + ";" + "pwd=" + db_pass + ";database=" + db_database;


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
                user_profile.Source = "https://cdn.discordapp.com/attachments/889233196091342920/1034143592974913636/icon.png";
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
                string set_saldo = "R$" + db.pass_saldo;
                set_saldo = set_saldo.Replace('.', ',');

                user_saldo.Text = set_saldo;

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
