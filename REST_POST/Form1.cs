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

namespace REST_POST
{
    public partial class Form1 : Form
    {
        string csLocal = Utils.GetConnectionStringComplete("Ecoprogram");
        string SQL = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdHash_MD5_Click(object sender, EventArgs e)
        {
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
                txtHASH.Text = sb.ToString().ToLower();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            Upload(openFileDialog.FileName);
        }

        private void Upload(string fileName)
        {
            var client = new WebClient();
            var uri = new Uri("https://tracker.arielcar.it/ws.php?user=astrea&token=af9c0281e15b34116af0b8d9441d5394&f=postCheckList");
            try
            {
                //client.Headers.Add("fileName", System.IO.Path.GetFileName(fileName));
                var data = System.IO.File.ReadAllBytes(fileName);
                var response = client.UploadFile(uri, fileName);
            }
            catch(WebException exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public class VIN
        {
            public string Targa { get; set; }
            public string Componente { get; set; }
            
            public IList<string> Valori { get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VIN account = new VIN
            {
                Targa = "AA000AA",
                Componente = "1.3.3",

                Valori = new List<string>
                {
                    "C"
                }
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(account, Formatting.Indented);
            // {
            //   "Email": "james@example.com",
            //   "Active": true,
            //   "CreatedDate": "2013-01-20T00:00:00Z",
            //   "Roles": [
            //     "User",
            //     "Admin"
            //   ]
            // }
            txtHASH.Text = json;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string json = "{" + Environment.NewLine;
            
            json += new string(' ', 10)+  @"""AA00AA""" +": {";
            txtHASH.Text = json;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/octet-stream");
                using (Stream fileStream = File.OpenRead(@"C:\test\struttura-json.json"))
                using (Stream requestStream = client.OpenWrite(new Uri("https://tracker.arielcar.it/ws.php?user=astrea&token=67c3e4ccb8ed96d79f7a96a3567afd56&f=postCheckList"), "POST"))
                {
                    try
                    {
                        var response = fileStream.CopyToAsync(requestStream);
                        txtHASH.Text = response.ToString();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            string myJson = "ciao";
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync("https://tracker.arielcar.it/ws.php?user=astrea&token=4b4958d65f60f57961c94e176ddc5ede&f=postCheckList", new StringContent(myJson, Encoding.UTF8, "application/json"));
                    txtHASH.Text = response.ToString();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://tracker.arielcar.it/ws.php?user=astrea&token=3e69cc1ee0944cad002137be16910e76&f=postCheckList");
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    
                });
                json = "ciao";
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            // QUESTO E' QUELLO CORRETTO CHE FUNGE !
            var values = new Dictionary<string, string>
            {
                { "test1", "hello" },
                { "test2", "world" }
            };

            var content = new FormUrlEncodedContent(values);

            string myContent = content.ReadAsStringAsync().Result;
            var client = new HttpClient();
            var response = await client.PostAsync("https://tracker.arielcar.it/ws.php?user=astrea&token=52860023271770f0679adcc7e6853f14&f=postCheckList", content);

            var responseString = await response.Content.ReadAsStringAsync();
            txtHASH.Text = responseString;


        }

        private async void button8_Click(object sender, EventArgs e)
        {
            VIN myVin = new VIN
            {
                Targa = "AA000AA",
                Componente = "1.3.3",
            };

            string a = "{test}";
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(a);
            string csv = "content here";

            var stringContent = new StringContent(csv, Encoding.UTF32, "application/json");
            string myContent = stringContent.ReadAsStringAsync().Result;
            var client = new HttpClient();
            var response = await client.PostAsync("https://tracker.arielcar.it/ws.php?user=astrea&token=52860023271770f0679adcc7e6853f14&f=postCheckList", stringContent);
            
            //txtHASH.Text = myContent;
            var responseString = await response.Content.ReadAsStringAsync();
            txtHASH.Text += responseString;
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //var client = new RestClient("http://example.com");
            //// client.Authenticator = new HttpBasicAuthenticator(username, password);
            //var request = new RestRequest("resource/{id}");
            //request.AddParameter("thing1", "Hello");
            //request.AddParameter("thing2", "world");
            //request.AddHeader("header", "value");
            //request.AddFile("file", path);
            //var response = client.Post(request);
            //var content = response.Content; // Raw content as string
            //var response2 = client.Post<Person>(request);
            //var name = response2.Data.Name;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            

                HttpClient client = new HttpClient();
                var uri = new Uri("https://tracker.arielcar.it/ws.php?user=astrea&token=52860023271770f0679adcc7e6853f14&f=postCheckList/");


                VIN myVin = new VIN();

                 myVin.Targa = "AA000";
                 myVin.Componente = "XXX";

                string json = "test";//JsonConvert.SerializeObject(myVin);


                try
                {
                    var content = new StringContent(json);

                //HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(false);
                // HttpResponseMessage response = client.PostAsync(uri, content).Result;
                HttpResponseMessage response = client.PostAsync("https://tracker.arielcar.it/ws.php?user=astrea&token=52860023271770f0679adcc7e6853f14&f=postCheckList", content).Result;
                txtHASH.Text += response;
                }
                catch (HttpRequestException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }

               
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://tracker.arielcar.it/ws.php?user=astrea&token=52860023271770f0679adcc7e6853f14&f=postCheckList");
            httpWebRequest.ContentType =   "application/json";
            httpWebRequest.Method = "POST";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))

            {

                string json = "{\"user\":\"test\"," +

                              "\"password\":\"bla\"}";



                streamWriter.Write(json);

            }



            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            txtHASH.Text += httpResponse;

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))

            {

                var result = streamReader.ReadToEnd();

            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://tracker.arielcar.it/ws.php?user=astrea&token=52860023271770f0679adcc7e6853f14&f=postCheckList");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new

            StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    Username = "maurizio",
                    Password = "test"
                });

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string url = "https://tracker.arielcar.it/ws.php?user=astrea&token=382d4c086f0ef9038a17ae6fad0a56d6&f=postCheckList";
            PostRequest(url);
        }

