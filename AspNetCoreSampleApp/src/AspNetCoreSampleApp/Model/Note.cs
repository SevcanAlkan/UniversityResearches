using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSampleApp.Model
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(500)]
        public string Content { get; set; }
    }
}
