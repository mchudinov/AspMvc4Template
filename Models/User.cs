using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            DateTimeCreated = DateTime.Now;
            Widgets = new HashSet<Widget>();
        }

        [Key]
        public Guid Id  { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Must be at least 5 charachters", MinimumLength = 5)]
        [RegularExpression(@"^[^@]+@[^@]+\.[^@]+$", ErrorMessage = "Email must be correct formatted")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Must be at least 5 charachters", MinimumLength = 5)]
        [RegularExpression(@"^[A-Za-z0-9_]+$", ErrorMessage = "Nickname must contain only letters a-z, numbers and underscore _")]
        public string Nickname { get; set; }

        [Required]
        public DateTime DateTimeCreated { get; set; }

        public virtual ICollection<Widget> Widgets { get; set; }
    }
}
