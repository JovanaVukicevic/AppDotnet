using AppDotnet.Dto;
using AppDotnet.Interfaces;
using AppDotnet.Models;
using AppDotnet.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository; 
        private readonly IMapper _mapper;
        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]

        public IActionResult GetGenres()
        {
            var genres = _mapper.Map<List<GenreDto>>(_genreRepository.GetGenres());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(genres);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGenre([FromBody] GenreDto genreCreate)
        {
            if (genreCreate == null)
                return BadRequest(ModelState);

            var genre = _genreRepository.GetGenres()
                .Where(m => m.Name.Trim().ToUpper() == genreCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (genre != null)
            {
                ModelState.AddModelError("", "Genre already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(genreCreate);
            if (!_genreRepository.CreateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");

        }
    }
}
