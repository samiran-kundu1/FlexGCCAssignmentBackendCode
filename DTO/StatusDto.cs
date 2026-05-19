using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text;

namespace DTO
{
    public class StatusDto
    {
        [Required, EnumDataType(typeof(StatusDTO))] public StatusDTO Status { get; set; }
    }
}
