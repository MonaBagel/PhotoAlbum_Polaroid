using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Polaroid_Proj.Models.Gallery
{
    public class Photo : GalleryItem
    {
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CapturedDate { get; set; }

        public int? AlbumId { get; set; }
        public virtual Album Album { get; set; }


    }
}
