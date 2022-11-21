using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Data
{
    public class FitnessClubDb:DbContext
    {
        //class DbSet - который описывает конкретную таблицу
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<FitnessClub> FitnessClubs { get; set; } = null!;
        public DbSet<TypeOfMembership> TypesOfMembership { get; set; } = null!;
        public DbSet<Account> Account { get; set; } = null!;
        public DbSet<AccountSession> AccountSession { get; set; } = null!;

        public FitnessClubDb(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

    }
}