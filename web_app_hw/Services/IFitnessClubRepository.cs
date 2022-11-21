using FitnessClub.Data;
using web_app_hw.Models.Dto;

namespace web_app_hw.Services
{
    public interface IFitnessClubRepository:IRepository<FitnessClub.Data.FitnessClub, int,FitnessClubDto>
    {

    }
}
