using FitnessClub.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using web_app_hw.Controllers;
using web_app_hw.Models.Dto;
using web_app_hw.Services;
using web_app_hw.Services.Implementation;

namespace FitnessServiceTest
{
    //������ �������� ����� � clientcontroller
    /*1)class ������������ ���������
     *2)���������� ������ ������������
     *3)������ ����� ������������ ������ ���� ������� ���������
     */
    public class ClientControllerTest
    {
        private readonly ClientController _clientController;
        private readonly Mock<IClientRepository> _mockClientRepository;


        public ClientControllerTest()
        {
            _mockClientRepository = new Mock<IClientRepository>();

            _clientController = new ClientController(_mockClientRepository.Object);//������� ������ ��������
        }

        [Theory]//
        [InlineData(new ClientDto()  {Name = "Test",Surname = "Surname", BirthDay = new DateTime(1997,10,08),FitnessClubId = 2,TypeOfMembershipId = 2})]//������ ������� ��������� ��� ������������
        public void CreateNewClientTest(ClientDto createClientDto)
        {
            var result = _clientController.CreateNewClient(createClientDto);
            Assert.IsAssignableFrom<ActionResult<int>>(result);
        }

        [Fact]//������� ��������� ��� ����������� �����
        public void GetAllClientsTest()
        {
            //1) ���������� ������
            _mockClientRepository.Setup(repository =>
            repository.GetAll()).Returns(new List<Client>());//���������� ������ � ������ ������������

            //2) ������������� ���������� ������������ ������
            var result = _clientController.GetAllClients();
            _mockClientRepository.Verify(repository=>
            repository.GetAll(),Times.Once());//� ������ ���������� ���������� ���������� ��� ����� ������ ��� ���� ������ ���� ��� ��� ���������

            //3) ���������� ���������� ���������� ������ � �������� �� ����������
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