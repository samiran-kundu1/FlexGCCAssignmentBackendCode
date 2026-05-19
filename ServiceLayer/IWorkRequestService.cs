using DTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace ServiceLayer
{
    public interface IWorkRequestService
    {
        Task<IEnumerable<WorkRequestDto>> GetAllRequestsAsync(Status? status, string search);
        Task<WorkRequestDto?> GetRequestByIdAsync(Guid id);
        Task<WorkRequestDto> CreateRequestAsync(WorkRequestDto dto);
        Task<bool> UpdateStatusAsync(Guid id, Status newStatus);
        Task<NoteDto?> AddNoteAsync(Guid workRequestId, NoteDto noteDto);
    }
}
