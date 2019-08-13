using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Collections;


namespace Client
{
    public partial class Form1 : Form
    {
        //Burda server da tanımladıklarımızdan farklı olarak TcpClient sınıfı ile serverdan gelen bilgileri alıyoruz
        public TcpClient istemci;
        private NetworkStream AgAkimi;
        private StreamReader AkimOkuyucu;
        private StreamWriter AkimYazici;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                istemci = new TcpClient("localhost", 1234);

            }
            catch (Exception)
            {
                Console.WriteLine("Bağlanamadı..");
                return;
            }

            //Server programında yaptıklarımızı burda da yapıyoruz.
            AgAkimi = istemci.GetStream();
            AkimOkuyucu = new StreamReader(AgAkimi);
            AkimYazici = new StreamWriter(AgAkimi);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Kullanıcı butona her tıkladığında textbox'ta yazı yoksa uyarı veriyoruz
            //Sonra AkimYazici vasıtası ile AgAkımına veriyi gönderip sunucudan gelen 
            //cevabı AkimOkuyucu ile alıp Mesaj la kullanıcıya gösteriyoruz 
            //Tabi olası hatalara karşı, Sunucuya bağlanmada hata oluştu mesajı veriyoruz.

            try
            {
                if (textBox1.Text=="")
                {
                    MessageBox.Show("Lütfen bir yazı giriniz", "Uyarı");
                    textBox1.Focus();
                    return;
                }

                string yazi;
                AkimYazici.WriteLine(textBox1.Text);
                AkimYazici.Flush();
                yazi = AkimOkuyucu.ReadLine();
                MessageBox.Show(yazi, "Sunucudan mesaj var.");

            }
            catch (Exception)
            {
                MessageBox.Show("Sunucuya bağlanmada hata var.");

                throw;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                AkimYazici.Close();
                AkimOkuyucu.Close();
                AgAkimi.Close();
            }

            catch
            {
                MessageBox.Show("Düzgün kapatilamiyor");
            }
        }

        //Ve bütün oluşturduğumuz nesneleri form kapatıldığında kapatıyoruz.
    }
}
