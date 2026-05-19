using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using System.Net.NetworkInformation;

namespace FlexGCCAssignment.Controllers
{
    [ApiController]
    [Route("api/work-requests")]
    public class WorkRequestsController : ControllerBase
    {
        private readonly IWorkRequestService _workRequestService;

        // Injecting the Service Layer, not the Database Context
        public WorkRequestsController(IWorkRequestService workRequestService)
        {
            _workRequestService = workRequestService;
        }

        // GET /api/work-requests
        [HttpGet]
        public async Task<IActionResult> GetRequests([FromQuery] Status? status, [FromQuery] string search)
        {
            var results = await _workRequestService.GetAllRequestsAsync(status, search);
            return Ok(results);
        }

        // GET /api/work-requests/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(Guid id)
        {
            var request = await _workRequestService.GetRequestByIdAsync(id);
            if (request == null)
                return NotFound(new { error = "Work request not found." });

            return Ok(request);
        }

        // POST /api/work-requests
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] WorkRequestDto dto)
        {
            // DTO Validation automatically managed by [ApiController]
            var createdRequest = await _workRequestService.CreateRequestAsync(dto);

            var responsePayload = new
            {
                message = $"Work request '{createdRequest.Title}' was created successfully.",
                data = createdRequest
            };

            // Omitting the route values (the id) completely
            return StatusCode(StatusCodes.Status201Created, responsePayload);
        }

        // PATCH /api/work-requests/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] Status status)
        {
            var wasUpdated = await _workRequestService.UpdateStatusAsync(id, status);

            if (!wasUpdated)
                return NotFound(new { error = "Work request not found or status update failed." });

            return NoContent();
        }
    

        // POST /api/work-requests/{id}/notes
        // Handles: Add a note to a work request
        [HttpPost("{id}/notes")]
        public async Task<IActionResult> AddNote(Guid id, [FromBody] NoteDto noteDto)
        {
            // The service should return the newly created Note DTO/Entity, or null if the WorkRequest doesn't exist
            var addedNote = await _workRequestService.AddNoteAsync(id, noteDto);

            if (addedNote == null)
                return NotFound(new { error = "Work request not found." });

            return Ok(new
            {
                message = "Note added successfully.",
                data = addedNote
            });
        }
    }
}
