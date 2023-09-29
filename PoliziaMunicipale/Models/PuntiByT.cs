using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale.Models
{
    public class PuntiByT
    {
        public int IdT { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public int TotPuntiPersi { get; set; }

        public PuntiByT() { }
        public PuntiByT(int idT, string surname, string name, int totPuntiPersi)
        {
            IdT = idT;
            Surname = surname;
            Name = name;
            TotPuntiPersi = totPuntiPersi;
        }
    }
}