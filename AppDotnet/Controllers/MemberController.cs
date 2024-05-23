//using Microsoft.AspNetCore.Components;
using AppDotnet.Data;
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
    public class MemberController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IMapper _mapper;
        public MemberController(IMemberRepository memberRepository, IMapper mapper, IBorrowingRepository borrowingRepository)
        {
                _memberRepository = memberRepository;
                _borrowingRepository= borrowingRepository;
                _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Member>))]
        public IActionResult GetMembers()
        {
            var members= _mapper.Map<List<MemberDto>>(_memberRepository.GetMembers());
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(members);

        }
        [HttpGet("{memId}")]
        [ProducesResponseType(200, Type = typeof(Member))]
        [ProducesResponseType(400)]
        public IActionResult GetMember(int memId)
        {
            if (!_memberRepository.MemberExists(memId))
                return NotFound();

            var member =_mapper.Map<MemberDto>( _memberRepository.GetMember(memId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(member);

        }
        [HttpGet("borrowing/{memId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Borrowing>))]
        [ProducesResponseType(400)]
        public IActionResult GetBorrowings(int memId)
        {
            if (!_memberRepository.MemberExists(memId))
                return NotFound();

            var borrowings = _memberRepository.GetBorrowings(memId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(borrowings);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMember([FromBody] MemberDto memberCreate)
        {
            if (memberCreate == null)
                return BadRequest(ModelState);

            var member = _memberRepository.GetMembers()
                .Where(m => m.FirstName.Trim().ToUpper() == memberCreate.FirstName.TrimEnd().ToUpper()).FirstOrDefault();
            if(member != null)
            {
                ModelState.AddModelError("", "Member already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var memberMap = _mapper.Map<Member>(memberCreate);

            if (!_memberRepository.CreateMember(memberMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");

        }
        [HttpPut("{memberId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMember(int memberId, [FromBody] MemberDto updatedMember)
        {
            if (updatedMember == null)
                return BadRequest(ModelState);

            if (memberId != updatedMember.Id)
                return BadRequest(ModelState);

            if (!_memberRepository.MemberExists(memberId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var membarMap = _mapper.Map<Member>(updatedMember);

            if (!_memberRepository.UpdateMember(membarMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating member.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{memberId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMember(int memberId)
        {
            if (!_memberRepository.MemberExists(memberId))
                return NotFound();

             

            var memberToDelete = _memberRepository.GetMember(memberId);

            
            if (_memberRepository.GetBorrowings(memberId) != null)
            {
                var borrowToDelete = _memberRepository.GetBorrowings(memberId);
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

            

            if (!_memberRepository.DeleteMember(memberToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting member.");
            }
            return NoContent();
        }
    }
}
