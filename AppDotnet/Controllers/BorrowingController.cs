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
    public class BorrowingController : Controller
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IMapper _mapper;
        public BorrowingController(IBorrowingRepository borrowingRepository, IMapper mapper)
        {
                _borrowingRepository = borrowingRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Borrowing>))]
        public IActionResult GetBorrowings()
        {
            var bor = _mapper.Map<List<BorrowingDto>>(_borrowingRepository.GetBorrowings());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bor);
        }
        [HttpGet("memberId")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Borrowing>))]
        [ProducesResponseType(400)]
        public IActionResult GetBorrowingsOfAMember(int memberId)
        {
            if (!_borrowingRepository.BorrowExists(memberId))
                return NotFound();

            var bor = _mapper.Map<BorrowingDto>(_borrowingRepository.GetBorrowingsOfAMember(memberId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bor);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBorrowing([FromBody] BorrowingDto borrowCreate)
        {
            if (borrowCreate == null)
                return BadRequest(ModelState);

            var borrow = _borrowingRepository.GetBorrowings()
                .Where(m => m.BookId == borrowCreate.BookId).FirstOrDefault();
            if (borrow != null)
            {
                ModelState.AddModelError("", "Borrowing already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var borrowMap = _mapper.Map<Borrowing>(borrowCreate);
            if (!_borrowingRepository.CreateBorrowing(borrowMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }
        [HttpPut("{borrowId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBorrowing(int borrowId, [FromBody] BorrowingDto updatedBorrowing)
        {
            if (updatedBorrowing == null)
                return BadRequest(ModelState);

            if(borrowId != updatedBorrowing.Id)
                return BadRequest(ModelState);

            if (!_borrowingRepository.BorrowExists(borrowId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var borrowMap = _mapper.Map<Borrowing>(updatedBorrowing);

            if (!_borrowingRepository.UpdateBorrowing(borrowMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating borrowing.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{borrowId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBorrowing(int borrowId)
        {
            if (!_borrowingRepository.BorrowingIdExists(borrowId)){
                return NotFound();

            }

            var borrowToDelete = _borrowingRepository.GetBorrowing(borrowId);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (!_borrowingRepository.DeleteBorrowing(borrowToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting a borrowing.");
            }
            return NoContent();
        }
    }
}
