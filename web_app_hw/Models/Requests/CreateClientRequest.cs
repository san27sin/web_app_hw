

using FitnessClub.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web_app_hw.Models.Requests
{
    public class CreateClientRequest
    {        
        public int Id { get; set; } //автоматическое создание
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
        public string Membership { get; set; }
    }
}
