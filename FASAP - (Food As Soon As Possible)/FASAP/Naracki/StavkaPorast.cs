using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka.Naracki
{
    public class StavkaPorast
    {
        public string ime { get; set; }
        public string stara { get; set; }
        public string nova { get; set; }
        public string cena { get; set; }
        public string procent { get; set; }

        public StavkaPorast(string i, string s, string n, string c, string p)
        {
            ime = i;
            stara = s;
            nova = n;
            cena = c;
            procent = p;
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
            if (ime.ToLower().Contains(zbor.ToLower()))
                return true;
            return false;
        }
    }
}
