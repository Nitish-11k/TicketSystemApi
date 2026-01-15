using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketSystemApi.Data;
using TicketSystemApi.Models;
using System.Linq;
using TicketSystemApi.DTOs;

namespace TicketSystemApi.Services
{
    public class TicketService
    {
      private readonly AppDbContext _context;

      public TicketService(AppDbContext context)
      {
          _context = context;
      }

      public async Task<List<Ticket>> GetAllTicketsAsync()
      {
          return await _context.Tickets.Include(t => t.Agent).ToListAsync();
      }

      public async Task<Ticket?> GetTicketByIdAsync(int id)
      {
          return await _context.Tickets.Include(t => t.Agent).FirstOrDefaultAsync(t => t.Id == id);
      }

      public async Task<Ticket> CreateTicketAsync(CreateTicketDto dto)
      {
          var ticket = new Ticket
          {
              Title = dto.Title,
              Description = dto.Description,
              Priority = (TicketPriority)dto.Priority,
              Status = TicketStatus.Open,
              CreatedAt = DateTime.UtcNow
              
          };

          _context.Tickets.Add(ticket);
          await _context.SaveChangesAsync();
          return ticket;
      }

      public async Task AssignAgentAsync(int ticketId, int agentId)
      {
          var ticket = await _context.Tickets.FindAsync(ticketId);
          if (ticket == null)
          {
              throw new KeyNotFoundException("Ticket not found");
          }
          if(ticket.Status == TicketStatus.Closed)
          {
              throw new InvalidOperationException("Cannot assign agent to a closed ticket");
          }
          
          var agent = await _context.Agents.FindAsync(agentId);
          if (agent == null)
          {
              throw new KeyNotFoundException("Agent not found"); 
          }
          ticket.AgentId = agentId;
          ticket.Status = TicketStatus.InProgress;
          await _context.SaveChangesAsync();
      }

      public async Task CloseTicketAsync(int id)
      {
          var ticket = await _context.Tickets.FindAsync(id);
          if (ticket == null)
          {
              throw new KeyNotFoundException("Ticket not found");
          }
          ticket.Status = TicketStatus.Closed;
          ticket.ClosedAt = DateTime.UtcNow;
          await _context.SaveChangesAsync();
      }
    }
}