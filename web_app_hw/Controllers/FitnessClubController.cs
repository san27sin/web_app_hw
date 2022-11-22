using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_app_hw.Models.Dto;
using web_app_hw.Services;
using web_app_hw.Services.Implementation;

namespace web_app_hw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FitnessClubController : ControllerBase
    {
        #region Services
        private readonly IFitnessClubRepository _fitnessClubRepository;
        #endregion

        public FitnessClubController(IFitnessClubRepository fitnessClubRepository)
        {
            _fitnessClubRepository = fitnessClubRepository;
        }

        [HttpPost("Create")]
        public IActionResult CreateFitnessClub([FromBody] FitnessClubDto fitnessClubDto)
        {
            return Ok(_fitnessClubRepository.Create(new FitnessClub.Data.FitnessClub()
            {
                Rank = fitnessClubDto.Rank,
                Location = fitnessClubDto.Location
            }));
        }

        [HttpGet("Get/all")]
        public IActionResult GetAllFitnessClubs()
        {
            return Ok(_fitnessClubRepository.GetAll());
        }

        [HttpGet("get/{id}")]
        public IActionResult GetFitnessById([FromRoute] int id)
        {
            return Ok(_fitnessClubRepository.GetById(id));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteFitnessById([FromRoute] int id)
        {
            if (_fitnessClubRepository.Delete(id))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("Update")]
        public IActionResult UpdateFitnessClub([FromBody] FitnessClubDto updateFitnessClub, int id)
        {
            if (_fitnessClubRepository.Update(updateFitnessClub, id))
                return Ok();
            else
                return BadRequest();
        }
    }
}
