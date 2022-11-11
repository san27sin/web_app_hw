using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_app_hw.Models.Requests;
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
        public IActionResult CreateNewClient([FromBody] CreateClientRequest createClientRequest)
        {
            return Ok(_clientRepository.Create(new Client()
            {                
                 Name = createClientRequest.Name,
                 Surname = createClientRequest.Surname,
                 BirthDay = createClientRequest.BirthDay,
            }));
        }

        [HttpGet("get/all")]
        public IActionResult GetAllClients()
        {
            return Ok(_clientRepository.GetAll());
        }

        [HttpGet("get/{id}")]
        public IActionResult GetClientById([FromRoute] int id)
        {
            return Ok(_clientRepository.GetById(id));
        }
    }
}
