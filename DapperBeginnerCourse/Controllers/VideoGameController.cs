using DapperBeginnerCourse.Models;
using DapperBeginnerCourse.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperBeginnerCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly IVideoGameRepository _ideoGameRepository;

        public VideoGameController(IVideoGameRepository ideoGameRepository)
        {
            _ideoGameRepository = ideoGameRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetAllAsync()
        {
            var videoGames = await _ideoGameRepository.GetAllAsync();
            return Ok(videoGames);
        }

        [HttpGet("{id}", Name = "GetId")]
        public async Task<ActionResult<VideoGame>> GetIdAsync(int id)
        {
            var videoGame = await _ideoGameRepository.GetByIdAsync(id);
            if (videoGame == null)
            {
                return NotFound("This video game does not exist in the database. :(");
            }
            return Ok(videoGame);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(VideoGame videoGame)
        {
            await _ideoGameRepository.AddAsync(videoGame);
            return CreatedAtAction("GetId", new { id = videoGame.Id }, videoGame);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult>UpdateAsync(int id, VideoGame videoGame)
        {
            var existsGame = await _ideoGameRepository.GetByIdAsync(id); 
            if (existsGame == null)
            {
                return NotFound("Video Game not found.");
            }

            videoGame.Id = id;

            await _ideoGameRepository.UpdateAsync(videoGame);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var existsGame = await _ideoGameRepository.GetByIdAsync(id);
            if (existsGame == null)
            {
                return NotFound("Video Game not found.");
            }

            await _ideoGameRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
