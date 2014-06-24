using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.Narachki;
using Oracle.DataAccess.Client;
using System.Data;
using System.Data.SqlClient;

namespace SmetkaZaNaracka.Naracki
{
    public class Onsite : Naracka
    {
        public int BrojMasa { get; set; }

        public Onsite(int narackaID, int vkupnaCena, DateTime vreme, int brojMasa)
            : base(narackaID, vkupnaCena, vreme)
        {
            BrojMasa = brojMasa;
        }

        public override void PostaviDodatok(OracleConnection conn, int resID, int narID)
        {
            string updateOnsite = @"insert into DODATOK (RESTORAN_ID, VRABOTEN_ID, MESEC_DODATOK, GODINA_DODATOK, IZNOS_DODATOK) VALUES (:ResID, :VrabID, :Month, :Year, 0)";
            OracleCommand cmd = new OracleCommand(updateOnsite, conn);

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int32);
            prm.Value = resID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("VrabID", OracleDbType.Int32);
            prm.Value = narID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("Month", OracleDbType.Char);
            prm.Value = String.Format("{0:00}", DateTime.Now.Month);
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("Year", OracleDbType.Char);
            prm.Value = DateTime.Now.Year.ToString();
            cmd.Parameters.Add(prm);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

            updateOnsite = @"UPDATE DODATOK SET IZNOS_DODATOK = IZNOS_DODATOK + :Vkupno where RESTORAN_ID = :ResID AND VRABOTEN_ID = :VrabID AND MESEC_DODATOK = :Month AND GODINA_DODATOK = :Year";

            cmd = new OracleCommand(updateOnsite, conn);

            prm = new OracleParameter("Vkupno", OracleDbType.Int32);
            prm.Value = (int)Math.Round(VkupnaCena * 0.02);
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("ResID", OracleDbType.Int32);
            prm.Value = resID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("VrabID", OracleDbType.Int32);
            prm.Value = narID;
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("Month", OracleDbType.Char);
            prm.Value = String.Format("{0:00}", DateTime.Now.Month);
            cmd.Parameters.Add(prm);

            prm = new OracleParameter("Year", OracleDbType.Char);
            prm.Value = DateTime.Now.Year.ToString();
            cmd.Parameters.Add(prm);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

        }

        public override void SqlInsert(OracleConnection conn, int resID)
        {

            string sqlID = @"SELECT NVL(MAX(NARACHKA_ID), - 1) FROM NARACHKA WHERE RESTORAN_ID = :ResID";
            OracleCommand cmd = new OracleCommand(sqlID, conn);

            OracleParameter prm = new OracleParameter("ResID", OracleDbType.Int64);
            prm.Value = resID;
            cmd.Parameters.Add(prm);

            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                NarackaID = dr.GetInt32(0) + 1;

            OracleCommand myCommand = conn.CreateCommand();
            OracleTransaction myTrans;

            // Start a local transaction
            myTrans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            // Assign transaction object for a pending local transaction
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = "INSERT INTO NARACHKA (RESTORAN_ID, NARACHKA_ID, VKUPNA_CENA) VALUES (:ResID, :NarachkaID , :VkCena)";

                prm = new OracleParameter("ResID", OracleDbType.Int64);
                prm.Value = resID;
                myCommand.Parameters.Add(prm);

                prm = new OracleParameter("NarachkaID", OracleDbType.Int64);
                prm.Value = NarackaID;
                myCommand.Parameters.Add(prm);

                prm = new OracleParameter("VkCena", OracleDbType.Int32);
                prm.Value = VkupnaCena;
                myCommand.Parameters.Add(prm);

                myCommand.ExecuteNonQuery();

                myCommand = conn.CreateCommand();
                myCommand.Transaction = myTrans;

                myCommand.CommandText = "INSERT INTO ONSITE_NARACHKA (RESTORAN_ID, NARACHKA_ID, BROJ_MASA) VALUES (:ResID, :NarachkaID, :BrojMasa)";

                prm = new OracleParameter("ResID", OracleDbType.Int64);
                prm.Value = resID;
                myCommand.Parameters.Add(prm);

                prm = new OracleParameter("NarachkaID", OracleDbType.Int64);
                prm.Value = NarackaID;
                myCommand.Parameters.Add(prm);

                prm = new OracleParameter("BrojMasa", OracleDbType.Int64);
                prm.Value = BrojMasa;
                myCommand.Parameters.Add(prm);

                myCommand.ExecuteNonQuery();

                int vkID = 0;
                foreach (var obj in Stavki)
                {
                    vkID = obj.SqlVklucuva(conn, myTrans, resID, NarackaID, vkID);
                }

                myTrans.Commit();
            }
            catch (Exception)
            {
                myTrans.Rollback();
                throw new NotImplementedException("Трансакцијата не помина");
            }
        }
    }
}
