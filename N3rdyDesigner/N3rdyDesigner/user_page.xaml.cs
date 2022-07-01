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
using Xamarin.Essentials;

namespace N3rdyDesigner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class user_page : ContentPage
    {

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MainPage();
            return true;
        }
        public user_page()
        {
            InitializeComponent();
            loader();
        }

        public void loader()
        {
            if (db.pass_sempfp == "1")
            {
                user_pfp.Source = "nopfp";
                user_pfpmenu.Source = "nopfp";
            }
            else
            {
                user_pfpmenu.Source = ImageSource.FromStream(() => new MemoryStream(db.pass_foto));
                user_pfp.Source = ImageSource.FromStream(() => new MemoryStream(db.pass_foto));
            }

            user_id.Text = user_id.Text + db.pass_id;
            user_email.Text = user_email.Text + db.pass_email;
            user_nome.Text = user_nome.Text + db.pass_nome;
            
            switch (db.pass_cargo)
            {
                case "dev":
                    user_cargo.Text = user_cargo.Text + db.pass_cargo;
                    user_cargo.TextColor = Xamarin.Forms.Color.Yellow;
                    break;

                case "admin":
                    user_cargo.Text = user_cargo.Text + db.pass_cargo;
                    user_cargo.TextColor = Xamarin.Forms.Color.Red;
                    break;


                default:
                    user_cargo.Text = user_cargo.Text + db.pass_cargo;
                    break;
            }

        }

        private void user_pfpmenu_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new user_page();
        }


        public static byte[] foto = null;
        public static BinaryReader brs = null;
        public Image imagem = null;

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var user_nwpfp = await MediaPicker.PickPhotoAsync(new MediaPickerOptions {Title = "Escolha sua nova foto de perfil."});

            var img = await user_nwpfp.OpenReadAsync();

            new_pfp.Source = ImageSource.FromStream(() => img);

            var con = new MySqlConnection(db.db_dados);
            con.Open();
            var com = con.CreateCommand();
            com.CommandText = "use " + db.db_database + "; update " + db.db_table + " set foto='" + img + "' where id='" + db.pass_id + "';";
            var reader = com.ExecuteReader();
            con.Close();

        }

        private void user_salvar_Clicked(object sender, EventArgs e)
        {
            
            //BinaryReader brs = new BinaryReader(img);
            //foto = brs.ReadBytes((int)img.Length);

            DisplayAlert("Dev Menu", " " + brs, "ok");

            


        }
        public byte[] ConvertStreamToByteArray(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}