using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace CodeVersioning.Controllers
{
    //С помощью атрибутов настраиваем контроллер для работы с API
    [ApiController]
    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2)]
    [ApiVersion(3)]
    //Добавляем в маршрут "/api/ для выразительности
    [Route("api/[controller]")]
    public class QueryStringController : ControllerBase
    {
        //Список состояний
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //Обязательно указываем через какой Http-метод клиент может обратиться в API
        [HttpGet]
        //Программируем три разные модификации Get() через специальный атрибут
        //разделяем к ним доступ по соответствующим версиям WebApi
        [MapToApiVersion(1)]
        public IEnumerable<string> Get1()
        {
            return Summaries.Where(x => x.StartsWith("B"));
        }

        [HttpGet]
        [MapToApiVersion(2)]
        public IEnumerable<string> Get2()
        {
            return Summaries.Where(x => x.StartsWith("C"));
        }

        [HttpGet]
        [MapToApiVersion(3)]
        public IEnumerable<string> Get3()
        {
            return Summaries.Where(x => x.StartsWith("S"));
        }

    }
}
