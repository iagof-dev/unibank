using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;

namespace N3rdyDesigner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loading : ContentPage
    {



        public Loading()
        {
            InitializeComponent();

        }


        private async void logar_Clicked_1(object sender, EventArgs e)
        {
            if (login_user.Text == null | senha_user.Text == null | login_user.Text == "" | senha_user.Text == "")
            {
                DisplayAlert("UniBank", "Erro! Há espaços vazios, preencha e tente novamente...", "Tentar Novamente");

            }
            else 
            { 
                loading.IsVisible = true;
                db.login_get(login_user.Text, senha_user.Text);

                if (db.valido == true)
                {
                DisplayAlert("UniBank", "Seja bem vindo, " + db.pass_nome + "!", "Ok");
                App.Current.MainPage = new MainPage();
                }
                else
                {
              
                    DisplayAlert("UniBank", "Erro! Email ou Senha incorreta", "Tentar Novamente");
                }

            }
        }
    }
}