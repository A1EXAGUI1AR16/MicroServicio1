using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroServicio1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PersonController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("get-person")]
        public async Task<IActionResult> GetPersonData()
        {
            // Hacer petición al segundo microservicio
            var response = await _httpClient.GetAsync("http://localhost:5198/api/Person");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return Ok(data); // Devuelve los datos del microservicio 2
            }

            return StatusCode((int)response.StatusCode, "Error al conectar con Microservicio 2");
        }
    }
}
