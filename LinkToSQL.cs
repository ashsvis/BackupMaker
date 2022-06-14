using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace BackupMaker
{
    public sealed class LinkToSQL: IDisposable
    {
        public MySqlConnection myConnection;
        private Properties.Settings settings;
        private string connectionString = String.Empty;
        private bool serverConnected = false;
        private static string lasterrorString = String.Empty;

        public LinkToSQL()
        {
            settings = Properties.Settings.Default;
            connectionString = "server=" + settings.Host +
                ";user=" + settings.User +
                ";database=" + settings.Schema +
                ";port=" + settings.Port +
                ";password=" + settings.Password + ";";
            myConnection = new MySqlConnection(connectionString);
            serverConnected = tryToConnect();
        }

        public byte[] GetBlobData(string table, string name)
        {
            byte[] result = new byte[] { };
            if (serverConnected)
            {
                string SQL = "select `value` from `" + table + "` where (`name`=@name)";
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    object obj = command.ExecuteScalar();
                    result = (byte[])obj;
                }
            }
            return result;
        }

        public List<object[]> GetRows(string SQL, int row, int count)
        {
            List<object[]> result = new List<object[]>();
            if (serverConnected)
            {
                using (MySqlCommand command =
                    new MySqlCommand(SQL + " LIMIT " + row + ", " + count, myConnection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<object> list = new List<object>();
                            for (int col = 0; col < reader.FieldCount; col++)
                                list.Add(reader.GetValue(col));
                            result.Add(list.ToArray());
                        }
                    }
                }
            }
            return result;
        }

        //public List<string> GetOneRow(string SQL, int row)
        //{
        //    List<string> result = new List<string>();
        //    if (serverConnected)
        //    {
        //        using (MySqlCommand command = 
        //            new MySqlCommand(SQL + " LIMIT " + row + ", 1", myConnection))
        //        {
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    for (int col = 0; col < reader.FieldCount; col++)
        //                        result.Add(reader.GetString(col));
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}

        public IDictionary<string, string> GetColumnsInfo(string tablename)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            if (serverConnected)
            {
                string SQL = String.Format("SHOW COLUMNS FROM `{0}`", tablename);
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(0), reader.GetString(1));
                        }
                    }
                }
            }
            return result;
        }

        private string GetCreateOperator(string SQL)
        {
            string result = String.Empty;
            if (serverConnected)
            {
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetString(1);
                        }
                    }
                }
            }
            return result;
        }

        public string GetCreateSchemaOperator(string item)
        {
            return GetCreateOperator(String.Format("SHOW CREATE SCHEMA `{0}`", item));
        }

        public string GetCreateTableOperator(string item)
        {
            return GetCreateOperator(String.Format("SHOW CREATE TABLE `{0}`", item));
        }

        private string[] GetItemsList(string SQL)
        {
            List<string> result = new List<string>();
            if (serverConnected)
            {
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    try
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(reader.GetString(0));
                            }
                            lasterrorString = String.Empty;
                        }
                    }
                    catch (MySqlException e)
                    {
                        lasterrorString = e.Message;
                    }
                }
            }
            return result.ToArray();
        }

        public void CreateThis(string SQL)
        {
            ExecSQL(SQL);
        }

        public void InsertThis(string SQL, string[] pars, object[] vals)
        {
            if (serverConnected)
            {
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    for (int i = 0; i < pars.Length; i++)
                        command.Parameters.AddWithValue(pars[i], vals[i]);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DropTable(string table)
        {
            ExecSQL("DROP TABLE IF EXISTS `" + table + "`");
        }

        public void DropSchema(string schema)
        {
            ExecSQL("DROP DATABASE IF EXISTS `" + schema + "`");
        }

        public void UseSchema(string schema)
        {
            ExecSQL("USE `" + schema + "`");
        }

        private void ExecSQL(string SQL)
        {
            if (serverConnected)
            {
                using (MySqlCommand command = new MySqlCommand(SQL, myConnection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                        lasterrorString = String.Empty;
                    }
                    catch (Exception e)
                    {
                        lasterrorString = e.Message;
                        throw;
                    }
                }
            }
        }

        public string[] GetTables()
        {
            return GetItemsList("SHOW TABLES");
        }

        public string[] GetDatabases()
        {
            return GetItemsList("SHOW SCHEMAS");
        }

        public bool Connected
        {
            get
            {
                return serverConnected;
            }
        }

        private bool tryToConnect()
        {
            try
            {
                // произведем попытку подключения
                myConnection.Open();
                lasterrorString = String.Empty;
                return true;
            }
            catch (Exception e)
            {
                lasterrorString = e.Message;
                return false;
            }
        }

        public static string LastError { get { return lasterrorString; } }

        public void Dispose()
        {
            myConnection.Dispose();
        }
    }

}
