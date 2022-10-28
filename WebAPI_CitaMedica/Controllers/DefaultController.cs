using Microsoft.AspNetCore.Mvc;

namespace WebAPI_CitaMedica.Controllers
{
    [Route("/")]
    [ApiController]
    public class DefaultController
    {
        private readonly string _text;
        public DefaultController(IConfiguration configuration)
        {
            var versionWebAPI = configuration.GetValue<string>("VersionWebAPI");
            _text = string.Concat(configuration.GetValue<string>("DefaultPageText"),
                "\nFecha actual: ", DateTime.Now.ToString(),
                "\nVersión API: " + versionWebAPI);
        }

        [HttpGet]
        public string Index()
        {
            return _text;
        }
    }
}
