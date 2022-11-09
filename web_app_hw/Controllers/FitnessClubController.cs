using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_app_hw.Models.Requests;
using web_app_hw.Services;

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
        public IActionResult CreateFitnessClub([FromBody] FitnessClubRequest fitnessClubRequest)
        {
            return Ok(_fitnessClubRepository.Create(new models.FitnessClub()
            {
                Rank = fitnessClubRequest.Rank,
                Location = fitnessClubRequest.Location
            }));
        }

        [HttpGet("Get/all")]
        public IActionResult GetAllFitnessClubs()
        {
            return Ok(_fitnessClubRepository.GetAll());
        }

    }
}
