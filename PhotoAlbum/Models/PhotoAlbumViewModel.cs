using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoAlbum.Models
{
    public class PhotoAlbumViewModel
    {
        [Display(Name = "Available Albums:")]
        public int SelectedAlbumId { get; set; }
        public List<Album> Albums { get; set; }

        public IEnumerable<SelectListItem> AvailableAlbums
        {
            get { return new SelectList(Albums, "id", "title"); }
        }
    }
}