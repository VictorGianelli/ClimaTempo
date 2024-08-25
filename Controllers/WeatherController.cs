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
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TestAPI()
        {
            try
            {
                //Busca da previsão do tempo para woeid=455912/São José dos Campos
                var url = "https://api.hgbrasil.com/weather?key=cefd468d";
                var data = await CondicaoClima(url);

                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unkown error");
            }
        }


        [HttpGet("Campinas/SP")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ClimaSaoPaulo()
        {
            try { 
                //Busca da previsão do tempo para a cidade de Campinas,SP
                var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name=Campinas,SP";
                var data = await CondicaoClima(url);

                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unkown error");
            }
        }

        [HttpGet("Macapá/AP")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ClimaMacapá()
        {
            try { 
                //Busca da previsão do tempo para a cidade de Macapa,AP
                var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name=Macapa,AP";
                var data = await CondicaoClima(url);

                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unkown error");
            }
        }

        [HttpGet("BeloHorizonte/MG")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ClimaBeloHoriznte()
        {
            try { 
                //Busca da previsão do tempo para a cidade de BeloHoroiznte,MG
                var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name=BeloHorizonte,MG";
                var data = await CondicaoClima(url);

                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unkown error");
            }
        }

        [HttpPost("escolhaCidade")]
        [ProducesResponseType(typeof(EscolhaCidade), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EscolhaCidade), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ClimaCidade([FromBody] EscolhaCidade request)
        {
            try { 
                //Busca da previsão do tempo para a cidade "nomeCidade" do estado "UF"
                var url = "https://api.hgbrasil.com/weather?key=cefd468d&city_name="+ request.NomeCidade.Trim() + ","+ request.UF;
                var data = await CondicaoClima(url);
            
                JObject value = JObject.Parse(data);
                string city_name = (string)value["city_name"]; 

                if (city_name.Trim() != request.NomeCidade.Trim())
                {
                    return BadRequest("Verify the input");
                }
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unkown error");
            }
        }

        [HttpPost("escolhaCidadePesquisaPersonalizada")]
        [ProducesResponseType(typeof(EscolhaCidade), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EscolhaCidade), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ClimaPersonalizadoCidade([FromBody] EscolhaCidade request)
        {
            try { 
                //Busca da previsão do tempo para a cidade "nomeCidade" do estado "UF"
                var url = "https://api.hgbrasil.com/weather?array_limit=5&fields=only_results,temp,city_name,forecast,max,min,date,condition&city_name=" + request.NomeCidade.Trim() + ","+ request.UF;
                var data = await CondicaoClima(url);

                JObject value = JObject.Parse(data);
                string city_name = (string)value["city_name"];

                if (city_name.Trim() != request.NomeCidade.Trim())
                {
                    return BadRequest("Verify the input");
                }
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unkown error");
            }
        }

        private static async Task<string> CondicaoClima(string url)
        {
            var httpClient = new HttpClient();
            var responseHttp = await httpClient.GetAsync(url);

            var data = await responseHttp.Content.ReadAsStringAsync();

            JObject value = JObject.Parse(data);
            string valid_key = (string)value["valid_key"];

            if(valid_key == "False")
            {
                throw new ArgumentException("The key isn't valid. Please, call the Tecnical Support");
            }
            return data;
        }
    }
}
