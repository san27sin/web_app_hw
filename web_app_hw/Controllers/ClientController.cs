using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_app_hw.Models.Dto;
using web_app_hw.Services;
using FitnessClub.Data;


namespace web_app_hw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        #region Services
        private readonly IClientRepository _clientRepository;
        #endregion

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost("Create")]
        public IActionResult CreateNewClient([FromBody] ClientDto createClientDto)
        {
            return Ok(_clientRepository.Create(new Client()
            {
                Name = createClientDto.Name,
                Surname = createClientDto.Surname,
                BirthDay = createClientDto.BirthDay,
                FitnessClubId = createClientDto.FitnessClubId,
                TypeOfMembershipId = createClientDto.TypeOfMembershipId
            }));
        }

        [HttpGet("Get/all")]
        public IActionResult GetAllClients()
        {
            return Ok(_clientRepository.GetAll());
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetClientById([FromRoute] int id)
        {
            return Ok(_clientRepository.GetById(id));
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteClientById([FromRoute] int id)
        {
            if (_clientRepository.Delete(id))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("Update")]
        public IActionResult UpdateClient([FromBody] ClientDto updateClientDto, int id)
        {
            if (_clientRepository.Update(updateClientDto, id))
                return Ok();
            else
                return BadRequest();
        }

    }
}
