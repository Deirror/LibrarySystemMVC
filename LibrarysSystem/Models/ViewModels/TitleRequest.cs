using LibrarysSystem.Models.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Web;

namespace LibrarysSystem.Models.ViewModels
{
    public class TitleRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public int Year { get; set; }

        public string? ISBN { get; set; }

        public string Type { get; set; }


        public string ImageName { get; set; }

        public string? Publisher { get; set; }

        public int? PublishedYear { get; set; }
      
        public string sectionName { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public static IFormFile ImageFile { get; set; }
    }
}
