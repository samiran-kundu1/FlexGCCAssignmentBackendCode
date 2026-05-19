using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    // Enums
    public enum Priority { Low, Medium, High }
    public enum Status { New, InProgress, Blocked, Completed }

    // Entity
    public class WorkRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; } = Status.New;
        public DateTime? DueDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public List<Note> Notes { get; set; } = new();
    }
}
