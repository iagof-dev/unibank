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
    public partial class DEVmenu : ContentPage
    {
        string db_dados = "server=" + Properties.Resources.db_server 
            + ";uid=" + Properties.Resources.db_user + ";" + "pwd=" + Properties.Resources.db_pass + ";database=" + 
            Properties.Resources.db_name;

        string db_table = Properties.Resources.db_tabela;

        string db_database = Properties.Resources.db_name;



        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MainPage();
            return true;
        }

        public DEVmenu()
        {
            InitializeComponent();
            loader();
        }

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

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (reg_user.Text == null || reg_email.Text == null || reg_senha.Text == null || reg_cargo.SelectedIndex == -1)
            {
                DisplayAlert("Dev Menu", "Há espaços para inserir informações vazias \nCorrija-os", "Ok");
            }
            else
            {
                cadastro();
            }
        }
        async void cadastro()
        {
            bool cad_resp = await DisplayAlert("Deseja Cadastrar?", "\nUsuario= " + reg_user.Text + "\nSenha= " + reg_senha.Text + "\nE-mail: " + reg_email.Text, "Não", "Sim");
            if (cad_resp == false)
            {
                //DisplayAlert("Dev", "Você aceitou", "Ok");
                user_registro();
            }
            else
            {
                DisplayAlert("Dev Menu", "Operação cancelada.", "Ok");
            }
        }


        private void reg_busca_Clicked(object sender, EventArgs e)
        {
            if (sea_id.Text == "0" || sea_id.Text == null)
            {
                DisplayAlert("Dev Menu", "ID Invalido!", "Corrigir");
            }
            else {
                db.user_search(Convert.ToInt16(sea_id.Text));
                db_search.IsVisible = true;
                sea_user.Text = "Nome de Usuario: " + db.sea_nome;
                sea_email.Text = "Email: " + db.sea_email;
                sea_pass.Text = "Senha: " + db.sea_senha;
                sea_cargo.Text = "Cargo: " + db.sea_cargo;
                sea_saldo.Text = "Saldo: R$" + db.sea_saldo;
                sea_sempfp.Text = "Sem PFP: " + db.sea_sempfp;
                sea_pfp.Source = ImageSource.FromStream(() => new MemoryStream(db.foto));

            }
        }
        public void user_registro()
        {
            var con = new MySqlConnection(db_dados);
            try
            {
                
                con.Open();
                var com = con.CreateCommand();
                com.CommandText = "use " + db_database + "; insert into " + db_table + " values (DEFAULT, '" + reg_user.Text + "', '" + reg_email.Text + "', '" + reg_senha.Text + "', NULL, '" + reg_cargo.SelectedItem + "', DEFAULT, DEFAULT);";
                var leitor = com.ExecuteReader();
                DisplayAlert("Dev Menu", "Usuário registrado com sucesso!", "Ok");
                
            }
            catch (Exception e)
            {
                DisplayAlert("Erro!", "" + e, "Ok");
            }
            con.Close();
        }

        public void user_addsld()
        {

        }

        private void user_addfinal_Clicked(object sender, EventArgs e)
        {
            int adic_id = Convert.ToInt32(user_idadd.Text);
            double adic_valor = Convert.ToDouble(user_qntadd.Text);

            db.add_valor(adic_id, adic_valor);

            DisplayAlert("UniBank", "Saldo adicionado!", "OK");
        }

        private void user_pfpmenu_Clicked(object sender, EventArgs e)
        {

        }
    }
}