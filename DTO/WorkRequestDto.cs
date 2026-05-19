using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public enum PriorityDTO { Low, Medium, High }
    public enum StatusDTO { New, InProgress, Blocked, Completed }
    public class WorkRequestDto
    {
        [Required, MaxLength(150)] public string Title { get; set; }
        [Required, MaxLength(100)] public string ClientName { get; set; }
        public string Description { get; set; }
        [Required, EnumDataType(typeof(PriorityDTO))] public PriorityDTO PriorityDto { get; set; }
        [Required, EnumDataType(typeof(StatusDTO))] public StatusDTO StatusDto { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
