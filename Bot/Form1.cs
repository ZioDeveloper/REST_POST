using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace Bot
{
    public partial class Form1 : Form
    {
        string myFileName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog theDialog = new OpenFileDialog();
                theDialog.Title = "Open CVS File";
                theDialog.Filter = "CSV files|*.csv|All files (*.*)|*.*";
                //theDialog.InitialDirectory = @"C:\test";
                if (theDialog.ShowDialog() == DialogResult.OK)
                    lblFileName.Text = theDialog.FileName.ToString();

                this.Cursor = Cursors.WaitCursor;

                myFileName = System.IO.Path.GetFileName(theDialog.FileName.ToString());
                
                Application.DoEvents();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmdImportaSingoloFile_Click(object sender, EventArgs e)
        {
            myFileName = lblFileName.Text;
            Cursor = Cursors.WaitCursor;

            DataTable imported_data = GetDataFromFile();

            if (imported_data == null) return;

            SaveImportDataToDatabase(imported_data);

            //MessageBox.Show("load data succ.....!");
            //txtFileName.Text = string.Empty;
            //txtLog.Text += Environment.NewLine + "Importazione terminata.";
            Application.DoEvents();
            Cursor = Cursors.Default;
        }

        private DataTable GetDataFromFile()
        {
            
            Application.DoEvents();

            DataTable importedData = new DataTable();
            int cnt = 0;
            try
            {
                using (StreamReader sr = new StreamReader(lblFileName.Text))
                {


                    string header = sr.ReadLine();
                    if (string.IsNullOrEmpty(header))
                    {

                        MessageBox.Show("no file data");
                        return null;
                    }



                    string[] headerColumns = header.Split(';');
                    foreach (string headerColumn in headerColumns)
                    {
                        string aLine = headerColumn.Replace("\"", "").Replace("\"", "");
                        importedData.Columns.Add(aLine);
                    }



                    while (!sr.EndOfStream)
                    {

                        string line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line)) continue;
                        string[] fields = line.Split(';');
                        DataRow importedRow = importedData.NewRow();

                        for (int i = 0; i < fields.Count(); i++)
                        {

                            importedRow[i] = fields[i];
                            cnt++;

                        }

                        importedData.Rows.Add(importedRow);
                    }
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("Riga " + cnt.ToString() + Environment.NewLine + e.Message);
            }

            return importedData;
        }

        private void SaveImportDataToDatabase(DataTable imported_data)
        {
            string sqlstm = "";
            int I = 0;
            //string cs = Utils.GetConnectionStringByName("WiseDB");
            //cs += "User ID=" + Utils.AppUser + ";Password=" + Utils.AppPassword;
            
                foreach (DataRow importRow in imported_data.Rows)
                {
                    MessageBox.Show(importRow["targa"].ToString());
                }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string contents;
            using (StreamReader sr = new StreamReader(@"C:\MAV\EcoProg\01_usati_pci.csv"))
            {
                contents = sr.ReadToEnd();
                contents = contents.Replace("\"", "");
                sr.Close();
            }

            using (StreamWriter sw = new StreamWriter(@"C:\MAV\EcoProg\01_usati_pci.csv"))
            {
                sw.Write(contents);
                sw.Close();
            }
        }
    }
}
