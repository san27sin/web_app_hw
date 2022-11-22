
using FitnessClub.Data;
using web_app_hw.Models.Dto;

namespace web_app_hw.Services.Implementation
{
    public class FitnessClubRepository : IFitnessClubRepository
    {
        #region services
        private readonly FitnessClubDb _db;
        #endregion

        public FitnessClubRepository(FitnessClubDb db)
        {
            _db = db;
        }


        public int Create(FitnessClub.Data.FitnessClub data)
        {
           _db.Add(data);
           _db.SaveChanges();
            return data.Id; 
        }

        public bool Delete(int id)
        {
            var fitnessClub = GetById(id);
            if(fitnessClub != null)
            {
                _db.FitnessClubs.Remove(fitnessClub);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public IList<FitnessClub.Data.FitnessClub> GetAll()
        {
            return _db.FitnessClubs.ToList();
        }

        public FitnessClub.Data.FitnessClub GetById(int id)
        {
           return _db.FitnessClubs.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(FitnessClubDto data, int id)
        {
            var fitnessClub = GetById(id);
            if (fitnessClub != null)
            {
                fitnessClub.Rank = data.Rank;
                fitnessClub.Location = data.Location;
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
