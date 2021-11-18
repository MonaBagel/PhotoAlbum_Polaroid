using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polaroid_Proj.Models.Gallery
{
    public class Album : GalleryItem
    {
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
