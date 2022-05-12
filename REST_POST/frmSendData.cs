using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Flurl.Http;
using RestSharp;

using System.Collections.Specialized;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Configuration;

namespace REST_POST
{
    public partial class frmSendData : Form
    {
        string csLocal = Utils.GetConnectionStringComplete("Ecoprogram");
        string SQL = "";
        string hash = "";
        string json = "{";
        string url = "";
        string result = "";
        public frmSendData()
        {
            InitializeComponent();
            txtHASH.Text = "";
            wfTimer.Enabled = false;
        }

       

        private void SendDataToEcoprogram(object sender, EventArgs e)
        {
            string aTarga = "";
            string aIDTelaio = "";
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["Ecoprogram"];
            string connStr = "";
            connStr = "Data Source=GESQL01,8194;Initial Catalog=Ecoprogram;Integrated Security=False;MultipleActiveResultSets=True;" + "User ID=" + Utils.AppUser + ";Password=" + Utils.AppPassword;
            txtHASH.Text += "Inizio lavoro : " + DateTime.Now.ToString("dd MM yyyy hh:mm:ss:ms") + Environment.NewLine;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                
                //myTimer.Enabled = false;


                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    SQL = " SELECT TOP 1 Targa,IDTelaio, Codice,C,NC,S,DS,NP,NV fROM  Ispezioni_Flat_vw WHERE IsTransferred = 0 AND IsClosed = 1  ORDER BY IDSezione , Codice";
                    cmd.CommandText = SQL;


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            json = "{";
                            json += Environment.NewLine + "\t" + dr["Targa"].ToString().DQuotedStrSalt();
                            aTarga = dr["Targa"].ToString().QuotedStr();
                            aIDTelaio = dr["IDTelaio"].ToString();
                            txtHASH.Text += aTarga + Environment.NewLine;
                            //MessageBox.Show(aTarga);
                        }
                    }
                }

                if (!String.IsNullOrEmpty(aTarga))
                {
                    using (SqlCommand cmd = new SqlCommand(SQL, conn))
                    {
                        SQL = " SELECT ID, Codice,C,NC,S,DS,NP,NV,Annotazioni,NomeFile FROM  Ispezioni_Flat_vw WHERE Targa = " + aTarga + "  ORDER BY IDSezione , Codice ";
                        cmd.CommandText = SQL;
                        int prog = 0;
                        int myVal = 0;
                        string myValue = "";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                prog++;
                                json += Environment.NewLine + "\t" + "\t" + dr["Codice"].ToString().DQuotedStrSalt();

                                //int.TryParse(dr["C"].ToString(), out myVal);
                                myValue = dr["C"].ToString();
                                if (myValue.ToUpper() == "TRUE")
                                { myVal = 1; }
                                else { myVal = 0; }
                                json += Environment.NewLine + "\t" + "\t" + "\t" + "C".DQuotedStr() + ":" + myVal + ",";


                                myValue = dr["NC"].ToString();
                                if (myValue.ToUpper() == "TRUE")
                                { myVal = 1; }
                                else { myVal = 0; }
                                json += Environment.NewLine + "\t" + "\t" + "\t" + "NC".DQuotedStr() + ":" + myVal + ",";


                                myValue = dr["S"].ToString();
                                if (myValue.ToUpper() == "TRUE")
                                { myVal = 1; }
                                else { myVal = 0; }
                                json += Environment.NewLine + "\t" + "\t" + "\t" + "S".DQuotedStr() + ":" + myVal + ",";

                                myValue = dr["DS"].ToString();
                                if (myValue.ToUpper() == "TRUE")
                                { myVal = 1; }
                                else { myVal = 0; }
                                json += Environment.NewLine + "\t" + "\t" + "\t" + "DS".DQuotedStr() + ":" + myVal + ",";

                                myValue = dr["NP"].ToString();
                                if (myValue.ToUpper() == "TRUE")
                                { myVal = 1; }
                                else { myVal = 0; }
                                json += Environment.NewLine + "\t" + "\t" + "\t" + "NP".DQuotedStr() + ":" + myVal + ",";

                                myValue = dr["NV"].ToString();
                                if (myValue.ToUpper() == "TRUE")
                                { myVal = 1; }
                                else { myVal = 0; }
                                json += Environment.NewLine + "\t" + "\t" + "\t" + "NV".DQuotedStr() + ":" + myVal + ",";

                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Note".DQuotedStr() + ":" + dr["Annotazioni"].ToString().Replace("\r\n", "").DQuotedStr() + ",";

                                if (!String.IsNullOrEmpty(dr["NomeFile"].ToString()))
                                {
                                    int numfoto = 0;
                                    using (SqlCommand cmdFoto = new SqlCommand(SQL, conn))
                                    {
                                        string t = dr["ID"].ToString();
                                        SQL = " SELECT  NomeFile  FROM Foto_Dettagli  WHERE IDDettaglio = @IDDettaglio ";
                                        cmdFoto.Parameters.AddWithValue("@IDDettaglio", dr["ID"]);
                                        cmdFoto.CommandText = SQL;

                                        using (SqlDataReader dr1 = cmdFoto.ExecuteReader())
                                        {
                                            while (dr1.Read())
                                            {
                                                numfoto++;
                                                string a = dr1["NomeFile"].ToString();

                                                if (numfoto == 1)
                                                {
                                                    // Converto to Base 64
                                                    string test = dr1["NomeFile"].ToString();
                                                    Byte[] bytes = File.ReadAllBytes(@"V:\BOT\Ecoprogram\Documenti\CheckMeccanica\" + dr1["NomeFile"].ToString());
                                                    String file = Convert.ToBase64String(bytes);
                                                    json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto1".DQuotedStr() + ":" + file.DQuotedStr() + ","; ;

                                                }

                                                else if (numfoto == 2)
                                                {
                                                    // Converto to Base 64
                                                    string test = dr1["NomeFile"].ToString();
                                                    Byte[] bytes = File.ReadAllBytes(@"V:\BOT\Ecoprogram\Documenti\CheckMeccanica\" + dr1["NomeFile"].ToString());
                                                    String file = Convert.ToBase64String(bytes);
                                                    json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto2".DQuotedStr() + ":" + file.DQuotedStr() + ",";

                                                }
                                                else
                                                {
                                                    // Converto to Base 64
                                                    string test = dr1["NomeFile"].ToString();
                                                    Byte[] bytes = File.ReadAllBytes(@"V:\BOT\Ecoprogram\Documenti\CheckMeccanica\" + dr1["NomeFile"].ToString());
                                                    String file = Convert.ToBase64String(bytes);
                                                    json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto3".DQuotedStr() + ":" + file.DQuotedStr();
                                                }



                                            }
                                            if (numfoto == 1)
                                            {
                                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto2".DQuotedStr() + ":" + "".DQuotedStr() + ",";
                                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto3".DQuotedStr() + ":" + "".DQuotedStr();
                                            }
                                            else if (numfoto == 2)
                                            {

                                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto3".DQuotedStr() + ":" + "".DQuotedStr();
                                            }
                                            else
                                            {

                                            }
                                        }
                                    }


                                }
                                else
                                {
                                    json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto1".DQuotedStr() + ":" + "".DQuotedStr() + ",";
                                    json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto2".DQuotedStr() + ":" + "".DQuotedStr() + ",";
                                    json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto3".DQuotedStr() + ":" + "".DQuotedStr();
                                }
                                if (prog < 60)
                                    json += Environment.NewLine + "\t" + "\t" + "}" + ",";
                                else
                                    json += Environment.NewLine + "\t" + "\t" + "}";

                            }
                        }


                    }

                    json += Environment.NewLine + "}}";
                    //txtHASH.Text += json;


                    //return;
                    string input = "Astrea_2022!_";

                    input += DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString("00") + DateTime.Now.Hour.ToString("00");
                    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                    {
                        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                        byte[] hashBytes = md5.ComputeHash(inputBytes);

                        // Convert the byte array to hexadecimal string
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < hashBytes.Length; i++)
                        {
                            sb.Append(hashBytes[i].ToString("X2"));
                        }
                        hash = sb.ToString().ToLower();
                    }

                    url = "https://tracker.arielcar.it/ws.php?user=astrea&token=" + hash + "&f=postCheckList";
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {

                        Debug.Write(json);
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    try
                    {
                        using (var response = httpWebRequest.GetResponse() as HttpWebResponse)
                        {
                            if (httpWebRequest.HaveResponse && response != null)
                            {
                                using (var reader = new StreamReader(response.GetResponseStream()))
                                {
                                    result = reader.ReadToEnd();
                                    txtHASH.Text += result;
                                    using (SqlCommand cmd = new SqlCommand(SQL, conn))
                                    {
                                        SQL = " UPDATE Ispezioni SET IsTransferred = 1 ,TransferDate = GETDATE() WHERE IDTelaio = @IDTelaio ";
                                        cmd.CommandText = SQL;
                                        cmd.Parameters.AddWithValue("@IDTelaio", aIDTelaio);
                                        int updated = cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    catch (WebException exx)
                    {
                        if (exx.Response != null)
                        {
                            using (var errorResponse = (HttpWebResponse)exx.Response)
                            {
                                using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                                {
                                    string error = reader.ReadToEnd();
                                    result = error;
                                    txtHASH.Text += result;
                                }
                            }

                        }
                    }
                }

                

            }

            txtHASH.Text += Environment.NewLine + Environment.NewLine + "Fine lavoro  : " + DateTime.Now.ToString("dd MM yyyy hh:mm:ss:ms") + Environment.NewLine;
            txtHASH.Text += "************************************************************** " + Environment.NewLine + Environment.NewLine;

            txtHASH.SelectionStart = txtHASH.Text.Length;
            txtHASH.ScrollToCaret();

        }

        private void cmdSetTimer_Click(object sender, EventArgs e)
        {
            int myMillesc = 0;
            string aValue = txtTimerValue.Text;
            bool success = int.TryParse(aValue, out myMillesc);
            wfTimer.Interval = myMillesc;
            wfTimer.Enabled = !wfTimer.Enabled;
            cmdSendToEcoprog.PerformClick();

            if ((wfTimer == null) || (!wfTimer.Enabled))
            {
                //myTimer = new System.Timers.Timer(10000); // Set up the timer for n seconds
                //myTimer.Elapsed += new ElapsedEventHandler(ExecuteTask);
                //myTimer.Enabled = true; // Enable it
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "STOPPED !";


            }
            else
            {
                // if (Utils.IsOdd(DateTime.Now.Hour))
                // {
                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = "Active !";
                // }


            }
        }

        private void wfTimer_Tick(object sender, EventArgs e)
        {
            //Application.DoEvents();

            //MessageBox.Show("TEST");
            //ExecuteTask(new object(), new EventArgs());
            cmdSendToEcoprog.PerformClick();


        }

        private void ExecuteTask(Object source, EventArgs e)
        {

            wfTimer.Enabled = false;
            SendDataToEcoprogram();
            wfTimer.Enabled = true;


        }

        private void SendDataToEcoprogram()
        {
            string aTarga = "";
            string aIDTelaio = "";
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["Ecoprogram"];
            string connStr = "";
            connStr = "Data Source=GESQL01,8194;Initial Catalog=Ecoprogram;Integrated Security=False;MultipleActiveResultSets=True;" + "User ID=" + Utils.AppUser + ";Password=" + Utils.AppPassword;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                //myTimer.Enabled = false;


                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    SQL = " SELECT TOP 1 Targa,IDTelaio, Codice,C,NC,S,DS,NP,NV fROM  Ispezioni_Flat_vw WHERE IsTransferred = 0 AND IsClosed = 1 ORDER BY IDSezione , Codice";
                    cmd.CommandText = SQL;


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            json = "{";
                            json += Environment.NewLine + "\t" + dr["Targa"].ToString().DQuotedStrSalt();
                            aTarga = dr["Targa"].ToString().QuotedStr();
                            aIDTelaio = dr["IDTelaio"].ToString();
                            //MessageBox.Show(aTarga);
                        }
                    }
                }

                if (aTarga == "")
                    return;
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    SQL = " SELECT ID, Codice,C,NC,S,DS,NP,NV,Annotazioni,NomeFile FROM  Ispezioni_Flat_vw WHERE Targa = " + aTarga + "  ORDER BY IDSezione , Codice ";
                    cmd.CommandText = SQL;
                    int prog = 0;
                    int myVal = 0;
                    string myValue = "";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            prog++;
                            json += Environment.NewLine + "\t" + "\t" + dr["Codice"].ToString().DQuotedStrSalt();

                            //int.TryParse(dr["C"].ToString(), out myVal);
                            myValue = dr["C"].ToString();
                            if (myValue.ToUpper() == "TRUE")
                            { myVal = 1; }
                            else { myVal = 0; }
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "C".DQuotedStr() + ":" + myVal + ",";


                            myValue = dr["NC"].ToString();
                            if (myValue.ToUpper() == "TRUE")
                            { myVal = 1; }
                            else { myVal = 0; }
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "NC".DQuotedStr() + ":" + myVal + ",";


                            myValue = dr["S"].ToString();
                            if (myValue.ToUpper() == "TRUE")
                            { myVal = 1; }
                            else { myVal = 0; }
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "S".DQuotedStr() + ":" + myVal + ",";

                            myValue = dr["DS"].ToString();
                            if (myValue.ToUpper() == "TRUE")
                            { myVal = 1; }
                            else { myVal = 0; }
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "DS".DQuotedStr() + ":" + myVal + ",";

                            myValue = dr["NP"].ToString();
                            if (myValue.ToUpper() == "TRUE")
                            { myVal = 1; }
                            else { myVal = 0; }
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "NP".DQuotedStr() + ":" + myVal + ",";

                            myValue = dr["NV"].ToString();
                            if (myValue.ToUpper() == "TRUE")
                            { myVal = 1; }
                            else { myVal = 0; }
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "NV".DQuotedStr() + ":" + myVal + ",";

                            json += Environment.NewLine + "\t" + "\t" + "\t" + "Note".DQuotedStr() + ":" + dr["Annotazioni"].ToString().DQuotedStr() + ",";

                            if (!String.IsNullOrEmpty(dr["NomeFile"].ToString()))
                            {
                                int numfoto = 0;
                                using (SqlCommand cmdFoto = new SqlCommand(SQL, conn))
                                {
                                    string t = dr["ID"].ToString();
                                    SQL = " SELECT  NomeFile  FROM Foto_Dettagli  WHERE IDDettaglio = @IDDettaglio ";
                                    cmdFoto.Parameters.AddWithValue("@IDDettaglio", dr["ID"]);
                                    cmdFoto.CommandText = SQL;

                                    using (SqlDataReader dr1 = cmdFoto.ExecuteReader())
                                    {
                                        while (dr1.Read())
                                        {
                                            numfoto++;
                                            string a = dr1["NomeFile"].ToString();

                                            if (numfoto == 1)
                                            {
                                                // Converto to Base 64
                                                string test = dr1["NomeFile"].ToString();
                                                Byte[] bytes = File.ReadAllBytes(@"V:\BOT\Ecoprogram\Documenti\CheckMeccanica\" + dr1["NomeFile"].ToString());
                                                String file = Convert.ToBase64String(bytes);
                                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto1".DQuotedStr() + ":" + file.DQuotedStr() + ","; ;

                                            }

                                            else if (numfoto == 2)
                                            {
                                                // Converto to Base 64
                                                string test = dr1["NomeFile"].ToString();
                                                Byte[] bytes = File.ReadAllBytes(@"V:\BOT\Ecoprogram\Documenti\CheckMeccanica\" + dr1["NomeFile"].ToString());
                                                String file = Convert.ToBase64String(bytes);
                                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto2".DQuotedStr() + ":" + file.DQuotedStr() + ",";

                                            }
                                            else
                                            {
                                                // Converto to Base 64
                                                string test = dr1["NomeFile"].ToString();
                                                Byte[] bytes = File.ReadAllBytes(@"V:\BOT\Ecoprogram\Documenti\CheckMeccanica\" + dr1["NomeFile"].ToString());
                                                String file = Convert.ToBase64String(bytes);
                                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto3".DQuotedStr() + ":" + file.DQuotedStr();
                                            }



                                        }
                                        if (numfoto == 1)
                                        {
                                            json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto2".DQuotedStr() + ":" + "".DQuotedStr() + ",";
                                            json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto3".DQuotedStr() + ":" + "".DQuotedStr();
                                        }
                                        else if (numfoto == 2)
                                        {

                                            json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto3".DQuotedStr() + ":" + "".DQuotedStr();
                                        }
                                        else
                                        {

                                        }
                                    }
                                }


                            }
                            else
                            {
                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto1".DQuotedStr() + ":" + "".DQuotedStr() + ",";
                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto2".DQuotedStr() + ":" + "".DQuotedStr() + ",";
                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto3".DQuotedStr() + ":" + "".DQuotedStr();
                            }
                            if (prog < 60)
                                json += Environment.NewLine + "\t" + "\t" + "}" + ",";
                            else
                                json += Environment.NewLine + "\t" + "\t" + "}";

                        }
                    }


                }

                json += Environment.NewLine + "}}";
                txtHASH.Text += json;
                MessageBox.Show("Eccolo");
                return;
                string input = "Astrea_2022!_";

                input += DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString("00") + DateTime.Now.Hour.ToString("00");
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    // Convert the byte array to hexadecimal string
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("X2"));
                    }
                    hash = sb.ToString().ToLower();
                }

                url = "https://tracker.arielcar.it/ws.php?user=astrea&token=" + hash + "&f=postCheckList";
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    Debug.Write(json);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                try
                {
                    using (var response = httpWebRequest.GetResponse() as HttpWebResponse)
                    {
                        if (httpWebRequest.HaveResponse && response != null)
                        {
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                result = reader.ReadToEnd();
                                txtHASH.Text += result;
                                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                                {
                                    SQL = " UPDATE Ispezioni SET IsTransferred = 1,TransferDate = GETDATE() WHERE IDTelaio = @IDTelaio";
                                    cmd.CommandText = SQL;
                                    cmd.Parameters.AddWithValue("@IDTelaio", aIDTelaio);
                                    int updated = cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                catch (WebException exx)
                {
                    if (exx.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)exx.Response)
                        {
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                string error = reader.ReadToEnd();
                                result = error;
                                txtHASH.Text += result;
                            }
                        }

                    }
                }

            }
        }

        private void cmdClean_Click(object sender, EventArgs e)
        {
            txtHASH.Text = "";
            txtHASH.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtTimerValue.Text = "86400000";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtTimerValue.Text = "43200000";
        }

        private void cmdCountVin_Click(object sender, EventArgs e)
        {
            txtHASH.Text = "";
            string connStr = "";
            int cnt = 0;
            connStr = "Data Source=GESQL01,8194;Initial Catalog=Ecoprogram;Integrated Security=False;MultipleActiveResultSets=True;" + "User ID=" + Utils.AppUser + ";Password=" + Utils.AppPassword;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                //myTimer.Enabled = false;


                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    SQL = " SELECT  COUNT(DISTINCT(Targa)) AS Cnt FROM Ispezioni_Flat_vw WHERE IsTransferred = 0 AND IsClosed = 1 ";

                    cmd.CommandText = SQL;

                    cnt = (int)cmd.ExecuteScalar();

                    try
                    {
                        txtHASH.Text = "There are " + cnt.ToString() + " records to be sent.";
                    }
                    catch
                    {
                        txtHASH.Text = "There are 0 records to be sent.";
                    }
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtTimerValue.Text = "14400000";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtTimerValue.Text = "7200000";
        }
    }
}
