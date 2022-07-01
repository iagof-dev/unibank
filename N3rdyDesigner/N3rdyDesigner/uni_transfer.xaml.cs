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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class uni_transfer : ContentPage
    {
        public void loader()
        {
            if (db.pass_sempfp == "1")
            {
                user_pfpmenu.Source = "nopfp";
            }
            else
            {
                user_pfpmenu.Source = ImageSource.FromStream(() => new MemoryStream(db.pass_foto));
            }
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MainPage();
            return true;
        }
        public uni_transfer()
        {
            InitializeComponent();
            loader();
        }

        private async void user_transfer_Clicked(object sender, EventArgs e)
        {
            if (user_trid.Text == null | user_trid.Text == "" | user_qntd.Text == null | user_qntd.Text == "" | user_qntd.Text == "0") {

                DisplayAlert("UniBank", "Há espaços para inserir informações vazias \nCorrija-os", "Ok");            
            
            }
            else { 
            bool cad_resp = await DisplayAlert("UniBank", "Você estará transferindo R$ " + user_qntd.Text + " para a conta com identificação (" + user_trid.Text + ") \nDeseja Prosseguir?", "Cancelar", "Sim");
            if (cad_resp == false)
            {
                    int target_id = Convert.ToInt32(user_trid.Text);
                    double target_qtd = Convert.ToDouble(user_qntd.Text);
                    //DisplayAlert("Dev", "Você aceitou", "Ok");
                    if (db.pass_saldo < target_qtd)
                {
                    DisplayAlert("Unibank", "Erro! Sem Saldo na Conta", "Ok");
                }
                else {
                        //operação para transferir
                        


                        db.user_transfer(target_id, target_qtd);

                        DisplayAlert("UniBank", "Transferêrencia concluida!", "Ok");
                }
            }
            else
            {
                DisplayAlert("UniBank", "Operação cancelada.", "Ok");
            }
            }
        }


        private void user_pfpmenu_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}