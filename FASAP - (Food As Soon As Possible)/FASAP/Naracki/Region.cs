using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SmetkaZaNaracka.Naracki
{
    public class Region
    {
        public string Ime { get; set; }
        public int BrojNaracki { get; set; }
        public decimal Procent { get; set; }

        public Region(string ime, int brojNaracki, decimal procent)
        {
            Ime = ime;
            BrojNaracki = brojNaracki;
            Procent = procent;
        }

        public bool Sodrzi(string Text)
        {
            String[] zborovi = Text.Split(new string[] { " ", "\t", "\n" }, StringSplitOptions.None);
            for (int i = 0; i < zborovi.Length; i++)
                if (!SodrziZbor(zborovi[i]))
                    return false;
            return true;
        }

        private bool SodrziZbor(string zbor)
        {
            if (Ime.ToLower().Contains(zbor.ToLower()))
                return true;
            return false;
        }
    }

    public class RegionNameAscCoparer : Comparer<Region>
    {

        public override int Compare(Region x, Region y)
        {
            int pom = x.Ime.CompareTo(y.Ime);
            if (pom == 0)
                return y.Procent.CompareTo(x.Procent);
            return pom;
        }
    }

    public class RegionNameDescCoparer : Comparer<Region>
    {

        public override int Compare(Region x, Region y)
        {
            int pom = y.Ime.CompareTo(x.Ime);
            if (pom == 0)
                return y.Procent.CompareTo(x.Procent);
            return pom;
        }
    }

    public class RegionProcentAscCoparer : Comparer<Region>
    {

        public override int Compare(Region x, Region y)
        {
            int pom = x.Procent.CompareTo(y.Procent);
            if (pom == 0)
                return x.Ime.CompareTo(y.Ime);
            return pom;
        }
    }

    public class RegionProcentDescCoparer : Comparer<Region>
    {

        public override int Compare(Region x, Region y)
        {
            int pom = y.Procent.CompareTo(x.Procent);
            if (pom == 0)
                return x.Ime.CompareTo(y.Ime);
            return pom;
        }
    }
}
