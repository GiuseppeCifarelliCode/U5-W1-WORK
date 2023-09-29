using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale.Models
{
    public class AnMag10Punti
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime DataViolazione { get; set; }
        public double Amount { get; set; }
        public int PuntiPersi { get; set; }

        public  AnMag10Punti() { }
        public AnMag10Punti(string surname, string name, DateTime dataViolazione, double amount, int puntiPersi)
        {
            Surname = surname;
            Name = name;
            DataViolazione = dataViolazione;
            Amount = amount;
            PuntiPersi = puntiPersi;
        }
    }
}