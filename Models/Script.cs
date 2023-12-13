using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps_Site.Models
{
    public class Script
    {
        public int ScriptID { get; set; }
        [Required]
        [Display(Name = "Script Name")]
        [StringLength(35)]
        public string ScriptName { get; set; }
        [Required]
        [Display(Name = "Script Description")]
        [StringLength(300)]
        public string ScriptDescription { get; set; }
        [Required]
        [Display(Name = "Script Content")]
        [StringLength(1200)]
        public string ScriptContent { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Author Date")]
        public DateTime AuthorDate { get; set; }
        [Required]
        public string Author { get; set; }
    }
}