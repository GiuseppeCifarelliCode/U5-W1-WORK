using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale.Models
{
    public class Verbale
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Il campo DataViolazione è obbligatorio")]
        public DateTime DataViolazione { get; set; }
        [Required(ErrorMessage = "Il campo Indirizzo è obbligatorio")]
        public string IndirizzoViolazione { get; set; }
        [Required(ErrorMessage = "Il campo Agente è obbligatorio")]
        public string Agente { get; set; }
        [Required(ErrorMessage = "Il campo DataVerbale è obbligatorio")]
        public DateTime DataVerbale { get; set; }
        [Required(ErrorMessage = "Il campo Importo è obbligatorio")]
        public double Importo { get; set; }
        [Required(ErrorMessage = "Il campo PuntiTolti è obbligatorio")]
        public int PuntiTolti { get; set; }
        [Required(ErrorMessage = "Il campo Trasgressore è obbligatorio")]
        [Display(Name = "Trasgressore")]
        public int IdTrasgressore { get; set; }
        [Required(ErrorMessage = "Il campo Violazione è obbligatorio")]
        [Display(Name = "Violazione")]
        public int IdViolazione { get; set; }

        public Verbale() { }
        public Verbale(DateTime dataViolazione, string indirizzoViolazione, string agente, DateTime dataVerbale, double importo, int puntiTolti, int idTrasgressore, int idViolazione)
        {
            DataViolazione = dataViolazione;
            IndirizzoViolazione = indirizzoViolazione;
            Agente = agente;
            DataVerbale = dataVerbale;
            Importo = importo;
            PuntiTolti = puntiTolti;
            IdTrasgressore = idTrasgressore;
            IdViolazione = idViolazione;
        }
    }
}