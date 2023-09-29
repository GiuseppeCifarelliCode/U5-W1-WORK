using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale.Models
{
    public class VerbaliByT
    {
        public int IdT { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public int TotVerbali { get; set; }
        public VerbaliByT() { }
        public VerbaliByT(int idT, string surname, string name, int totVerbali)
        {
            IdT = idT;
            Surname = surname;
            Name = name;
            TotVerbali = totVerbali;
        }
    }
}