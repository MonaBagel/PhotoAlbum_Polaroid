using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polaroid_Proj.Models.Gallery
{
    public class AlbumModel : GalleryItem
    {
        public virtual ICollection<PhotoModel> Photos { get; set; }
    }
}
