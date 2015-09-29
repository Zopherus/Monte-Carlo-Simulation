using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CreateTables
{
	class DBHelper
	{
		private MySqlConnection _connection;
		private string SERVER = "simulationdata.c6peoxnxyjsc.us-west-2.rds.amazonaws.com:3306";
		private string DB = "simulationdata";
		private string UID = "Zopherus";

		public DBHelper()
		{
			Initialize();
		}

		private void Initialize()
		{
			//string connectionString = "SERVER=" + SERVER + ";" + "DATABASE=" +
				//DB + ";" + "UID=" + UID + ";" + "PASSWORD=" + PASSWORD + ";";
			string connectionString = "Server=simulationdata.c6peoxnxyjsc.us-west-2.rds.amazonaws.com; Database=Data; Uid=Zopherus; Pwd=";
			_connection = new MySqlConnection(connectionString);

			try
			{
				_connection.Open();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		private bool OpenConnection()
		{
			if (_connection.State == System.Data.ConnectionState.Open)
				return true;
			try
			{
				_connection.Open();
				Console.WriteLine("CONNECTION: " + _connection.State);
				return true;
			}
			catch (MySqlException ex)
			{
				Console.WriteLine(ex.Message + ex.Number);
				//When handling errors, you can your application's response based 
				//on the error number.
				//The two most common error numbers when connecting are as follows:
				//0: Cannot connect to server.
				//1045: Invalid user name and/or password.
				switch (ex.Number)
				{
					case 0:
						Console.WriteLine("Cannot connect to server.  Contact administrator");
						break;

					case 1045:
						Console.WriteLine("Invalid username/password, please try again");
						break;
				}
				return false;
			}
		}

		private bool CloseConnection()
		{
			try
			{
				_connection.Close();
				return true;
			}
			catch (MySqlException ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		public bool Query(string query)
		{
			if (!this.OpenConnection())
				return false;

			MySqlCommand cmd = new MySqlCommand(query, _connection);

			cmd.ExecuteNonQuery();

			return true;
		}

		public Tuple<string, string>[] FetchQuery(string query, string[] fieldNames)
		{
			if (!this.OpenConnection())
				return null;

			MySqlCommand cmd = new MySqlCommand(query, _connection);

			MySqlDataReader dataReader = cmd.ExecuteReader();

			int i = 0;

			var results = new Tuple<string, string>[fieldNames.Length];
			while (dataReader.Read())
			{
				results[i] = new Tuple<string, string>(fieldNames[i], (string)dataReader[fieldNames[i]]);
				i++;
			}

			return results;
		}
	}
}
