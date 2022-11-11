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
            _db.Clients.Add(data);
            _db.SaveChanges();
            return data.Id;
        }

        public bool Delete(int id)
        {
            Client client = GetById(id);
            if (client != null)
            {
                _db.Clients.Remove(client);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public IList<Client> GetAll()
        {
            return _db.Clients.ToList();
        }

        public Client GetById(int id)
        {
            return _db.Clients.FirstOrDefault(x => x.Id == id);
        }
            

        public bool Update(Client data)
        {
            Client client = GetById(data.Id);
            if (client != null)
            {
                client = data;
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
