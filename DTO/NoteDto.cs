using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class NoteDto
    {
        [Required] public string Content { get; set; }
    }
}
