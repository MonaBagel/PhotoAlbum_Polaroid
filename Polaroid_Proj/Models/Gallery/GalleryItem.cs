﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Polaroid_Proj.Models.Gallery
{
    public class GalleryItem
    {
        [Key]
        public int GalleryItemId { get; set; }
        public int UserId { get; set; }

        [MaxLength(20)]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}