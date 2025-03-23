
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.OpenApi.Models;

namespace CodeVersioning
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi("v1");
            builder.Services.AddOpenApi("v2");
            builder.Services.AddOpenApi("v3");

            //Добавляем поддержку Swagger
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeVersioning", Version = "v1", });
                options.SwaggerDoc("v2", new OpenApiInfo { Title = "CodeVersioning", Version = "v2", });
                options.SwaggerDoc("v3", new OpenApiInfo { Title = "CodeVersioning", Version = "v3", });
            });


            //Добавляем поддержку версионирования
            builder.Services.AddApiVersioning(options =>
            {
                //Версия API по-умолчанию
                options.DefaultApiVersion = new ApiVersion(2);
                //Добавляем специальные HTTP-залоговки, в которых перечислены аклуальные и устаревшие версии API
                options.ReportApiVersions = true;
                //Используем API версию по-умолчанию, если клиент явно не указал нужную ему
                options.AssumeDefaultVersionWhenUnspecified = true;
                //Определяем, что будеи ожидать нужную версию API в самой строке запроса или в URL-сегменте
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(), //https://site.com/v2/getdata
                    new QueryStringApiVersionReader("version")); //https://site.com/getdata?version=2
            })
            //Подключаем поддержку MVC для персионирования API
            .AddMvc()
            //Данные метод исправляем конечные маршруты и подставляет нужную версию API через параметр
            .AddApiExplorer(options => 
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {

                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeVersioning v1");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", "CodeVersioning v2");
                    options.SwaggerEndpoint("/swagger/v3/swagger.json", "CodeVersioning v3");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            //Определяем общий ApiVersionSet для всех endpoints
            //В этом примере для  Minumal Apis реализуется все версии API
            ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                .HasDeprecatedApiVersion(new ApiVersion(1))
                .HasApiVersion(new ApiVersion(2))
                .ReportApiVersions()
                .Build();
            //Один endpoint но разный функционал в зависимости от версии API
            app.MapGet("/api/getdata", () => Results.Text("v1"))
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(new ApiVersion(1));

            app.MapGet("/api/getdata", () => Results.Text("v2"))
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(new ApiVersion(2));

            app.Run();
        }
    }
}
