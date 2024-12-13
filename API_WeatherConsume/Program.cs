#region Menü Başlangıcı
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

Console.WriteLine("Hava Durumu Uygulamasına Hoşgeldiniz \n" ); //ctrl+D yapınca alta aynısını getiriryor
Console.WriteLine("*** Yapmak İstediğiniz İşlemi Seçiniz *** \\n");
Console.WriteLine("1-Şehir Listesini Getirin");
Console.WriteLine("2-Şehir ve Hava Durumu Listesini Getirin");
Console.WriteLine("3-Yeni Şehir Ekleme");
Console.WriteLine("4-Şehir Silme İşlemi");
Console.WriteLine("5-Şehir Güncelleme İşlemi");
Console.WriteLine("6-ID'ye Göre Şehir Getirme");
Console.WriteLine();
#endregion


string number;
Console.WriteLine("Tercihiniz:");
number = Console.ReadLine();

if (number == "1")
{
    string url = "http://localhost:5242/api/WeathersControllers";
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response= await client.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();
        JArray jArray = JArray.Parse(responseBody);
        int index = 1;
        foreach( var item in jArray)
        {
            string cityName = item["cityName"].ToString();
            Console.WriteLine($"{index}.Şehir : {cityName}");
            index++;
        }
    }

}
if (number == "2")
{
    string url = "http://localhost:5242/api/WeathersControllers";
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        string responseBody= await response.Content.ReadAsStringAsync();
        JArray jArray = JArray.Parse(responseBody);
        foreach(var item in jArray)
        {
            char degreeSymbol = '\u00B0';
            string cityName = item["cityName"].ToString();
            string country = item["country"].ToString();
            string temp = item["temp"].ToString();
            Console.WriteLine(cityName + " - " + country + " ---> "+ temp + $"{degreeSymbol}C"  );


        }
    }

}
if (number == "3")
{
    Console.WriteLine("Şehir ekleme işlemleri ");

}
Console.Read();