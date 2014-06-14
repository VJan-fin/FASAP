using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka
{
    public class StavkaKey
    {
        public int IdStavka { get; set; }
        public string ImeGlavno { get; set; }

        public override int GetHashCode()
        {
            return ImeGlavno.GetHashCode() + IdStavka;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is StavkaKey))
                return false;
            StavkaKey st = obj as StavkaKey;
            if (st.IdStavka != IdStavka || st.ImeGlavno != ImeGlavno)
                return false;
            return true;
        }
    }
}
