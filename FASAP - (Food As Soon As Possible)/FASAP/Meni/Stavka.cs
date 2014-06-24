using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace SmetkaZaNaracka
{
    public class Stavka : MenuComponent
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Opis { get; set; }
        public int Cena { get; set; }

        public Stavka(int id, string ime, decimal cena, string opis = "")
        {
            ID = id;
            Ime = ime;
            Opis = opis;
            Cena = (int)cena;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string GetDescription()
        {
            return Opis;
        }

        public override int ComputeCost()
        {
            return Cena;
        }

        public override string Print(string indent)
        {
            return String.Format("{0}{1}", indent);
        }

        public override void AddComp(MenuComponent mc)
        {
            throw new NotImplementedException();
        }

        public override void RemoveComp(MenuComponent mc)
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return Ime;
        }

        public override List<MenuComponent> GetContent()
        {
            throw new NotImplementedException();
        }

        public override MenuComponent GetReference(MenuComponent mc)
        {
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Stavka))
                return false;
            Stavka st = obj as Stavka;
            if (ID != st.ID || !Parent.Equals(st.Parent))
                return false;
            return true;
        }

        public StavkaKey GetStavkaKey()
        {
            StavkaKey sid = new StavkaKey();
            sid.IdStavka = ID;
            sid.ImeGlavno = ImeGlavno;
            return sid;
        }

        public override void SqlInsert(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            string insertRest = @"INSERT INTO STAVKA (RESTORAN_ID, IME_MENI, STAVKA_ID, OPIS_STAVKA, CENA_STAVKA, DODATOK_STAVKA, IME_STAVKA) VALUES (:ResID, :ImeMeni, NVL((select max(stavka_id) from stavka where restoran_id = :ResID1 AND ime_meni = :ImeMeni1), 0) + 1, :OpisStavka, :CenaStavka, 0, :ImeStavka)";
            OracleCommand cmd = new OracleCommand(insertRest, conn);

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int64);
            prm.Value = resID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ImeMeni", OracleDbType.Varchar2);
            prm.Value = Parent.GetName();
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ResID1", OracleDbType.Int64);
            prm.Value = resID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ImeMeni1", OracleDbType.Varchar2);
            prm.Value = Parent.GetName();
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("OpisStavka", OracleDbType.Varchar2);
            prm.Value = Opis;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("CenaStavka", OracleDbType.Int64);
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

        public override void SqlDelete(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            string insertRest = @"UPDATE STAVKA
                                SET VALIDNOST_STAVKA = 0
                                WHERE RESTORAN_ID = :ResID AND IME_MENI = :ImeMeni AND STAVKA_ID = :StavkaID";
            OracleCommand cmd = new OracleCommand(insertRest, conn);

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int64);
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
                throw new NotImplementedException("Не постои ставката");
            Parent.RemoveComp(this);
        }

        public override void SqlActivate(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            string insertRest = @"UPDATE STAVKA
                                SET VALIDNOST_STAVKA = 1
                                WHERE RESTORAN_ID = :ResID AND IME_MENI = :ImeMeni AND STAVKA_ID = :StavkaID";
            OracleCommand cmd = new OracleCommand(insertRest, conn);

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int64);
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

        public override void SQLUpdate(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            string insertRest = @"UPDATE STAVKA
                                SET OPIS_STAVKA = :OpisStavka, CENA_STAVKA = :CenaStavka, IME_STAVKA = :ImeStavka, DODATOK_STAVKA = 0
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

        public override void SetName(string name)
        {
            Ime = name;
        }

        public override void SetDescription(string description)
        {
            Opis = description;
        }

        public override void SetCost(int cost)
        {
            Cena = cost;
        }

        public override int SqlVklucuva(OracleConnection conn, OracleTransaction myTrans, int resID, int narID, int vkID, int q)
        {
            OracleCommand myCommand = conn.CreateCommand();
            myCommand.Transaction = myTrans;

            myCommand.CommandText = "INSERT INTO VKLUCHUVA (RESTORAN_ID, NARACHKA_ID, VKLUCHUVA_ID, IME_MENI, STAVKA_ID, KOLICHINA_STAVKA) VALUES (:ResID, :NarID, :VkID, :ImeMeni, :StavkaID, :Kolicina)";

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int64);
            prm.Value = resID;
            myCommand.Parameters.Add(prm);

            prm = new OracleParameter("NarachkaID", OracleDbType.Int64);
            prm.Value = narID;
            myCommand.Parameters.Add(prm);

            prm = new OracleParameter("VkID", OracleDbType.Int64);
            prm.Value = vkID;
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

            myCommand.ExecuteNonQuery();

            return vkID + 1;
        }

        public override bool Sodrzi(Object obj)
        {
            return this.Equals(obj);
        }

        public override MenuComponent GetSameComponent(MenuComponent mc)
        {
            if (Equals(mc))
                return this;
            return null;
        }
    }
}
