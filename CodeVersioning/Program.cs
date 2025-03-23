
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

            //��������� ��������� Swagger
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeVersioning", Version = "v1", });
                options.SwaggerDoc("v2", new OpenApiInfo { Title = "CodeVersioning", Version = "v2", });
                options.SwaggerDoc("v3", new OpenApiInfo { Title = "CodeVersioning", Version = "v3", });
            });


            //��������� ��������� ���������������
            builder.Services.AddApiVersioning(options =>
            {
                //������ API ��-���������
                options.DefaultApiVersion = new ApiVersion(2);
                //��������� ����������� HTTP-���������, � ������� ����������� ���������� � ���������� ������ API
                options.ReportApiVersions = true;
                //���������� API ������ ��-���������, ���� ������ ���� �� ������ ������ ���
                options.AssumeDefaultVersionWhenUnspecified = true;
                //����������, ��� ����� ������� ������ ������ API � ����� ������ ������� ��� � URL-��������
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(), //https://site.com/v2/getdata
                    new QueryStringApiVersionReader("version")); //https://site.com/getdata?version=2
            })
            //���������� ��������� MVC ��� ��������������� API
            .AddMvc()
            //������ ����� ���������� �������� �������� � ����������� ������ ������ API ����� ��������
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

            //���������� ����� ApiVersionSet ��� ���� endpoints
            //� ���� ������� ���  Minumal Apis ����������� ��� ������ API
            ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                .HasDeprecatedApiVersion(new ApiVersion(1))
                .HasApiVersion(new ApiVersion(2))
                .ReportApiVersions()
                .Build();
            //���� endpoint �� ������ ���������� � ����������� �� ������ API
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
