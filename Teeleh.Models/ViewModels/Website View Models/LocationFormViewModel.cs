using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Teeleh.Models.Enums;

namespace Teeleh.Models.ViewModels.Website_View_Models
{
    public class LocationFormViewModel
    {
        
        [DisplayName("Parent Location")]
        public IEnumerable<Location> Parents { get; set; }

        public int ParentId { get; set; }

        [Required]
        public LocationType Type { get; set; }

        [Required]
        public HttpPostedFileBase ExcelFile { get; set; }
    }
}
