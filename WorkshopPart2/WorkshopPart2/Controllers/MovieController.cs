using Domain.Enums;
using Dtos.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared;

namespace WorkshopPart2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;   
        }

        [HttpGet("getmovies")]
        public ActionResult<List<MovieDto>> GetAll()
        {
            try
            {
                return Ok(_movieService.GetAllMovies());
            }
            catch(MovieException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<MovieDto> GetById(int id)
        {
            try
            {
                return Ok(_movieService.GetMovieById(id));  
            }
            catch (MovieNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("createmovie")]
        public IActionResult AddMovie([FromBody] CreateMovieDto createMovieDto)
        {
            try
            {
                _movieService.AddMovie(createMovieDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

        [HttpPut("updatemovie")]
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto updateMovieDto)
        {
            try
            {
                _movieService.UpdateMovie(updateMovieDto);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (MovieNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                _movieService.DeleteMovie(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (MovieNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (MovieException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("filtermovies")]
        public IActionResult FilterMovies(int? year, GenreEnum? genre)
        {
            try
            {
                return Ok(_movieService.FilterMovies(year, genre)); 
            }
            catch(MovieNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (MovieException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
