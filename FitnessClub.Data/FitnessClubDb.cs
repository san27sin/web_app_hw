using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Data
{
    public class FitnessClubDb:DbContext
    {
        //class DbSet - который описывает конкретную таблицу

        public DbSet<Client> Clients { get; set; }
        public DbSet<FitnessClub> FitnessClubs { get; set; }
        public DbSet<TypeOfMembership> TypesOfMembership { get; set; }


        public FitnessClubDb(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

    }
}