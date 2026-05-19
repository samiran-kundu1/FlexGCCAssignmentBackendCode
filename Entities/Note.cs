using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Note
    {
        public Guid Id { get; set; }

        public Guid WorkRequestId { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        // Navigation property
        public WorkRequest WorkRequest { get; set; } = null!;
    }
}
