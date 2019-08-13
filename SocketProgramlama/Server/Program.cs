using System;
using System.Net.Sockets;
using System.IO;


namespace Server
{
    public class Server
    {
        public static void Main()
        {
            //Bilgi alisverisi için bilgi almak istedigimiz port numarasini TcpListener sinifi ile gerçeklestiriyoruz
            TcpListener TcpDinleyicisi = new TcpListener(1234);
            TcpDinleyicisi.Start();

            Console.WriteLine("Sunucu Başlatıldı...");

            //Soket baglantimizi yapiyoruz.Bunu TcpListener sinifinin AcceptSocket metodu ile yaptigimiza dikkat edin
            Socket IstemciSoketi = TcpDinleyicisi.AcceptSocket();

            // Baglantının olup olmadığını kontrol ediyoruz
            if (!IstemciSoketi.Connected)
            {
                Console.WriteLine("Sunucu Başlatılamıyor!");
            }
            else
            {
                //Sonsuz döngü sayesinde AgAkimini sürekli okuyoruz
                while (true)
                {
                    Console.WriteLine("İstemci bağlantısı sağlandı.");

                    //IstemciSoketi verilerini NetworkStream sinifi türünden nesneye aktariyoruz.
                    NetworkStream AgAkimi = new NetworkStream(IstemciSoketi);

                    //Soketteki bilgilerle islem yapabilmek için StreamReader ve StreamWriter siniflarini kullaniyoruz
                    StreamWriter AkimYazici = new StreamWriter(AgAkimi);
                    StreamReader AkimOkuyucu = new StreamReader(AgAkimi);

                    //StreamReader ile String veri tipine aktarma islemi önceden bir hata olursa bunu handle etmek gerek
                    try
                    {
                        string IstemciString = AkimOkuyucu.ReadLine();
                        Console.WriteLine("Gelen bilgi:" + IstemciString);

                        //Istemciden gelen bilginin uzunlugu hesaplaniyor
                        int uzunluk=IstemciString.Length;

                        //AgAkimina, AkimYazını ile IstemciString inin uzunluğunu yazıyoruz 
                        AkimYazici.WriteLine(uzunluk.ToString());

                        AkimYazici.Flush();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Sunucu kapatılıyor..");
                        return;
                    }

                }

            }
            IstemciSoketi.Close();
            Console.WriteLine("Sunucu Kapatiliyor...");
        }
        
    }
}
