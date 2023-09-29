using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale.Models
{
    public class Violazione
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Il campo Description è obbligatorio")]
        public string Description { get; set; }
        public Violazione() { }

        public Violazione(string description)
        {
            Description = description;
        }
    }
}