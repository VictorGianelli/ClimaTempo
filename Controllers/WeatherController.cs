using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClimaTempo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet("testeAPI")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TestAPI()
        {
            var httpClient = new HttpClient();
            //Busca da previsão do tempo para woeid=455912/São José dos Campos
            var url = "https://api.hgbrasil.com/weather?key=cefd468d";
            var response = await httpClient.GetAsync(url);

            var data = await response.Content.ReadAsStringAsync();

            return Ok(data);
        }

        public static async Task Get(string data)
        {
            var httpClient = new HttpClient();
            //var json = new HttpClient().GetAsync(url);

            var url = "https://api.hgbrasil.com/weather?woeid=455912"+".json";

            var response = await httpClient.GetAsync(url);

            data = await response.Content.ReadAsStringAsync();

        }

        [HttpGet("Campinas,SP")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClimaSaoPaulo()
        {
            var httpClient = new HttpClient();
            //Busca da previsão do tempo para a cidade de Campinas,SP
            var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name=Campinas,SP";
            var response = await httpClient.GetAsync(url);

            var data = await response.Content.ReadAsStringAsync();

            return Ok(data);
        }

        [HttpGet("Macapá,AP")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClimaMacapá()
        {
            var httpClient = new HttpClient();
            //Busca da previsão do tempo para a cidade de Macapa,AP
            var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name=Macapa,AP";
            var response = await httpClient.GetAsync(url);

            var data = await response.Content.ReadAsStringAsync();

            return Ok(data);
        }

        [HttpGet("BeloHorizonte,MG")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClimaBeloHoriznte()
        {
            var httpClient = new HttpClient();
            //Busca da previsão do tempo para a cidade de BeloHoroiznte,MG
            var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name=BeloHorizonte,MG";
            var response = await httpClient.GetAsync(url);

            var data = await response.Content.ReadAsStringAsync();

            return Ok(data);
        }

        [HttpGet("escolhaCidade")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClimaCidade(string nomeCidade,string UF)
        {
            var httpClient = new HttpClient();
            //Busca da previsão do tempo para a cidade "nomeCidade" do estado "UF"
            var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name="+ nomeCidade.Trim() + ","+UF;
            var response = await httpClient.GetAsync(url);

            var data = await response.Content.ReadAsStringAsync();
            //var value = JsonConvert.DeserializeObject(data);
            JObject value = JObject.Parse(data);
            string by = (string)value["by"];

            return Ok(value);
        }


        [HttpGet("escolhaCidadePesquisaPersonalizada")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClimaCidadeSP(string nomeCidade, string UF)
        {
            var httpClient = new HttpClient();
            //Busca da previsão do tempo para a cidade "nomeCidade" do estado "UF"
            var url = "https://api.hgbrasil.com/weather?array_limit=5&fields=only_results,temp,city_name,forecast,max,min,date,condition&key=cefd468d";
            var response = await httpClient.GetAsync(url);

            var data = await response.Content.ReadAsStringAsync();
            //var value = JsonConvert.DeserializeObject(data);
            JObject value = JObject.Parse(data);
            string by = (string)value["by"];

            return Ok(data);
        }

    }
}
