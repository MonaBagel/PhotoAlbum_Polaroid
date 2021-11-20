using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Polaroid_Proj.Models.Gallery
{
    public class PhotoModel : GalleryItem
    {
        public string ImageName { get; set; }

        public int? AlbumId { get; set; }
        public virtual Album Album { get; set; }

        /*public string FileName { get; set; }*/

        [NotMapped]
        [DisplayName("Upload Photo")]
        [Required(ErrorMessage = "Please upload a photo.")]
        public IFormFile GalleryPhoto { get; set; }
    }
}
