using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Description;

namespace PoliziaMunicipale.Models
{
    public static class DB
    {
        public static void AggiungiTrasgressore(string surname, string name, string address, string city, int cap, string cf)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [ANAGRAFICA] (cognome, nome, indirizzo, citta, CAP, CF) VALUES(@surname, @name, @address, @city, @cap,@cf)";
                cmd.Parameters.AddWithValue("surname", surname);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("address", address);
                cmd.Parameters.AddWithValue("city", city);
                cmd.Parameters.AddWithValue("cap", cap);
                cmd.Parameters.AddWithValue("cf", cf);
                int IsOk = cmd.ExecuteNonQuery();

            }

            catch { }
            finally
            {
                conn.Close();
            }
        }

        public static void AggiungiViolazione(string description)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [TIPOVIOLAZIONE] (descrizione) VALUES(@description)";
                cmd.Parameters.AddWithValue("description", description);
                int IsOk = cmd.ExecuteNonQuery();

            }

            catch { }
            finally
            {
                conn.Close();
            }
        }

        public static List<Trasgressore> getAllTrasgressori()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from ANAGRAFICA", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<Trasgressore> trasgressori = new List<Trasgressore>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Trasgressore t = new Trasgressore();
                t.Id = Convert.ToInt32(sqlDataReader["idAnagrafica"]);
                t.Surname = sqlDataReader["cognome"].ToString();
                t.Name = sqlDataReader["nome"].ToString();
                t.Address = sqlDataReader["indirizzo"].ToString();
                t.City = sqlDataReader["citta"].ToString();
                t.CAP = Convert.ToInt32(sqlDataReader["CAP"]);
                t.CF = sqlDataReader["CF"].ToString();
                trasgressori.Add(t);
            }

            conn.Close();
            return trasgressori;
        }

        public static List<Violazione> getAllViolazioni()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from TIPOVIOLAZIONE", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<Violazione> violazioni = new List<Violazione>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Violazione v = new Violazione();
                v.Id = Convert.ToInt32(sqlDataReader["idViolazione"]);
                v.Description = sqlDataReader["descrizione"].ToString();
                violazioni.Add(v);
            }

            conn.Close();
            return violazioni;
        }
        public static List<Verbale> getAllVerbali()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from VERBALE", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<Verbale> verbali = new List<Verbale>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Verbale v = new Verbale();
                v.Id = Convert.ToInt32(sqlDataReader["idVerbale"]);
                v.DataViolazione = Convert.ToDateTime(sqlDataReader["dataviolazione"]);
                v.IndirizzoViolazione = sqlDataReader["indirizzoViolazione"].ToString();
                v.Agente = sqlDataReader["nominativoAgente"].ToString();
                v.DataVerbale = Convert.ToDateTime(sqlDataReader["dataTrascrizioneVerbale"]);
                v.Importo = Convert.ToDouble(sqlDataReader["importo"]);
                v.PuntiTolti = Convert.ToInt32(sqlDataReader["decurtamentoPunti"]);
                v.IdTrasgressore = Convert.ToInt32(sqlDataReader["idAnagrafica"]);
                v.IdViolazione = Convert.ToInt32(sqlDataReader["idViolazione"]);
                verbali.Add(v);
            }

            conn.Close();
            return verbali;
        }

        public static void AggiungiVerbale(DateTime dataViolazione,string address,string agent,DateTime dataVerbale,double amount,int points,int idT,int idV)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [VERBALE] (dataViolazione,IndirizzoViolazione,nominativoAgente,dataTrascrizioneVerbale,importo,decurtamentoPunti,idAnagrafica,idViolazione) " +
                    "VALUES(@dataViolazione,@address,@agent,@dataVerbale,@amount,@points,@idT,@idV)";
                cmd.Parameters.AddWithValue("dataViolazione", dataViolazione);
                cmd.Parameters.AddWithValue("address", address);
                cmd.Parameters.AddWithValue("agent", agent);
                cmd.Parameters.AddWithValue("dataVerbale", dataVerbale);
                cmd.Parameters.AddWithValue("amount", amount);
                cmd.Parameters.AddWithValue("points", points);
                cmd.Parameters.AddWithValue("idT", idT);
                cmd.Parameters.AddWithValue("idV", idV);
                int IsOk = cmd.ExecuteNonQuery();

            }

            catch(Exception ex) { }
            finally
            {
                conn.Close();
            }
        }

        public static List<VerbaliByT> getCountVerbaliByTrasgressore()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("SELECT ANAGRAFICA.idAnagrafica,cognome,nome, COUNT(*) AS TotVerbali FROM VERBALE INNER JOIN ANAGRAFICA " +
                "ON ANAGRAFICA.idAnagrafica = VERBALE.idAnagrafica GROUP BY ANAGRAFICA.idAnagrafica,cognome,nome", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<VerbaliByT> verbaliByT = new List<VerbaliByT>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                VerbaliByT v = new VerbaliByT();
                v.IdT = Convert.ToInt32(sqlDataReader["idAnagrafica"]);
                v.Surname = sqlDataReader["cognome"].ToString();
                v.Name = sqlDataReader["nome"].ToString();
                v.TotVerbali = Convert.ToInt32(sqlDataReader["TotVerbali"]);
                verbaliByT.Add(v);
            }

            conn.Close();
            return verbaliByT;
        }

        public static List<PuntiByT> getPuntiByTrasgressore()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("SELECT ANAGRAFICA.idAnagrafica,cognome,nome, SUM(decurtamentoPunti) AS TotPuntiPersi FROM VERBALE "+
                                            "INNER JOIN ANAGRAFICA ON ANAGRAFICA.idAnagrafica = VERBALE.idAnagrafica GROUP BY ANAGRAFICA.idAnagrafica, cognome, nome", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<PuntiByT> puntiByT = new List<PuntiByT>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                PuntiByT p = new PuntiByT();
                p.IdT = Convert.ToInt32(sqlDataReader["idAnagrafica"]);
                p.Surname = sqlDataReader["cognome"].ToString();
                p.Name = sqlDataReader["nome"].ToString();
                p.TotPuntiPersi = Convert.ToInt32(sqlDataReader["TotPuntiPersi"]);
                puntiByT.Add(p);
            }

            conn.Close();
            return puntiByT;
        }

        public static List<AnMag10Punti> getTrasgressoriMag10Punti()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("SELECT cognome, nome, dataViolazione, importo, decurtamentoPunti FROM VERBALE INNER JOIN ANAGRAFICA "+
                                            "ON ANAGRAFICA.idAnagrafica = VERBALE.idAnagrafica WHERE decurtamentoPunti > 10", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<AnMag10Punti> anMag10 = new List<AnMag10Punti>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                AnMag10Punti a = new AnMag10Punti();
                a.Surname = sqlDataReader["cognome"].ToString();
                a.Name = sqlDataReader["nome"].ToString();
                a.DataViolazione = Convert.ToDateTime(sqlDataReader["dataViolazione"]);
                a.Amount = Convert.ToDouble(sqlDataReader["importo"]);
                a.PuntiPersi = Convert.ToInt32(sqlDataReader["decurtamentoPunti"]);
                anMag10.Add(a);
            }

            conn.Close();
            return anMag10;
        }

        public static List<ImportoMag400> getImportoMag400()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("SELECT cognome, nome, dataViolazione, importo, decurtamentoPunti FROM VERBALE INNER JOIN ANAGRAFICA "+
                                            "ON ANAGRAFICA.idAnagrafica = VERBALE.idAnagrafica WHERE importo > 400", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<ImportoMag400> amountMag400 = new List<ImportoMag400>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                ImportoMag400 a = new ImportoMag400();
                a.Surname = sqlDataReader["cognome"].ToString();
                a.Name = sqlDataReader["nome"].ToString();
                a.DataViolazione = Convert.ToDateTime(sqlDataReader["dataViolazione"]);
                a.Amount = Convert.ToDouble(sqlDataReader["importo"]);
                a.Points = Convert.ToInt32(sqlDataReader["decurtamentoPunti"]);
                amountMag400.Add(a);
            }

            conn.Close();
            return amountMag400;
        }

        public static Trasgressore getTrasgressoreById(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from ANAGRAFICA where idAnagrafica = @id", conn);
            cmd.Parameters.AddWithValue("id", id);
            SqlDataReader sqlDataReader;

            conn.Open();

            Trasgressore t = new Trasgressore();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                t.Id = Convert.ToInt32(sqlDataReader["idAnagrafica"]);
                t.Surname = sqlDataReader["cognome"].ToString();
                t.Name = sqlDataReader["nome"].ToString();
                t.Address = sqlDataReader["indirizzo"].ToString();
                t.City = sqlDataReader["citta"].ToString();
                t.CAP = Convert.ToInt32(sqlDataReader["CAP"]);
                t.CF = sqlDataReader["CF"].ToString();
            }

            conn.Close();
            return t;
        }

        public static Violazione getViolazioneById(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from TIPOVIOLAZIONE where idViolazione = @id", conn);
            cmd.Parameters.AddWithValue("id", id);
            SqlDataReader sqlDataReader;

            conn.Open();

            Violazione v = new Violazione();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                v.Id = Convert.ToInt32(sqlDataReader["idViolazione"]);
                v.Description = sqlDataReader["descrizione"].ToString();
            }

            conn.Close();
            return v;
        }
        public static Verbale getVerbaleById(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from VERBALE where idVerbale = @id", conn);
            cmd.Parameters.AddWithValue("id", id);
            SqlDataReader sqlDataReader;

            conn.Open();

            Verbale v = new Verbale();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                v.Id = Convert.ToInt32(sqlDataReader["idVerbale"]);
                v.DataViolazione = Convert.ToDateTime(sqlDataReader["dataViolazione"]);
                v.IndirizzoViolazione = sqlDataReader["indirizzoViolazione"].ToString();
                v.Agente = sqlDataReader["nominativoAgente"].ToString();
                v.DataVerbale = Convert.ToDateTime(sqlDataReader["dataTrascrizioneVerbale"]);
                v.Importo = Convert.ToDouble(sqlDataReader["importo"]);
                v.PuntiTolti = Convert.ToInt32(sqlDataReader["decurtamentoPunti"]);
                v.IdTrasgressore = Convert.ToInt32(sqlDataReader["idAnagrafica"]);
                v.IdViolazione = Convert.ToInt32(sqlDataReader["idViolazione"]);
            }

            conn.Close();
            return v;
        }
        public static void UpdateTrasgressore(int id, string surname, string name, string address, string city, int cap, string cf)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE ANAGRAFICA SET cognome=@surname,nome=@name,indirizzo=@address,citta=@city,CAP=@cap,CF=@cf WHERE idAnagrafica = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("surname", surname);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("address", address);
                cmd.Parameters.AddWithValue("city", city);
                cmd.Parameters.AddWithValue("cap", cap);
                cmd.Parameters.AddWithValue("cf", cf);
                int IsOk = cmd.ExecuteNonQuery();

            }

            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
        }

        public static void RemoveTrasgressore(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM ANAGRAFICA where idAnagrafica=@id";
            cmd.Parameters.AddWithValue("id", id);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public static void UpdateViolazione(int id, string description)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE TIPOVIOLAZIONE SET descrizione=@description WHERE idViolazione = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("description", description);  
                int IsOk = cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
        }
        public static void RemoveViolazione(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM TIPOVIOLAZIONE where idViolazione=@id";
            cmd.Parameters.AddWithValue("id", id);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public static void UpdateVerbale(int id, DateTime dataV, string address, string agent, DateTime dataT, double amount, int points, int idT, int idV)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE VERBALE SET dataViolazione=@dataV,indirizzoViolazione=@address,nominativoAgente=@agent,dataTrascrizioneVerbale=@dataT,importo=@amount,decurtamentoPunti=@points, idAnagrafica=@idT, idViolazione=@idV WHERE idVerbale = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("dataV", dataV);
                cmd.Parameters.AddWithValue("address", address);
                cmd.Parameters.AddWithValue("agent", agent);
                cmd.Parameters.AddWithValue("dataT", dataT);
                cmd.Parameters.AddWithValue("amount", amount);
                cmd.Parameters.AddWithValue("points", points);
                cmd.Parameters.AddWithValue("idT", idT);
                cmd.Parameters.AddWithValue("idV", idV);
                int IsOk = cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
        }

        public static void RemoveVerbale(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM VERBALE where idVerbale=@id";
            cmd.Parameters.AddWithValue("id", id);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}