using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BackupMaker
{
    public partial class frmBackupMaker : Form
    {
        private bool firstLoad = false;
        private Properties.Settings settings;

        public frmBackupMaker()
        {
            InitializeComponent();
        }

        private void frmBackupMaker_Load(object sender, EventArgs e)
        {
            lbTableName.Text = String.Empty;
            lbTableName.Visible = false;
            progressBar1.Visible = false;
            label5.Visible = false;
            settings = Properties.Settings.Default;
            firstLoad = true;
            cbHost.Text = settings.Host;
            cbPort.Text = settings.Port;
            cbUser.Text = settings.User;
            cbPassword.Text = settings.Password;
            cbSchema.Text = settings.Schema;
            firstLoad = false;
            using (LinkToSQL linkSQL = new LinkToSQL())
            {
                cbSchema.Items.AddRange(linkSQL.GetDatabases());
                clbTables.Items.AddRange(linkSQL.GetTables());
            }
        }

        private void cbHost_TextChanged(object sender, EventArgs e)
        {
            if (!firstLoad)
            {
                switch ((sender as ComboBox).Name)
                {
                    case "cbHost": settings.Host = cbHost.Text; settings.Save(); break;
                    case "cbPort": settings.Port = cbPort.Text; settings.Save(); break;
                    case "cbUser": settings.User = cbUser.Text; settings.Save(); break;
                    case "cbPassword": settings.Password = cbPassword.Text; settings.Save(); break;
                    case "cbSchema":
                        settings.Schema = cbSchema.Text;
                        settings.Save();
                        //---------------------
                        clbTables.Items.Clear();
                        using (LinkToSQL linkSQL = new LinkToSQL())
                            clbTables.Items.AddRange(linkSQL.GetTables());
                        break;
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (!backgroundExportWorker.IsBusy)
                {
                    btnExport.Enabled = false;
                    btnImport.Enabled = false;
                    lbTableName.Visible = true;
                    progressBar1.Visible = true;
                    label5.Visible = true;
                    backgroundExportWorker.RunWorkerAsync(
                        new Tuple<string, string>(
                            folderBrowserDialog.SelectedPath,
                            settings.Schema));
                }
            }
        }

        private void ExportSchema(BackgroundWorker worker, string selectedDir, string schema)
        {
            string schemaDir = selectedDir + "\\" + schema;
            if (!Directory.Exists(schemaDir)) Directory.CreateDirectory(schemaDir);
            using (LinkToSQL linkSQL = new LinkToSQL())
            {
                SaveCreateOperator(schemaDir + "\\" + "CreateSchemaOperator.txt",
                    linkSQL.GetCreateSchemaOperator(settings.Schema), linkSQL);
                for (int i = 0; i < clbTables.Items.Count; i++)
                {
                    if (clbTables.GetItemChecked(i))
                    {
                        string tablename = clbTables.Items[i].ToString();
                        worker.ReportProgress(
                            (int)(i * 100f / clbTables.Items.Count), tablename);
                        string tableDir = schemaDir + "\\" + tablename;
                        if (!Directory.Exists(tableDir))
                            Directory.CreateDirectory(tableDir);
                        SaveCreateOperator(tableDir + "\\" + "CreateTableOperator.txt",
                            linkSQL.GetCreateTableOperator(tablename), linkSQL);
                        ExportTable(linkSQL, tableDir, tablename);
                    }
                }
            }
        }

        private void ExportTable(LinkToSQL linkSQL, string dir, string tablename)
        {
            string filename = dir + "\\" + "TableContent.csv";
            if (File.Exists(filename)) File.Delete(filename);
            IDictionary<string, string> columns = linkSQL.GetColumnsInfo(tablename);
            string selectSQL = BuildSelectOperator(linkSQL, tablename, columns);
            bool headadded = false;
            int row = 0;
            while (true)
            {
                List<object[]> rowData = linkSQL.GetRows(selectSQL, row, 100);
                if (rowData.Count > 0)
                {
                    using (StreamWriter sw = File.AppendText(filename))
                    {
                        StringBuilder sb;
                        if (!headadded)
                        {
                            //---------- Заголовок ------------
                            sb = new StringBuilder();
                            foreach (KeyValuePair<string, string> kvp in columns)
                            {
                                sb.Append(kvp.Key);
                                sb.Append("\t");
                            }
                            sb.Remove(sb.Length - 1, 1);
                            sw.WriteLine(sb.ToString());
                            headadded = true;
                        }
                        //---------- Столбцы -------------
                        foreach (object[] items in rowData)
                        {
                            if (items.Length == columns.Count)
                            {
                                sb = new StringBuilder();
                                int i = 0;
                                string name = String.Empty;
                                foreach (KeyValuePair<string, string> kvp in columns)
                                {
                                    if (kvp.Key.Equals("name"))
                                        name = items[i].ToString();
                                    string datatype = kvp.Value.ToLower();
                                    if (datatype.IndexOf("int") >= 0)
                                        sb.Append(items[i].ToString());
                                    else
                                        if (datatype.StartsWith("double"))
                                            sb.Append(items[i].ToString().Replace(',', '.'));
                                        else
                                            if (datatype.StartsWith("timestamp"))
                                                sb.Append(
                                                    ((DateTime)items[i]).ToString("yyyy-MM-dd HH:mm:ss"));
                                            else
                                                if (datatype.IndexOf("blob") >= 0)
                                                {
                                                    sb.Append("[blob]");
                                                    SaveBlobToFile(linkSQL, dir, tablename, name);
                                                }
                                                else
                                                    sb.Append(items[i].ToString());
                                    sb.Append("\t");
                                    i++;
                                }
                                sb.Remove(sb.Length - 1, 1);
                                sw.WriteLine(sb.ToString());
                            }
                        }
                        sw.Flush();
                    }
                }
                else
                    break;
                row += 100;
            }
        }

        private static void SaveBlobToFile(LinkToSQL linkSQL, string dir,
            string tablename, string name)
        {
            byte[] image = linkSQL.GetBlobData(tablename, name);
            string filename = dir + "\\" + name + ".blob";
            if (File.Exists(filename)) File.Delete(filename);
            File.WriteAllBytes(filename, image);
        }

        private string BuildSelectOperator(LinkToSQL linkSQL, string tablename,
            IDictionary<string, string> columns)
        {
            StringBuilder sb = new StringBuilder("SELECT ");
            foreach (KeyValuePair<string, string> kvp in columns)
                sb.Append("`" + kvp.Key + "`, ");
            sb.Remove(sb.Length - 2, 2);
            sb.Append(" FROM `" + tablename + "`");
            return sb.ToString();
        }

        private void SaveCreateOperator(string fileDir, string value, LinkToSQL linkSQL)
        {
            using (StreamWriter file = File.CreateText(fileDir))
            {
                file.WriteLine(value);
                file.Flush();
            }
        }

        private string LoadCreateOperator(string fileDir)
        {
            string result = String.Empty;
            if (File.Exists(fileDir))
            {
                using (StreamReader file = File.OpenText(fileDir))
                {
                    result = file.ReadToEnd();
                    result = result.Replace('\n', ' ').Replace('\r', ' ');
                }
            }
            return result;
        }

        private void cbAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbTables.Items.Count; i++)
            {
                clbTables.SetItemChecked(i, cbAll.Checked);
            }
        }

        private void backgroundExportWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            Tuple<string, string> args = (Tuple<string, string>)e.Argument;
            ExportSchema(worker, args.Item1, args.Item2);
        }

        private void backgroundExportWorker_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            lbTableName.Text = (string)e.UserState;
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundExportWorker_RunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs e)
        {
            lbTableName.Visible = false;
            progressBar1.Visible = false;
            label5.Visible = false;
            lbTableName.Text = String.Empty;
            progressBar1.Value = 0;
            btnExport.Enabled = true;
            btnImport.Enabled = true;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (!backgroundImportWorker.IsBusy)
                {
                    btnExport.Enabled = false;
                    btnImport.Enabled = false;
                    lbTableName.Visible = true;
                    progressBar1.Visible = true;
                    label5.Visible = true;
                    backgroundImportWorker.RunWorkerAsync(
                            folderBrowserDialog.SelectedPath);
                }
            }
        }

        private void ImportSchema(BackgroundWorker worker, string schemaDir)
        {
            if (Directory.Exists(schemaDir))
            {
                string schema = Path.GetFileName(schemaDir);
                settings.Schema = schema;
                settings.Save();
                using (LinkToSQL linkSQL = new LinkToSQL())
                {
                    if (linkSQL.Connected)
                        linkSQL.DropSchema(schema);
                }
                settings.Schema = String.Empty; ;
                settings.Save();
                using (LinkToSQL linkSQL = new LinkToSQL())
                {
                    string createSchemaOperator =
                        LoadCreateOperator(schemaDir + "\\" + "CreateSchemaOperator.txt");
                    if (linkSQL.Connected)
                        linkSQL.CreateThis(createSchemaOperator);
                }
                using (LinkToSQL linkSQL = new LinkToSQL())
                {
                    linkSQL.UseSchema(schema);
                    string[] dirs = Directory.GetDirectories(schemaDir, "*.*");
                    int i = 0;
                    foreach (string dirname in dirs)
                    {
                        string tablename = Path.GetFileName(dirname);
                        worker.ReportProgress(
                            (int)(i * 100f / dirs.Length), tablename);
                        linkSQL.DropTable(tablename);
                        string createTableOperator =
                            LoadCreateOperator(dirname + "\\" + "CreateTableOperator.txt");
                        linkSQL.CreateThis(createTableOperator);
                        ImportTable(linkSQL, dirname, tablename);
                        i++;
                    }
                }
            }
        }

        private void ImportTable(LinkToSQL linkSQL, string dir, string tablename)
        {
            string filename = dir + "\\" + "TableContent.csv";
            if (File.Exists(filename))
            {
                IDictionary<string, string> columns = linkSQL.GetColumnsInfo(tablename);
                StringBuilder insertOperator = new StringBuilder(
                    "INSERT INTO `" + tablename + "` (");
                List<string> colsToAdd = columns.Keys.ToList();
                List<string> colsNames = new List<string>();
                foreach (string colname in colsToAdd)
                    colsNames.Add("`" + colname + "`");
                insertOperator.Append(String.Join(", ", colsNames.ToArray()));
                insertOperator.Append(") VALUES (");
                List<string> colsValues = new List<string>();
                foreach (string colname in colsToAdd)
                    colsValues.Add("@" + colname );
                insertOperator.Append(String.Join(", ", colsValues.ToArray()));
                insertOperator.Append(")");

                using (StreamReader sr = File.OpenText(filename))
                {
                    int lineno = 0;
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        if (line.IndexOf('\t') < 0)
                        {
                            line = line.Replace("\",\"", "\t");
                            line = line.Replace(",\"", "\t");
                            line = line.Replace("\",", "\t");
                            line = line.Substring(1, line.Length - 2);
                        }
                        string[] items = line.Split(new char[] { '\t' });
                        if (lineno > 0)
                        {
                            object[] vals = new object[items.Length];
                            int i = 0;
                            string name = String.Empty;
                            foreach (KeyValuePair<string, string> kvp in columns)
                            {
                                if (kvp.Value.IndexOf("blob") >= 0)
                                {
                                    filename = dir + "\\" + name + ".blob";
                                    if (File.Exists(filename))
                                    {
                                        vals[i] = File.ReadAllBytes(filename);
                                    }
                                    else
                                        vals[i] = null; 
                                }
                                else
                                    vals[i] = items[i];
                                if (kvp.Key.Equals("name"))
                                    name = vals[i].ToString();
                                i++;
                            }
                            linkSQL.InsertThis(insertOperator.ToString(),
                                colsValues.ToArray(),
                                vals);
                        }
                        lineno++;
                    }
                }
            }
        }

        private void backgroundImportWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            ImportSchema(worker, (string)e.Argument);
        }

        private void backgroundImportWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lbTableName.Text = (string)e.UserState;
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundImportWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lbTableName.Visible = false;
            progressBar1.Visible = false;
            label5.Visible = false;
            lbTableName.Text = String.Empty;
            progressBar1.Value = 0;
            btnExport.Enabled = true;
            btnImport.Enabled = true;
        }

    }
}
