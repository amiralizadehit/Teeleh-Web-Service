using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teeleh.Models.ViewModels;

namespace Teeleh.Models.Dtos
{
    public class StringPairDto
    {
        [Required]
        public SessionInfoObject session { get; set; }

        [Required]
        public string StringId { get; set; }
    }
}
