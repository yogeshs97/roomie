using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Models
{
    public class AccountLogin
    {
        [Key]
        [Display(Name ="ID")]
        public int Id { get; set; }

        [Required(ErrorMessage ="Username cannot be empty")]
        [Display(Name ="username")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password cannot be empty")]
        [Display(Name ="password")]
        public string Password { get; set; }

    }
}
