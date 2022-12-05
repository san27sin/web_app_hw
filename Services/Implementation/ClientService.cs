using Grpc.Core;
using web_app_hwProto;
using static web_app_hwProto.ClientService;

namespace web_app_hw.Services.Implementation
{
    public class ClientService : ClientServiceBase//наследуемся от виртуального класса созданного proto
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public override Task<CreateNewClientDtoResponce> CreateNewClient(CreateNewClientDto request, ServerCallContext context)
        {
            var id = _clientRepository.Create(new FitnessClub.Data.Client
            {
                Name = request.Name,
                Surname = request.Surname,
                BirthDay = default,
                FitnessClubId = request.FitnessClubId,
                TypeOfMembershipId = request.TypeOfMembershipId
            });

            CreateNewClientDtoResponce responce = new CreateNewClientDtoResponce();
            responce.Id = id;
            return Task.FromResult(responce);
        }

        public override Task<GetAllClientsResponce> GetAllClients(GetAllClientsRequest request, ServerCallContext context)
        {
            var response = new GetAllClientsResponce();



            response.Clients.AddRange(_clientRepository.GetAll().Select(el =>
            new web_app_hwProto.Client
            {
                Id = el.Id,
                Name = el.Name,
                Surname = el.Surname,
                BirthDay = default,
                FitnessClubId = el.FitnessClubId,
                TypeOfMembershipId = el.TypeOfMembershipId
            }).ToList());
            return Task.FromResult(response);
        }

        public override Task<DeleteClientByIdResponce> DeleteClientById(DeleteClientByIdRequest request, ServerCallContext context)
        {
            _clientRepository.Delete(request.Id);
            return Task.FromResult(new DeleteClientByIdResponce());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<GetClientByIdResponce> GetClientById(GetClientByIdRequest request, ServerCallContext context)
        {
            var result = _clientRepository.GetById(request.Id);
            return Task.FromResult(
                new GetClientByIdResponce()
                {
                     Client = new Client()
                    {
                         Id = result.Id,
                         Name = result.Name,
                         Surname = result.Surname,
                         BirthDay = default,
                         FitnessClubId = result.FitnessClubId,
                         TypeOfMembershipId = result.TypeOfMembershipId
                     }
                });
        }

        public override Task<UpdateClientResponce> UpdateClient(UpdateClientRequest request, ServerCallContext context)
        {
            _clientRepository.Update(
                new Models.Dto.ClientDto
                {
                    Name = request.Client.Name,
                    Surname = request.Client.Surname,
                    BirthDay = default,
                    FitnessClubId = request.Client.FitnessClubId,
                    TypeOfMembershipId = request.Client.TypeOfMembershipId
                }, request.Id);
            return Task.FromResult(new UpdateClientResponce());
        }
    }
}
