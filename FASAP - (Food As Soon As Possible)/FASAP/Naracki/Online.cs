﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmetkaZaNaracka.Narachki;
using Oracle.DataAccess.Client;
using System.Data;

namespace SmetkaZaNaracka.Naracki
{
    public class Online : Naracka
    {
        public string AdresaZaDostava { get; set; }
        public string Kontakt { get; set; }
        public string ImeKlient { get; set; }
        public string PrezimeKlient { get; set; }
        public int CenaZaDostava { get; set; }

        public Online(int narackaID, int vkupnaCena, DateTime vreme, string adresaZaDostava, string kontakt, string imeKlient, string prezimeKlient)
            : base(narackaID, vkupnaCena, vreme)
        {
            AdresaZaDostava = adresaZaDostava;
            Kontakt = kontakt;
            ImeKlient = imeKlient;
            PrezimeKlient = prezimeKlient;
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

                myCommand.CommandText = "INSERT INTO ONLINE_NARACHKA (RESTORAN_ID, NARACHKA_ID, ADRESA_ZA_DOSTAVA, KONTAKT, IME_KLIENT, PREZIME_KLIENT, CENA_ZA_DOSTAVA) VALUES (:ResID, :NarachkaID, :AdresaZaDostava, :Kontakt, :Ime, :Prezime, :CenaZaDostava)";

                prm = new OracleParameter("ResID", OracleDbType.Int64);
                prm.Value = resID;
                myCommand.Parameters.Add(prm);

                prm = new OracleParameter("NarachkaID", OracleDbType.Int64);
                prm.Value = NarackaID;
                myCommand.Parameters.Add(prm);

                prm = new OracleParameter("AdresaZaDostava", OracleDbType.Varchar2);
                prm.Value = AdresaZaDostava;
                myCommand.Parameters.Add(prm);

                prm = new OracleParameter("Kontakt", OracleDbType.Varchar2);
                prm.Value = Kontakt;
                myCommand.Parameters.Add(prm);

                prm = new OracleParameter("Ime", OracleDbType.Varchar2);
                prm.Value = ImeKlient;
                myCommand.Parameters.Add(prm);

                prm = new OracleParameter("Prezime", OracleDbType.Varchar2);
                prm.Value = PrezimeKlient;
                myCommand.Parameters.Add(prm);

                prm = new OracleParameter("CenaZaDostava", OracleDbType.Varchar2);
                prm.Value = CenaZaDostava;
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
