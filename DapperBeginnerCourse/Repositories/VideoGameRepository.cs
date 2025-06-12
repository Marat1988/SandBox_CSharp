using Dapper;
using DapperBeginnerCourse.Models;
using System.Data.SqlClient;

namespace DapperBeginnerCourse.Repositories
{
    public class VideoGameRepository : IVideoGameRepository
    {
        private readonly IConfiguration _configuration;

        public VideoGameRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task AddAsync(VideoGame videoGame)
        {
            using var connection = GetConnection();
            var query = "INSERT INTO VideoGames (Title, Publisher, Developer, ReleaseDate) VALUES (@Title, @Publisher, @Developer, @ReleaseDate); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            int newId = await connection.QuerySingleAsync<int>(query, videoGame);
            videoGame.Id = newId;
               
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync("DELETE FROM VideoGames WHERE Id=@id", new { Id = id });
        }

        public async Task<List<VideoGame>> GetAllAsync()
        {
            using var connection = GetConnection();
            var videoGames = await connection
                .QueryAsync<VideoGame>("SELECT * FROM VideoGames");
            return videoGames.ToList();
        }

        public async Task<VideoGame> GetByIdAsync(int id)
        {
            using var connection = GetConnection();
            connection.Open();
            var videoGame = await connection.QueryFirstOrDefaultAsync<VideoGame>("SELECT * FROM VideoGames WHERE ID = @Id", new { Id = id });
            return videoGame;
        }



        public async Task UpdateAsync(VideoGame videoGame)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync("UPDATE VideoGames SET Title=@Title, Publisher=@Publisher, Developer=@Developer, ReleaseDate=@ReleaseDate WHERE Id=@Id", videoGame);
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
