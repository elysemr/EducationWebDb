using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationWebDb.Models
{
    public class Major
    {
        public int Id { get; set; }
        [StringLength(5), Required]
        public string Code { get; set; }
        [StringLength(50), Required]
        public string Description { get; set; }
        public int MinSat { get; set; }

        public virtual IEnumerable<Student> Students { get; set; } //need to do this when using fluent api


    public Major () { }

    }
}
