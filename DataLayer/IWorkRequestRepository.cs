using Entities;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace DataLayer
{
    public interface IWorkRequestRepository
    {
        Task<IEnumerable<WorkRequest>> GetFilteredAsync(Status? status, string search);
        Task<WorkRequest?> GetByIdAsync(Guid id);
        Task AddAsync(WorkRequest workRequest);
        Task UpdateAsync(WorkRequest workRequest);
        Task<bool> SaveChangesAsync();

        Task AddNoteAsync(Note note);
    }
}
