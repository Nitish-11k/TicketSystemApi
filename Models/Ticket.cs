using System.ComponentModel.DataAnnotations;

namespace TicketSystemApi.Models
{
    public enum TicketStatus
  {
        Open,
        InProgress,
        Closed,
        Resolved
  }

    public enum TicketPriority
  {
        Low,
        Medium,
        High,
        Urgent
  }
    
    public class Ticket
  {
        [Key]
        public int Id {get; set;}

        [Required]
        [MaxLength(100)]
        public string Title {get; set;} = string.Empty;

        public string Description {get; set;} = string.Empty;

        public TicketStatus Status {get; set;} = TicketStatus.Open;

        public TicketPriority Priority {get; set;} 

        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }

        public DateTime? AssignedAt { get; set; }

        public int? AgentId {get; set;}

        public Agent? Agent {get; set;}
  }
}