        public  string Quoted(string str)
        {
            return "\"" + str + "\"";
        }

        private  string PostRequest(string url)
        {
            
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            string result = "";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{" + Environment.NewLine + "\t" + ("AA000AA").DQuotedStr() + ": {"+ Environment.NewLine + "\t" + "\t" + ("1.3.3").DQuotedStr() +":" + "{}}}";
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
                        }
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)e.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            result = error;
                        }
                    }

                }
            }

            return result;

        }

        private void button14_Click(object sender, EventArgs e)
        {
            string str = "{\"objects\":[{\"id\":1,\"title\":\"Book\",\"position_x\":0,\"position_y\":0,\"position_z\":0,\"rotation_x\":0,\"rotation_y\":0,\"rotation_z\":0,\"created\":\"2016-09-21T14:22:22.817Z\"},{\"id\":2,\"title\":\"Apple\",\"position_x\":0,\"position_y\":0,\"position_z\":0,\"rotation_x\":0,\"rotation_y\":0,\"rotation_z\":0,\"created\":\"2016-09-21T14:22:52.368Z\"}]}";
            dynamic json = JsonConvert.DeserializeObject(str);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // creo HASH
            txtHASH.Text = "";
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
                txtHASH.Text = sb.ToString().ToLower();
            }

            string hash = txtHASH.Text;

            //string json = "{" + Environment.NewLine + "\t" + ("AA000AA").DQuotedStr() + ": {" + Environment.NewLine + "\t" + "\t" + ("1.3.3").DQuotedStr() + ":" + "{}}}";
            string json = "{" ;


            using (SqlConnection connLocal = new SqlConnection(csLocal))
            {
                connLocal.Open();
                string aTarga = "";
               
                using (SqlCommand cmd = new SqlCommand(SQL, connLocal))
                {
                    SQL = " SELECT TOP 1 Targa,Codice,C,NC,S,DS,NP,NV fROM  Ispezioni_Flat_vw WHERE IsTransferred = 0 ORDER BY IDSezione , Codice";
                    cmd.CommandText = SQL;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            json += Environment.NewLine + "\t" + dr["Targa"].ToString().DQuotedStrSalt() ;
                            aTarga = dr["Targa"].ToString().QuotedStr();
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand(SQL, connLocal))
                {
                    SQL = " SELECT Codice,C,NC,S,DS,NP,NV,Annotazioni FROM  Ispezioni_Flat_vw WHERE Targa = " + aTarga +  "  ORDER BY IDSezione , Codice ";
                    cmd.CommandText = SQL;
                    int prog = 0;
                    int myVal= 0;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            prog++;
                            json += Environment.NewLine + "\t" + "\t" + dr["Codice"].ToString().DQuotedStrSalt();
                            int.TryParse(dr["C"].ToString(), out myVal);
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "C".DQuotedStr() + ":" + myVal +",";
                            int.TryParse(dr["NC"].ToString(), out myVal);
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "NC".DQuotedStr() + ":" + myVal + ",";
                            int.TryParse(dr["S"].ToString(), out myVal);
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "S".DQuotedStr() + ":" + myVal + ",";
                            int.TryParse(dr["DS"].ToString(), out myVal);
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "DS".DQuotedStr() + ":" + myVal + ",";
                            int.TryParse(dr["NP"].ToString(), out myVal);
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "NP".DQuotedStr() + ":" + myVal + ",";
                            int.TryParse(dr["NV"].ToString(), out myVal);
                            json += Environment.NewLine + "\t" + "\t" + "\t" + "NV".DQuotedStr() + ":" + myVal + ",";

                            json += Environment.NewLine + "\t" + "\t" + "\t" + "Note".DQuotedStr() + ":" + dr["Annotazioni"].ToString().DQuotedStr() + ","; 

                            if (prog == 1)
                            {
                                // Converto to Base 64
                                Byte[] bytes = File.ReadAllBytes(@"C:\Test\TEST.jpg");
                                String file = Convert.ToBase64String(bytes);
                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto".DQuotedStr() + ":" + file.DQuotedStr();
                            }
                            else
                            {
                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto".DQuotedStr() + ":" + "".DQuotedStr();
                            }
                            if (prog <60)
                                json += Environment.NewLine + "\t" + "\t" + "}" + ",";
                            else
                                json += Environment.NewLine + "\t" + "\t" + "}" ;
                        }
                    }

                   
                }

                json += Environment.NewLine + "}}";
                txtHASH.Text = json;
            }

            return;
            string url = "https://tracker.arielcar.it/ws.php?user=astrea&token=" + hash + "&f=postCheckList";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            string result = "";
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
                        }
                    }

                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Byte[] bytes = File.ReadAllBytes(@"C:\Test\TEST.jpg");
            String file = Convert.ToBase64String(bytes);

            
            txtHASH.Text = file;

            //Byte[] bytesRet = Convert.FromBase64String(file);
            //File.WriteAllBytes(@"C:\Test\pippoNEW.jpg", bytesRet);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Byte[] bytesRet = Convert.FromBase64String(txtHASH.Text);
            File.WriteAllBytes(@"C:\Test\TEST02.jpg", bytesRet);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            // creo HASH
            txtHASH.Text = "";
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
                txtHASH.Text = sb.ToString().ToLower();
            }

            string hash = txtHASH.Text;

            //string json = "{" + Environment.NewLine + "\t" + ("AA000AA").DQuotedStr() + ": {" + Environment.NewLine + "\t" + "\t" + ("1.3.3").DQuotedStr() + ":" + "{}}}";
            string json = "{";


            using (SqlConnection connLocal = new SqlConnection(csLocal))
            {
                connLocal.Open();
                string aTarga = "";

                using (SqlCommand cmd = new SqlCommand(SQL, connLocal))
                {
                    SQL = " SELECT TOP 1 Targa,Codice,C,NC,S,DS,NP,NV fROM  Ispezioni_Flat_vw WHERE IsTransferred = 0 AND Targa = 'FT705YW' ORDER BY IDSezione , Codice";
                    cmd.CommandText = SQL;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            json += Environment.NewLine + "\t" + dr["Targa"].ToString().DQuotedStrSalt();
                            aTarga = dr["Targa"].ToString().QuotedStr();
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand(SQL, connLocal))
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
                                using (SqlCommand cmdFoto = new SqlCommand(SQL, connLocal))
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
                                                Byte[] bytes = File.ReadAllBytes(@"C:\Test\" + dr1["NomeFile"].ToString());
                                                String file = Convert.ToBase64String(bytes);
                                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto1".DQuotedStr() + ":" + file.DQuotedStr() + ","; ;
                                               
                                            }

                                            else if (numfoto == 2)
                                            {
                                                // Converto to Base 64
                                                string test = dr1["NomeFile"].ToString();
                                                Byte[] bytes = File.ReadAllBytes(@"C:\Test\" + dr1["NomeFile"].ToString());
                                                String file = Convert.ToBase64String(bytes);
                                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto2".DQuotedStr() + ":" + file.DQuotedStr() + ","; 

                                            }
                                            else
                                            {
                                                // Converto to Base 64
                                                string test = dr1["NomeFile"].ToString();
                                                Byte[] bytes = File.ReadAllBytes(@"C:\Test\" + dr1["NomeFile"].ToString());
                                                String file = Convert.ToBase64String(bytes);
                                                json += Environment.NewLine + "\t" + "\t" + "\t" + "Foto3".DQuotedStr() + ":" + file.DQuotedStr();
                                            }

                                            
                                            
                                        }
                                        if (numfoto==1)
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
                txtHASH.Text = json;
            }

            //return;
            string url = "https://tracker.arielcar.it/ws.php?user=astrea&token=" + hash + "&f=postCheckList";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            string result = "";
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
                        }
                    }

                }
            }
        }
    }
}
