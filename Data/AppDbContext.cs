using Microsoft.EntityFrameworkCore;
using TicketSystemApi.Models;

namespace TicketSystemApi.Data
{
    public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Agent> Agents { get; set; }
  }
}