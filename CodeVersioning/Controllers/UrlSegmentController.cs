using Asp.Versioning;
using CodeVersioning.Models.V1;
using Microsoft.AspNetCore.Mvc;

namespace CodeVersioning.Controllers
{
    [ApiController]
    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2)]
    //��������� � ������� "/api/ ��� ���������������
    //����� ��������� ��������� ������� "vX" ��� �������� ������� ������ API
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UrlSegmentController : ControllerBase
    {
        //������ �������� ���������
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        //����������� ��������� ����� ����� Http-����� ������ ����� ���������� � API
        [HttpGet]
        //������������� ��� ������ ����������� Get() ����� ����������� �������
        //��������� � ��� ������ �� ��������������� ������� WebApi
        [MapToApiVersion(1)]
        public IEnumerable<Models.V1.WeatherForecast> Get1()
        {
            //���������������� ������ ������ ������ Get() ��������� ��� ���������
            return Enumerable.Range(1, 5).Select(index => new Models.V1.WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
        }

        [HttpGet]
        //����� ����� �������� "range" ����� ������� ������� ���-�� ������� � ������
        [MapToApiVersion(2)]
        public IEnumerable<Models.V2.WeatherForecast> Get2(int? range)
        {
            return Enumerable.Range(1, range ?? 5).Select(index => new Models.V2.WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                YourApiKey = Guid.NewGuid()
            });
        }
    }
}
