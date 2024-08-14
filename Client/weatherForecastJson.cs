using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ClimaTempo.Client
{
    public class weatherForecastJson
    {
        //public async Task<IActionResult> InicioAPI()
        //{
            //var httpClient = new HttpClient();
            ////Busca da previsão do tempo para woeid=455912/São José dos Campos
            //var url = "https://api.hgbrasil.com/weather?key=cefd468d";
            //var response = await httpClient.GetAsync(url);

            //JObject value = JObject.Parse(await response.Content.ReadAsStringAsync());
            //string error = (string)value["error"];

            //if (error == "True")
            //{
            //    throw new Exception("Por favor, entre em contato com o Suporte");
            //}
            //return value;
        //}
    }
}
