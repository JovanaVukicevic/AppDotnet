using AppDotnet.Dto;
using AppDotnet.Interfaces;
using AppDotnet.Models;
using AppDotnet.Repository;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc;

namespace AppDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController: Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;
        private readonly IBorrowingRepository _borrowingRepository;
        public BookController(IBookRepository bookRepository, IMapper mapper, IGenreRepository gerneRepository, IBorrowingRepository borrowingRepository)
        {
                _bookRepository = bookRepository;
                _genreRepository = gerneRepository;
                _borrowingRepository = borrowingRepository;
                _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public IActionResult GetBooks()
        {
            var books = _mapper.Map<List<BookDto>>(_bookRepository.GetBooks());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(books);
        }
        [HttpGet("{title}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]
        public IActionResult GetBook(string title)
        {
            if(!_bookRepository.BookExists(title))
                return NotFound();

            var book = _mapper.Map<Book>(_bookRepository.GetBook(title));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBook([FromBody] BookDto bookCreate)
        {
            if (bookCreate == null)
                return BadRequest(ModelState);

            var book = _bookRepository.GetBooks()
                .Where(m => m.Title.Trim().ToUpper() == bookCreate.Title.TrimEnd().ToUpper()).FirstOrDefault();
            if (book != null)
            {
                ModelState.AddModelError("", "Book already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookMap = _mapper.Map<Book>(bookCreate);
            bookMap.Genre = _genreRepository.GetGenreByName(bookCreate.genreName);

            if (!_bookRepository.CreateBook(bookMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");

        }
        [HttpPut("{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMember(int bookId, [FromBody] BookDto updatedBook)
        {
            if (updatedBook == null)
                return BadRequest(ModelState);

            if (bookId != updatedBook.Id)
                return BadRequest(ModelState);

            if (!_bookRepository.BookExistsId(bookId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var bookMap = _mapper.Map<Book>(updatedBook);

            if (!_bookRepository.UpdateBook(bookMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating book.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBook(int bookId)
        {
            if (!_bookRepository.BookExistsId(bookId))
            {
                return NotFound();

            }

            var bookToDelete = _bookRepository.GetBook(bookId);
            if (_bookRepository.GetBorrowingOfABook(bookId) != null)
            {
                var borrowToDelete = _bookRepository.GetBorrowingOfABook(bookId);
                // if (!ModelState.IsValid)
                //   return BadRequest(ModelState);



                if (!_borrowingRepository.DeleteBorrowing(borrowToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting borrowings.");
                }
                //return NoContent();
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (!_bookRepository.DeleteBook(bookToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting a book.");
            }
            return NoContent();
        }
    }
}
