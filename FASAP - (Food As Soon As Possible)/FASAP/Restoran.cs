using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class Restoran
    {
        public int RestoranID { get; set; }
        public string Ime { get; set; }
        public string PicturePath { get; set; }
        public string Ulica { get; set; }
        public string Grad { get; set; }
        public string RabotnoVreme { get; set; }
        public int Kapacitet { get; set; }
        public int BrojMasi { get; set; }
        public int SlobodniMasi { get; set; }
        public decimal CenaZaDostava { get; set; }
        public decimal PragZaDostava { get; set; }
        public DateTime DatumNaOtvoranje { get; set; }
        public string Kategorija { get; set; }
        public double Rejting { get; set; }
        public List<Telefon> Kontakt { get; set; }
        public MenuComponent GlavnoMeni { get; set; }

        public Restoran()
        {
            Kontakt = new List<Telefon>();
            PicturePath = null;
        }

        public override string ToString()
        {
            return Ime;
        }
    }
}
