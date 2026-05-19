using Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DataLayer
{
    public class WorkRequestRepository : IWorkRequestRepository
    {
        private readonly WorkspaceDbContext _context;

        public WorkRequestRepository(WorkspaceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkRequest>> GetFilteredAsync(Status? status, string search)
        {
            var query = _context.WorkRequests.AsQueryable();

            if (status.HasValue)
                query = query.Where(w => w.Status == status.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(w => w.Title.Contains(search) || w.ClientName.Contains(search));

            return await query.OrderByDescending(w => w.CreatedDate).ToListAsync();
        }

        public async Task<WorkRequest?> GetByIdAsync(Guid id)
        {
            return await _context.WorkRequests
                .Include(w => w.Notes)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task AddAsync(WorkRequest workRequest)
        {
            await _context.WorkRequests.AddAsync(workRequest);
        }

        public  Task UpdateAsync(WorkRequest workRequest)
        {
            // Entity Framework tracks changes automatically if entities are attached,
            // but explicit updates ensure detached entities are handled correctly.
            _context.WorkRequests.Update(workRequest);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        // Add this inside your WorkRequestRepository class
        public async Task AddNoteAsync(Note note)
        {
            // EF Core is smart enough to figure out which DbSet this belongs to
            await _context.AddAsync(note);
        }
    }
}
