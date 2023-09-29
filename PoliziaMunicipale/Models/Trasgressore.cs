using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale.Models
{
    public class Trasgressore
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Il campo Surname è obbligatorio")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Il campo Name è obbligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Il campo Address è obbligatorio")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Il campo City è obbligatorio")]
        public string City { get; set; }
        [Required(ErrorMessage = "Il campo CAP è obbligatorio")]
        public int CAP { get; set; }
        [Required(ErrorMessage = "Il campo CF è obbligatorio")]
        public string CF { get; set; }

        public Trasgressore() { }
        public Trasgressore(string surname, string name, string address, string city, int cAP, string cF)
        {
            Surname = surname;
            Name = name;
            Address = address;
            City = city;
            CAP = cAP;
            CF = cF;
        }
    }
}