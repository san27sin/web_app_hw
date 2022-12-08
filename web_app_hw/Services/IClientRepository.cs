using FitnessClub.Data;
using web_app_hw.Models.Dto;

namespace web_app_hw.Services
{
    public interface IClientRepository:IRepository<Client, int,ClientDto>
    {

    }
}
