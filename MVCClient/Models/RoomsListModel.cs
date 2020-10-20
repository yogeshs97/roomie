using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class RoomsListModel
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [Display(Name = "Area/Location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Space Count is required")]
        [Display(Name = "Space Vacant")]
        public int SpaceCount { get; set; }

        [Display(Name = "Rent(optional)")]
        public int Rent { get; set; }

        [Display(Name = "Description(optional)")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [Display(Name = "Contact Number")]
        public string ContactNo { get; set; }
    }
}
