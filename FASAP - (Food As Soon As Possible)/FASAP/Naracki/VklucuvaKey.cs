using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmetkaZaNaracka.Naracki
{
    public class VklucuvaKey
    {
        public int NarackaID { get; set; }
        public int? VklucuvaID { get; set; }

        public VklucuvaKey(int narID, int? vkKey)
        {
            NarackaID = narID;
            VklucuvaID = vkKey;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is VklucuvaKey))
                return false;
            VklucuvaKey vk = obj as VklucuvaKey;
            if (vk.VklucuvaID == null || VklucuvaID == null)
                return false;
            if (vk.VklucuvaID != VklucuvaID || vk.NarackaID != NarackaID)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            if (VklucuvaID == null)
                return NarackaID * 100000;
            else return NarackaID * 100000 + (int)VklucuvaID;
        }
    }
}
