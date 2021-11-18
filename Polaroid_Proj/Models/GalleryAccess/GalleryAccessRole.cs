using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Polaroid_Proj.Models.GalleryAccess
{
    public class GalleryAccessRole
    {
        [Key]
        public int AccessRoleId { get; set; }

        [MaxLength(20)]
        public string AccessLevelTitle { get; set; }

        [MaxLength(255)]
        public string AccessRoleDecription { get; set; }
    }
}
