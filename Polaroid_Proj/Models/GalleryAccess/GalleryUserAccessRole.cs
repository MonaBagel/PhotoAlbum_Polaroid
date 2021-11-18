using Polaroid_Proj.Models.Gallery;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Polaroid_Proj.Models.GalleryAccess
{
    public class GalleryUserAccessRole
    {
        [Key]
        public int UserAccessRoleId { get; set; }

        public int? GalleryItemId { get; set; }
        public virtual GalleryItem GalleryItem { get; set; }


        public int? AccessRoleId { get; set; }
        public virtual GalleryAccessRole AccessRole { get; set; }


    }
}
