using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class Kelner : Vraboten
    {
        public Kelner(int vrabID, int resID, string ime, string prezime, string username, string password)
            : base(vrabID, resID, ime, prezime, username, password)
        {
        }

        public Kelner()
        {
        }

        public override void PrevzemiNaracka()
        {
            throw new NotImplementedException();
        }
    }
}
