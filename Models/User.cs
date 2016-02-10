using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        [Key]
        public Guid Id  { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Must be at least 5 charachters", MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Must be at least 5 charachters", MinimumLength = 5)]
        public string Nickname { get; set; }

        [Required]
        public DateTime DateTimeCreated { get; set; }

        public IEnumerable<Widget> Widgets { get; set; }
    }
}
