using FitnessClub.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using web_app_hw.Controllers;
using web_app_hw.Models.Dto;
using web_app_hw.Services;
using web_app_hw.Services.Implementation;

namespace FitnessServiceTest
{
    //делаем тестовый класс к clientcontroller
    /*1)class тестировани€ публичный
     *2)подключаем проект тестировани€
     *3)каждый метод тестировани€ должен быть помечен атрибутом
     *
     *библиотека Xunit priority - позвол€ет нам выставить приоритетность выполнени€
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
        [InlineData(new ClientDto() { Name = "Test", Surname = "Surname", BirthDay = new DateTime(1997, 10, 08), FitnessClubId = 2, TypeOfMembershipId = 2 })]//задаем входные параметры дл€ тестировани€
        public void CreateNewClientTest(ClientDto createClientDto)
        {
            _mockClientRepository.Setup(repository =>
            repository.Create(It.IsAny<Client>())).Verifiable();//ѕроверка на верификацию
            var result = _clientController.CreateNewClient(createClientDto);//создание метода на контроллере
            //Assert.IsAssignableFrom<ActionResult<int>(result);
            _mockClientRepository.Verify(repository =>
            repository.Create(It.IsAny<Client>()), Times.Once);
        }

        [Fact]//атрибут указывает нам тестируемый метод
        public void GetAllClientsTest()
        {
            //1) подготовка данных
            _mockClientRepository.Setup(repository =>
            repository.GetAll()).Returns(new List<Client>());//подготовка данных в рамках тестировани€

            //2) использование исполнение тестируемого метода
            var result = _clientController.GetAllClients();
            _mockClientRepository.Verify(repository =>
            repository.GetAll(), Times.Once());//в рамках выполнени€ придыдущей инструкции наш метод должен был быть вызван один раз или несколько

            //3) подготовка эталонного результата метода и проверка на валидность
            //Assert.IsAssignableFrom <ActionResult<IList<Client>>>(result);
        }
    }
}    
}