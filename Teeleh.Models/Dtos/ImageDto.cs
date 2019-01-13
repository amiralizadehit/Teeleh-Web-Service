using Teeleh.Models.Enums;

namespace Teeleh.Models.Dtos
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public ImageType Type { get; set; }

        
    }

}
