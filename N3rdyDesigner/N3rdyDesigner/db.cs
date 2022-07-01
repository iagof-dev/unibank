///
///     Projeto - UniBank
///
/// Horas Perdidas no Projeto: +60 horas
/// Data de Inicio: 22/05/2022
/// Data de Finalização: NÃO FINALIZADO
/// 
////////////////////////////////////
///     Feito Por ©N3rdyDzn
///     github.com/n3rdydzn
///         n3rdydzn.xyz
////////////////////////////////////
///











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

    public class db
    {

        ///
        /// Dados do Banco de dados (Flexivel)
        ///
        public static string db_dados = "server=" + Properties.Resources.db_server + ";uid=" + Properties.Resources.db_user + ";" + "pwd=" + Properties.Resources.db_pass + ";database=" + Properties.Resources.db_name;

        public static string db_table = Properties.Resources.db_tabela;

        public static string db_database = Properties.Resources.db_name;

        ///
        /// Informações do Usuário no Banco de Dados
        ///
        public static string pass_nome = string.Empty;
        public static string pass_email = string.Empty;
        public static string pass_senha = string.Empty;
        public static string pass_cargo = string.Empty;
        public static string pass_sempfp = string.Empty;
        public static double pass_saldo = 0;
        public static int pass_id = 0;
        public static byte[] pass_foto = new byte[0];
        

        public static bool valido = false;

        ///
        ///Verificar Email e senha são compativeis
        ///

        public static void login_get(string ut_email, string ut_pass)
        {

            var con = new MySqlConnection(db.db_dados);
            try
            {

                con.Open();
                var com = con.CreateCommand();
                com.CommandText = "use " + db.db_database + "; select * from " + db.db_table + " where email='" + ut_email + "';";
                var reader = com.ExecuteReader();
                while (reader.Read())
                {
                    pass_senha = reader.GetString("senha");
                    if (ut_pass == pass_senha)
                    {
                        db.valido = true;
                        db.pass_email = reader.GetString("email");
                        db.pass_saldo = reader.GetDouble("saldo");
                        db.pass_nome = reader.GetString("usuario");
                        db.pass_sempfp = reader.GetString("sempfp");
                        db.pass_cargo = reader.GetString("cargo");
                        db.pass_id = reader.GetInt32("id");
                        if (db.pass_sempfp == "0")
                        {
                            pass_foto = (byte[])(reader["foto"]);
                        }

                    }
                    else
                    {
                        db.valido = false;
                    }

                }
            }
            catch (Exception b)
            {
                db.valido = false;

            }
            con.Close();

            return;
        }


        ///
        /// procuramento de usuarios
        ///
        public static string sea_nome = string.Empty;
        public static string sea_email = string.Empty;
        public static string sea_senha = string.Empty;
        public static string sea_cargo = string.Empty;
        public static string sea_erro = string.Empty;
        public static string sea_sempfp = string.Empty;
        public static double sea_saldo = 0;
        public static byte[] foto = new byte[0];

        public static void user_search(int us_id)
        {
            var con = new MySqlConnection(db_dados);
            try
            {

                con.Open();
                var com = con.CreateCommand();
                com.CommandText = "use " + db_database + "; select * from " + db_table + " where id='" + us_id + "';";
                var reader = com.ExecuteReader();
                while (reader.Read())
                {
                    db.sea_nome = reader.GetString("usuario");
                    db.sea_email = reader.GetString("email");
                    db.sea_senha = reader.GetString("senha");
                    db.sea_cargo = reader.GetString("cargo");
                    db.sea_saldo = reader.GetDouble("saldo");
                    db.sea_sempfp = reader.GetString("sempfp");

                    //byte[] img = (byte[])(reader["foto"]);
                    //MemoryStream ms = new MemoryStream(img);
                    //foto = ImageSource.FromStream(() => new MemoryStream((byte[])(reader["foto"])));
                    foto = (byte[])(reader["foto"]);
                    //sea_pfp.Source = ImageSource.FromStream(() => new MemoryStream((byte[])(reader["foto"])));
                   
                    //DisplayAlert("Dev Menu", "Um usuário encontrado com ID (" + sea_id.Text + ")!", "Ok");
                    con.Close();
                    //sea_user.Text = "Nome de Usuario: " + nome;
                    //sea_email.Text = "Email: " + email;
                    //sea_pass.Text = "Senha: " + senha;
                    //sea_cargo.Text = "Cargo: " + cargo;
                    //sea_saldo.Text = "Saldo: R$" + saldo;

                }
            }
            catch (Exception e)
            {
                sea_erro = ""+e;
                con.Close();
            }
        }


        public static void user_transfer(int tgt_id, double tgt_qntd) {

            //valor do alvo
            //calcular valor + quantidade transferida
            //atualizar no banco

            //pegar saldo do user & remover e salvar em banco

            //Target Dados
            double tgdad_saldo = 0;


            //Pegando saldo do Target
            var con = new MySqlConnection(db.db_dados);
            con.Open();
                var com = con.CreateCommand();
                com.CommandText = "use " + db.db_database + "; select * from " + db.db_table + " where id='" + tgt_id + "';";
                var reader = com.ExecuteReader();
                while (reader.Read())
                {
                    tgdad_saldo = reader.GetDouble("saldo");

                }
            con.Close();


            //Calcuando novo saldo dos inviduos
            double tgtdad_setar = tgdad_saldo + tgt_qntd;
            
            double usersaldo_new = db.pass_saldo - tgt_qntd;

            db.pass_saldo = usersaldo_new;

            //definindo novas informações :)
            var con2 = new MySqlConnection(db.db_dados);
            con2.Open();
            var com2 = con2.CreateCommand();
            com2.CommandText = "use " + db.db_database + "; update " + db_table + " set saldo='" + tgtdad_setar + "' where id='" + tgt_id + "'; update " + db_table + " set saldo='" + usersaldo_new + "' where id='" + db.pass_id + "';";
            var reader2 = com2.ExecuteReader();
            con2.Close();


        }

        //adicionar valor
        public static void add_valor(int id, double valor)
        {
            double valorfinal = db.pass_saldo + valor;

            var con = new MySqlConnection(db.db_dados);
            con.Open();
            var com = con.CreateCommand();
            com.CommandText = "use " + db.db_database + "; update " + db.db_table + " set saldo='" + valorfinal + "' where id='" + id + "';";
            var reader = com.ExecuteReader();
            con.Close();

            if (id == db.pass_id)
            {
                db.pass_saldo = valorfinal;
            }

        }
        

        public static void user_newpfp(byte[] stream)
        {
            //enviar foto para banco de dados

            
            var con = new MySqlConnection(db.db_dados);
            con.Open();
            var com = con.CreateCommand();
            
            com.CommandText = "use " + db.db_database + "; UPDATE " + db.db_table + " SET foto='" + stream + "', sempfp = '0' where id='" + db.pass_id + "';";
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }


           

    }

}