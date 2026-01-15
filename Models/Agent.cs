using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TicketSystemApi.Models
{
    public class Agent
  {
        [Key]
        public int Id {get; set;}

        [Required]
        public string Name {get; set;} = string.Empty;

        [Required]
        [EmailAddress]

        public string Email {get; set;} = string.Empty;

        [JsonIgnore]

        public List<Ticket> AssignedTickets {get; set;} = new();
  }
}