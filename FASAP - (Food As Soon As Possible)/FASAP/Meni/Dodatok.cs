using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace SmetkaZaNaracka
{
    public class Dodatok : Stavka
    {
        public MenuComponent Osnovna { get; set; }

        public Dodatok(int id, string ime, decimal cena, string opis = "")
            : base(id, ime, cena, opis)
        {
            Osnovna = null;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override int ComputeCost()
        {
            if (Osnovna != null)
                return base.ComputeCost() + Osnovna.ComputeCost();
            else return base.ComputeCost();
        }

        public override string GetDescription()
        {
            if (Osnovna != null)
                return String.Format("{0}, {1}", Osnovna.GetDescription(), base.GetDescription());
            else return base.GetDescription();
        }

        public override bool Sodrzi(Object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Dodatok))
                return false;
            Dodatok dodatok = obj as Dodatok;
            if (ID != dodatok.ID || !Parent.Equals(dodatok.Parent))
                if (Osnovna != null)
                    return Osnovna.Sodrzi(obj);
            return true;
        }

        public override string GetName()
        {
            if (Osnovna != null)
                return String.Format("{0}, {1}", Osnovna.GetName(), base.GetName());
            else return base.GetName();
        }

        public override MenuComponent GetReference(MenuComponent mc)
        {
            if (mc == null)
                throw new NotImplementedException(String.Format("Не е можно да го нарачате само додатокот \"{0}\"", GetName()));
            MenuComponent pom = this;
            while (pom.Parent != null)
            {
                if (pom.Parent.Equals(mc.Parent))
                    break;
                pom = pom.Parent;
            }
            if (pom.Parent == null)
                throw new NotImplementedException(String.Format("Додатокот \"{0}\" не е соодветен на производот \"{1}\"", this.GetName(), mc.GetName()));
            if (mc.Sodrzi(this))
                throw new NotImplementedException(String.Format("Производот \"{0}\" веќе го содржи додатокот \"{1}\"", mc.GetName(), GetName()));
            Dodatok dodatok = new Dodatok(ID, Ime, Cena, Opis);
            dodatok.Parent = Parent;
            dodatok.Osnovna = mc;
            return dodatok;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Dodatok))
                return false;
            Dodatok dodatok = obj as Dodatok;
            if (ID != dodatok.ID || !Parent.Equals(dodatok.Parent))
                return false;
            if (Osnovna == null && dodatok.Osnovna == null)
                return true;
            if (Osnovna == null)
                return false;
            if (!Osnovna.Equals(dodatok.Osnovna))
                return false;
            return true;
        }

        public override void SqlInsert(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            string insertRest = @"INSERT INTO STAVKA (RESTORAN_ID, IME_MENI, STAVKA_ID, OPIS_STAVKA, CENA_STAVKA, DODATOK_STAVKA, IME_STAVKA) VALUES (:ResID, :ImeMeni, (select max(stavka_id) from stavka) + 1, :OpisStavka, :CenaStavka, 1, :ImeStavka)";
            OracleCommand cmd = new OracleCommand(insertRest, conn);

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int64);
            prm.Value = resID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ImeMeni", OracleDbType.Varchar2);
            prm.Value = Parent.GetName();
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("OpisStavka", OracleDbType.Varchar2);
            prm.Value = Opis;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("CenaStavka", OracleDbType.Int16);
            prm.Value = Cena;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ImeStavka", OracleDbType.Varchar2);
            prm.Value = Ime;
            cmd.Parameters.Add(prm);

            int br;
            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw new NotImplementedException("Проверете ја вашата конекција");
            }


        }

        public override void SQLUpdate(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            string insertRest = @"UPDATE STAVKA
                                SET OPIS_STAVKA = :OpisStavka, CENA_STAVKA = :CenaStavka, IME_STAVKA = :ImeStavka, DODATOK_STAVKA = 1
                                WHERE RESTORAN_ID = :ResID AND IME_MENI = :ImeMeni AND STAVKA_ID = :StavkaID";
            OracleCommand cmd = new OracleCommand(insertRest, conn);

            OracleParameter prm = new OracleParameter("OpisStavka", OracleDbType.Varchar2);
            prm.Value = Opis;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("CenaStavka", OracleDbType.Int64);
            prm.Value = this.ComputeCost();
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ImeStavka", OracleDbType.Varchar2);
            prm.Value = Ime;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ResID", OracleDbType.Int64);
            prm.Value = resID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ImeMeni", OracleDbType.Varchar2);
            prm.Value = Parent.GetName();
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("StavkaID", OracleDbType.Int64);
            prm.Value = ID;
            cmd.Parameters.Add(prm);

            int br;
            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new NotImplementedException("Проверете ја вашата конекција");
            }

            if (br == 0)
                throw new DuplicatePrimaryKeyException("Не постои ставката");
        }

        public override int SqlVklucuva(OracleConnection conn, OracleTransaction myTrans, int resID, int narID, int vkID, int q)
        {
            OracleCommand myCommand = conn.CreateCommand();
            myCommand.Transaction = myTrans;

            int pomID = Osnovna.SqlVklucuva(conn, myTrans, resID, narID, vkID, q);
            myCommand.CommandText = "INSERT INTO VKLUCHUVA (RESTORAN_ID, NARACHKA_ID, VKLUCHUVA_ID, IME_MENI, STAVKA_ID, KOLICHINA_STAVKA, DODATOK_ID) VALUES (:ResID, :NarID, :VkID, :ImeMeni, :StavkaID, :Kolicina, :DodatokID)";

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int64);
            prm.Value = resID;
            myCommand.Parameters.Add(prm);

            prm = new OracleParameter("NarachkaID", OracleDbType.Int64);
            prm.Value = narID;
            myCommand.Parameters.Add(prm);

            prm = new OracleParameter("VkID", OracleDbType.Int64);
            prm.Value = pomID;
            myCommand.Parameters.Add(prm);

            prm = new OracleParameter("ImeMeni", OracleDbType.Varchar2);
            prm.Value = Parent.GetName();
            myCommand.Parameters.Add(prm);

            prm = new OracleParameter("StavkaID", OracleDbType.Int64);
            prm.Value = ID;
            myCommand.Parameters.Add(prm);

            prm = new OracleParameter("Kolicina", OracleDbType.Int64);
            prm.Value = q;
            myCommand.Parameters.Add(prm);

            prm = new OracleParameter("DodatokID", OracleDbType.Int64);
            prm.Value = pomID - 1;
            myCommand.Parameters.Add(prm);


            myCommand.ExecuteNonQuery();

            return pomID + 1;
        }

        public override MenuComponent GetSameComponent(MenuComponent mc)
        {
            if (base.Equals(mc))
                return this;
            return null;
        }
    }
}
