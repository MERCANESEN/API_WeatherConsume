#region Menü Başlangıcı
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

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
            Console.WriteLine(cityName + " - " + country + " ---> " + temp + $"{degreeSymbol}C");

        }
    }

}
if (number == "3")
{
    Console.Write("*** Yeni Veri Girişi ***\n");
    string cityName, country, detail;
    decimal temp;


    Console.Write("Şehir Adı:");
    cityName = Console.ReadLine();

    Console.Write("Ülke Adı:");
    country = Console.ReadLine();

    Console.Write("Sıcaklık:");
    temp = decimal.Parse(Console.ReadLine());

    Console.Write("Hava Durumu Bilgisi:");
    detail=Console.ReadLine();

    string url = "http://localhost:5242/api/WeathersControllers";
    var newWeather = new
    {
        cityName = cityName,
        country = country,
        temp = temp,
        detail = detail

    };


    using (HttpClient client = new HttpClient())

    {
        string json = JsonConvert.SerializeObject(newWeather);
        StringContent content = new StringContent(json, encoding: Encoding.UTF8 ,"application/json");
        HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
    }



}
if(number == "4")
{
    string url = "http://localhost:5242/api/WeathersControllers?id=";
    Console.WriteLine("*** Silmek İstediğiniz Şehrin Id sini Giriniz:");
    int id = int.Parse(Console.ReadLine());

    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.DeleteAsync(url + id);
        response.EnsureSuccessStatusCode();
    }


}
if(number== "5")
{
    Console.Write("*** Veri Güncelleme İşlemi ***\n");
    string cityName, country, detail;
    decimal temp;
    int id;

    Console.Write("Şehir Id:");
    id=int.Parse(Console.ReadLine());

    Console.Write("Şehir Adı:");
    cityName = Console.ReadLine();

    Console.Write("Ülke Adı:");
    country = Console.ReadLine();

    Console.Write("Sıcaklık:");
    temp = decimal.Parse(Console.ReadLine());

    Console.Write("Hava Durumu Bilgisi:");
    detail = Console.ReadLine();
    string url = "http://localhost:5242/api/WeathersControllers";
    var updateWeather = new 
    {       CityId=id,
            CityName = cityName,
            Country = country,
            Temp = temp,
            Detail = detail

};
    using(HttpClient client = new HttpClient())
    {
        string json = JsonConvert.SerializeObject(updateWeather);
        StringContent content = new StringContent(json , Encoding.UTF8 , "application/json");
        HttpResponseMessage response = await client.PutAsync(url, content);
        response.EnsureSuccessStatusCode();

    }  
}
if(number== "6")
{
    string url = "http://localhost:5242/api/WeathersControllers/GetIdWeatherCity?id=";

    Console.Write("Bilgilerinizi Getirmek İstediğiniz Şehrin Id sini Giriniz:");
    int id = int.Parse(Console.ReadLine());
    Console.WriteLine();

    using(HttpClient client = new HttpClient())
    {

        HttpResponseMessage response = await client.GetAsync(url + id);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject weatherCityObject = JObject.Parse(responseBody);

        string cityName = weatherCityObject["cityName"].ToString();
        string detail = weatherCityObject["detail"].ToString();
        string country = weatherCityObject["country"].ToString();
        decimal temp =decimal.Parse(weatherCityObject["temp"].ToString());

        Console.WriteLine("Girmiş Olduğunuz Id ye Göre Bilgiler");
        Console.WriteLine();
        Console.Write("Şehir: " + cityName + "\nÜlke: " + country + "\nDetay: " + detail + "\nSıcaklık: " + temp);



    }
}
Console.Read();
