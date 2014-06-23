using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.Narachki;
using Oracle.DataAccess.Client;
using System.Data;

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

        public override void SqlInsert(OracleConnection conn, int resID)
        {

            string sqlID = @"SELECT MAX(NARACHKA_ID) FROM NARACHKA WHERE RESTORAN_ID = :ResID";
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
