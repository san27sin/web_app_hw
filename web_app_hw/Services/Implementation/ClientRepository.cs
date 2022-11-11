using FitnessClub.Data;
using web_app_hw.Models.Dto;

namespace web_app_hw.Services.Implementation
{
    public class ClientRepository : IClientRepository
    {
        #region region

        private readonly FitnessClubDb _db;

        #endregion 

        public ClientRepository(FitnessClubDb db)
        {
            _db = db;
        }

        public int Create(Client data)
        {
            
        }

        public bool Delete(int id)
        {
            
        }

        public IList<Client> GetAll()
        {
            
        }

        public Client GetById(int id)
        {
            

        public bool Update(Client data)
        {
            
        }
    }
}
