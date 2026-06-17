using Newtonsoft.Json;
using System.Text;

namespace CiudApp.Services
{
    public class IAService
    {
        private readonly IConfiguration _configuration;

        public IAService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> PreguntarAsync(string pregunta)
        {
            var apiKey = _configuration["OpenRouter:ApiKey"];

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "http://localhost:5095");
            httpClient.DefaultRequestHeaders.Add("X-Title", "CiudApp");

            var requestBody = new
            {
                model = "nvidia/nemotron-3-ultra-550b-a55b:free",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = """
                        Eres el asistente virtual de CiudApp.

                        CiudApp tiene actualmente estos módulos:

                        - EcoRetos
                        - Rutas Seguras
                        - CiudadMap
                        - Vida Universitaria
                        - Dashboard
                        - Ranking de usuarios
                        - Perfil de usuario

                        No inventes funcionalidades.

                        Si el usuario pregunta algo que no existe en la aplicación,
                        indica amablemente que esa funcionalidad no está implementada.

                        Responde siempre en español y de forma breve.
                        """
                    },
                    new
                    {
                        role = "user",
                        content = pregunta
                    }
                }
            };

            var json = JsonConvert.SerializeObject(requestBody);

            var response = await httpClient.PostAsync(
                "https://openrouter.ai/api/v1/chat/completions",
                new StringContent(json, Encoding.UTF8, "application/json"));

            var content = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(content)!;

            return result.choices[0].message.content.ToString();
        }
    }
}