using FitnessClub.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using web_app_hw.Controllers;
using web_app_hw.Models.Dto;
using web_app_hw.Services;
using web_app_hw.Services.Implementation;

namespace FitnessServiceTest
{
    //делаем тестовый класс к clientcontroller
    /*1)class тестирования публичный
     *2)подключаем проект тестирования
     *3)каждый метод тестирования должен быть помечен атрибутом
     */
    public class ClientControllerTest
    {
        private readonly ClientController _clientController;
        private readonly Mock<IClientRepository> _mockClientRepository;


        public ClientControllerTest()
        {
            _mockClientRepository = new Mock<IClientRepository>();

            _clientController = new ClientController(_mockClientRepository.Object);//создаем объект заглушку
        }

        [Theory]//
        [InlineData(new ClientDto()  {Name = "Test",Surname = "Surname", BirthDay = new DateTime(1997,10,08),FitnessClubId = 2,TypeOfMembershipId = 2})]//задаем входные параметры для тестирования
        public void CreateNewClientTest(ClientDto createClientDto)
        {
            var result = _clientController.CreateNewClient(createClientDto);
            Assert.IsAssignableFrom<ActionResult<int>>(result);
        }

        [Fact]//атрибут указывает нам тестируемый метод
        public void GetAllClientsTest()
        {
            //1) подготовка данных
            _mockClientRepository.Setup(repository =>
            repository.GetAll()).Returns(new List<Client>());//подготовка данных в рамках тестирования

            //2) использование исполнение тестируемого метода
            var result = _clientController.GetAllClients();
            _mockClientRepository.Verify(repository=>
            repository.GetAll(),Times.Once());//в рамках выполнения придыдущей инструкции наш метод должен был быть вызван один раз или несколько

            //3) подготовка эталонного результата метода и проверка на валидность
            Assert.IsAssignableFrom<ActionResult<IList<Client>>>(result);
        }

      
        public IActionResult GetClientByIdTest([FromRoute] int id)
        {
            return Ok(_clientRepository.GetById(id));
        }

        public void DeleteClientByIdTest([FromRoute] int id)
        {
            if (_clientRepository.Delete(id))
                return Ok();
            else
                return BadRequest();
        }

        public void UpdateClientTest([FromBody] ClientDto updateClientDto, int id)
        {
            if (_clientRepository.Update(updateClientDto, id))
                return Ok();
            else
                return BadRequest();
        }
    }    
}