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

        public int? AlbumId { get; set; }
        public virtual Album Album { get; set; }


    }
}
