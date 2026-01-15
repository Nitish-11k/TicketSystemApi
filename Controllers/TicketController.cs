using Microsoft.AspNetCore.Mvc;
using TicketSystemApi.Services;
using TicketSystemApi.DTOs;
using TicketSystemApi.Models;
using System.Threading.Tasks;
using System.Linq;


namespace TicketSystemApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
  {
    private readonly TicketService _ticketService;

    public TicketController(TicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      return Ok(await _ticketService.GetAllTicketsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var ticket = await _ticketService.GetTicketByIdAsync(id);
      return ticket  == null ? NotFound() : Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketDto ticketCreateDto)
    {
      var createdTicket = await _ticketService.CreateTicketAsync(ticketCreateDto);
      return CreatedAtAction(nameof(GetById), new { id = createdTicket.Id }, createdTicket);
    }

    [HttpPut("{id}/assign/{agentId}")]
        public async Task<IActionResult> AssignAgent(int id, int agentId)
        {
            
            await _ticketService.AssignAgentAsync(id, agentId);
            return Ok(new { message = "Agent assigned successfully" });
        }

        [HttpPatch("{id}/close")]
        public async Task<IActionResult> CloseTicket(int id)
        {
            await _ticketService.CloseTicketAsync(id);
            return Ok(new { message = "Ticket closed" });
        }
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] TicketStatus status)
        {
          await _ticketService.UpdateStatusAsync(id, status);
          return Ok(new { message = "Status updated successfully" });
        }
  }
}