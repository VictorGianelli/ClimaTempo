using ClimaTempo.Communication.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
            //Busca da previsão do tempo para woeid=455912/São José dos Campos
            var url = "https://api.hgbrasil.com/weather?key=cefd468d";
            var data = await CondicaoClima(url);

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

        [HttpPost()]
        [ProducesResponseType(typeof(Value), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Value), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClimaTeste([FromBody] Value request)
        {
            var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name=Campinas,SP";
            var data = await CondicaoClima(url);

            return Ok(data);
        }


        [HttpGet("Campinas/SP")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClimaSaoPaulo()
        {
            //Busca da previsão do tempo para a cidade de Campinas,SP
            var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name=Campinas,SP";
            var data = await CondicaoClima(url);

            return Ok(data);
        }

        [HttpGet("Macapá/AP")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClimaMacapá()
        {
            //Busca da previsão do tempo para a cidade de Macapa,AP
            var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name=Macapa,AP";
            var data = await CondicaoClima(url);

            return Ok(data);
        }

        [HttpGet("BeloHorizonte/MG")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClimaBeloHoriznte()
        {
            //Busca da previsão do tempo para a cidade de BeloHoroiznte,MG
            var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name=BeloHorizonte,MG";
            var data = await CondicaoClima(url);

            return Ok(data);
        }

        [HttpPost("escolhaCidade")]
        [ProducesResponseType(typeof(EscolhaCidade), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EscolhaCidade), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClimaCidade([FromBody] EscolhaCidade request)
        {
            //Busca da previsão do tempo para a cidade "nomeCidade" do estado "UF"
            var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name="+ request.NomeCidade.Trim() + ","+ request.UF;
            var data = await CondicaoClima(url);
            
            JObject value = JObject.Parse(data);
            string by = (string)value["by"];

            return Ok(data);
        }

        [HttpPost("escolhaCidadePesquisaPersonalizada")]
        [ProducesResponseType(typeof(EscolhaCidade), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EscolhaCidade), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClimaPersonalizadoCidade([FromBody] EscolhaCidade request)
        {
            //Busca da previsão do tempo para a cidade "nomeCidade" do estado "UF"
            var url = "https://api.hgbrasil.com/weather?array_limit=5&fields=only_results,temp,city_name,forecast,max,min,date,condition&key=cefd468d&city_name="+ request.NomeCidade.Trim() + ","+ request.UF;
            var data = await CondicaoClima(url);

            JObject value = JObject.Parse(data);
            string by = (string)value["by"];

            return Ok(data);
        }

        private static async Task<string> CondicaoClima(string url)
        {
            var httpClient = new HttpClient();
            var responseHttp = await httpClient.GetAsync(url);

            var data = await responseHttp.Content.ReadAsStringAsync();
            return data;
        }
    }
}
