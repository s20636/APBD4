﻿using System.ComponentModel.DataAnnotations;

namespace Cwiczenia4.Models
{
    public class Animal
    {

        public int IdAnimal { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Area { get; set; }
    }
}
