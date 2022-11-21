using FitnessClub.Data;
using web_app_hw.Models.Dto;

namespace web_app_hw.Services.Implementation
{
    public class TypeOfMembershipRepository : ITypeOfMembershipRepository
    {
        #region Services
        private readonly FitnessClubDb _db;
        #endregion

        public TypeOfMembershipRepository(FitnessClubDb db)
        {
            _db = db;   
        }

        public int Create(TypeOfMembership data)
        {
            _db.Add(data);
            _db.SaveChanges();
            return data.Id;
        }

        public bool Delete(int id)
        {
            var typeOfMembership = GetById(id);
            if (typeOfMembership != null)
            {
                _db.TypesOfMembership.Remove(typeOfMembership);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public IList<TypeOfMembership> GetAll()
        {
            return _db.TypesOfMembership.ToList();
        }

        public TypeOfMembership GetById(int id)
        {
            return _db.TypesOfMembership.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(TypeOfMembershipDto data, int id)
        {
            var typeOfMembership = GetById(id);
            if (typeOfMembership != null)
            {
                typeOfMembership.Level = data.Level;
                typeOfMembership.Money = data.Money;
                typeOfMembership.Expired = data.Expired;
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
