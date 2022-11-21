using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Data
{
    [Table("AccountSession")]
    public class AccountSession
    {
        //Сессионный ключ/токен
        //Сессия объединяет в себе пользователя и его ключик 
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        [Required]
        public string SessionToken { get; set; }

        public int AccountId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime TimeCreated { get; set; }
        
        public bool IsClosed { get; set; } //сосояние сессии, завершена или нет 


        [Column(TypeName = "datetime2")]
        public DateTime TimeClosed { get; set; }
        public virtual Account Account { get; set; }
    }
}
