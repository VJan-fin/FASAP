using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data.SqlClient;

namespace SmetkaZaNaracka
{
    public class Meni : MenuComponent
    {
        public string Ime { get; set; }
        public List<MenuComponent> Content { get; set; }
        public bool ValidnostMeni { get; set; }

        public Meni(string ime)
        {
            Ime = ime;
            Content = new List<MenuComponent>();
        }

        public override string GetDescription()
        {
            return "";
        }

        public override int ComputeCost()
        {
            throw new NotImplementedException();
        }

        public override string Print(string indent)
        {
            StringBuilder podmeni = new StringBuilder();
            podmeni.Append(String.Format("\n{0}{1}", indent, GetName()));
            foreach (var obj in Content)
                podmeni.Append(obj.Print(String.Format("\t{0}", indent)));
            return podmeni.ToString();
        }

        public override void AddComp(MenuComponent mc)
        {
            Content.Add(mc);
        }

        public override void RemoveComp(MenuComponent mc)
        {
            Content.Remove(mc);
        }

        public override string GetName()
        {
            return Ime;
        }

        public override List<MenuComponent> GetContent()
        {
            return Content;
        }

        public override MenuComponent GetReference(MenuComponent mc)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Meni))
                return false;
            Meni meni = obj as Meni;
            if (Ime != meni.Ime)
                return false;
            //foreach (var obt in Content)
            //    foreach (var oob in meni.Content)
            //        if (!obt.Equals(oob))
            //            return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void SqlInsert(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            string insertRest = @"INSERT INTO MENI (RESTORAN_ID, IME_MENI, IME_GLAVNO) VALUES (:ResID, :ImeMeni, :ImeGlavno)";
            OracleCommand cmd = new OracleCommand(insertRest, conn);

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int64);
            prm.Value = resID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ImeMeni", OracleDbType.Varchar2);
            prm.Value = Ime;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ImeGlavno", OracleDbType.Varchar2);
            prm.Value = this.Parent.GetName();
            cmd.Parameters.Add(prm);

            int br;
            try
            {
                br = cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 0001: // Primary key violation
                            throw new DuplicatePrimaryKeyException(String.Format("Менито \"{0}\" веќе постои, ве молиме пробајте друго име.", Ime));
                        default:
                            throw new NotImplementedException("Проверете ја вашата конекција");
                    }
                }
            }
        }

        public override void SqlDelete(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            string insertRest = @"UPDATE MENI
                                SET VALIDNOST_MENI = 0
                                WHERE RESTORAN_ID = :ResID AND IME_MENI = :ImeMeni";
            OracleCommand cmd = new OracleCommand(insertRest, conn);

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int64);
            prm.Value = resID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ImeMeni", OracleDbType.Varchar2);
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

            if (br == 0)
                throw new DuplicatePrimaryKeyException("Не постои ставката");
            ValidnostMeni = false;
        }

        public override void SqlActivate(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            string insertRest = @"UPDATE MENI
                                SET VALIDNOST_MENI = 1
                                WHERE RESTORAN_ID = :ResID AND IME_MENI = :ImeMeni";
            OracleCommand cmd = new OracleCommand(insertRest, conn);

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int64);
            prm.Value = resID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ImeMeni", OracleDbType.Varchar2);
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

            if (br == 0)
                throw new NotImplementedException("Не постои ставката");
            ValidnostMeni = true;
        }

        public override void SQLUpdate(Oracle.DataAccess.Client.OracleConnection conn, int resID)
        {
            throw new NotImplementedException("Не е имплементирано");
        }

        public override void SetName(string name)
        {
            Ime = name;
        }

        public override void SetDescription(string description)
        {
        }

        public override void SetCost(int cost)
        {
        }

        public override int SqlVklucuva(OracleConnection conn, OracleTransaction myTrans, int resID, int narID, int vkID, int q)
        {
            throw new NotImplementedException();
        }
    }
}

public class DuplicatePrimaryKeyException : Exception
{
    public DuplicatePrimaryKeyException()
        : base()
    {
    }

    public DuplicatePrimaryKeyException(String message)
        : base(message)
    {
    }
}