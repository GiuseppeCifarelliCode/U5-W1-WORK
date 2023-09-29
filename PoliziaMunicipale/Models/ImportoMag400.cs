using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale.Models
{
    public class ImportoMag400
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime DataViolazione { get; set; }
        public double Amount { get; set; }
        public int Points { get; set; }
        public ImportoMag400() { }
        public ImportoMag400(string surname, string name, DateTime dataViolazione, double amount, int points)
        {
            Surname = surname;
            Name = name;
            DataViolazione = dataViolazione;
            Amount = amount;
            Points = points;
        }
    }
}