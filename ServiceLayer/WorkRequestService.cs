using System;
using System.Collections.Generic;
using System.Text;
    using AutoMapper;
    using DataLayer;
    using DTO;
    using Entities;

namespace ServiceLayer
{

    public class WorkRequestService : IWorkRequestService
    {
        private readonly IWorkRequestRepository _repository;
        private readonly IMapper _mapper;

        // Inject IMapper alongside your repository
        public WorkRequestService(IWorkRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkRequestDto>> GetAllRequestsAsync(Status? status, string search)
        {
            var entities = await _repository.GetFilteredAsync(status, search);

            // Map list of entities to list of response DTOs
            return _mapper.Map<IEnumerable<WorkRequestDto>>(entities);
        }

        public async Task<WorkRequestDto?> GetRequestByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return _mapper.Map<WorkRequestDto>(entity);
        }

        public async Task<WorkRequestDto> CreateRequestAsync(WorkRequestDto dto)
        {
            var entity = _mapper.Map<WorkRequest>(dto);

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<WorkRequestDto>(entity);
        }

        public async Task<bool> UpdateStatusAsync(Guid id, Status newStatus)
        {
            var request = await _repository.GetByIdAsync(id);
            if (request == null) return false;

            request.Status = newStatus;
            request.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(request);
            return await _repository.SaveChangesAsync();
        }


        public async Task<NoteDto?> AddNoteAsync(Guid workRequestId, NoteDto noteDto)
        {
            var workRequest = await _repository.GetByIdAsync(workRequestId);
            if (workRequest == null)
            {
                return null;
            }

            var note = _mapper.Map<Note>(noteDto);

            note.WorkRequestId = workRequestId;
            note.CreatedDate = DateTime.UtcNow;

            await _repository.AddNoteAsync(note);
            await _repository.SaveChangesAsync();

            return _mapper.Map<NoteDto>(note);
        }
    }
}
