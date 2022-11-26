using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_app_hw.Models.Dto;
using web_app_hw.Services;
using web_app_hw.Services.Implementation;

namespace web_app_hw.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfMembershipController : ControllerBase
    {
        #region Services
        private readonly ITypeOfMembershipRepository _typeOfMembershipRepository;
        #endregion

        public TypeOfMembershipController(ITypeOfMembershipRepository typeOfMembershipRepository)
        {
            _typeOfMembershipRepository = typeOfMembershipRepository;
        }

        [HttpPost("Create")]
        public IActionResult CreateTypeOfMembershipClub([FromBody] TypeOfMembershipDto typeOfMembershipDto)
        {
            return Ok(_typeOfMembershipRepository.Create(new FitnessClub.Data.TypeOfMembership()
            {
                Level = typeOfMembershipDto.Level,
                Expired = typeOfMembershipDto.Expired,
                Money = typeOfMembershipDto.Money,
            }));
        }

        [HttpGet("get/all")]
        public IActionResult GetAllTypeOfMembershipClubs()
        {
            return Ok(_typeOfMembershipRepository.GetAll());
        }

        [HttpGet("get/{id}")]
        public IActionResult GetTypeOfMembershipById([FromRoute] int id)
        {
            return Ok(_typeOfMembershipRepository.GetById(id));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteTypeOfMembershipById([FromRoute] int id)
        {
            if (_typeOfMembershipRepository.Delete(id))
                return Ok();
            else
                return BadRequest();
        }

    }
}
