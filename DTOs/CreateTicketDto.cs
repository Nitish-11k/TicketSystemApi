using System.ComponentModel.DataAnnotations;

namespace TicketSystemApi.DTOs
{
    public class CreateTicketDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; }
        // public int AssignedUserId { get; set; }
    }
}