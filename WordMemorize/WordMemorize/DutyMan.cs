using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace Denemelerfalan
{
	class DutyMan
	{
		private SqlConnection con;
		private SqlCommand cmd;
		private SqlDataReader dr;

		private void Missionary()
		{
			con.Open();
			cmd.ExecuteNonQuery();
			cmd.Parameters.Clear();
			con.Close();
		}



		public DutyMan()
		{
			Connect();
		}

		public void Connect()
		{
			con = new SqlConnection
				(@"Data Source=DESKTOP-KA1TM07\SQLEXPRESS;Initial Catalog=WordMemorize;Integrated Security=True");
			cmd = new SqlCommand();
			cmd.Connection = con;
		}



		public DataTable Fill()
		{

			try
			{

				cmd.CommandText = "Select * FROM dataWords";
				cmd.CommandType = CommandType.Text;
				con.Open();
				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);
				con.Close();

				return dataTable;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (con != null)
				{
					con.Close();
				}
			}

		}

		public void Add(string englishW, string turkishW, DateTime date)
		{
			try
			{
				cmd.CommandText = "INSERT INTO dataWords(englishWord, turkishWord, seviye, tarih) values(@dbenglishWord,@dbturkishWord, 0, @dbtarih)";
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@dbenglishWord", englishW);
				cmd.Parameters.AddWithValue("@dbturkishWord", turkishW);
				cmd.Parameters.AddWithValue("@dbtarih", date);

				Missionary();
			}
			catch
			{
				throw;
			}
			finally
			{
				if (con != null)
				{
					con.Close();
				}
			}

		}

		public void Update(int id, string englishW, string turkishW, int seviye, DateTime date)
		{
			try
			{
				cmd.CommandText = "UPDATE dataWords set englishWord = @dbenglishW, turkishWord = @dbturkishW, seviye = @dbseviye, tarih = @dbtarihK WHERE id = @dbid";
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@dbid", id);
				cmd.Parameters.AddWithValue("@dbenglishW", englishW);
				cmd.Parameters.AddWithValue("@dbturkishW", turkishW);
				cmd.Parameters.AddWithValue("@dbseviye", seviye);
				cmd.Parameters.AddWithValue("@dbtarihK", date);

				Missionary();

			}
			catch
			{
				throw;
			}
			finally
			{
				if (con != null)
				{
					con.Close();
				}
			}

		}
		public void Delete(int id)
		{
			try
			{
				cmd.CommandText = "DELETE FROM dataWords  WHERE id = @dbid";
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@dbid", id);

				Missionary();

			}
			catch
			{
				throw;
			}
			finally
			{
				if (con != null)
				{
					con.Close();
				}
			}

		}
		public List<Sorular> TakeQuestions()
		{
			List<Sorular> sorular = new List<Sorular>();

			DateTime kalamar = DateTime.Today.AddDays(-1);
			DateTime kalamar1 = DateTime.Today.AddDays(-6);
			DateTime kalamar2 = DateTime.Today.AddMonths(-1);
			DateTime kalamar3 = DateTime.Today.AddMonths(-6);
			DateTime kalamar4 = DateTime.Today.AddYears(-1);

			cmd.CommandText = "SELECT * FROM dataWords WHERE (seviye = 0 and tarih <= @kalamars) or (seviye = 1 and tarih <= @kalamar1s) or (seviye = 2 and tarih <= @kalamar2s) or (seviye = 3 and tarih <= @kalamar3s) or (seviye = 4 and tarih <= @kalamar4s) ";
			cmd.CommandType = CommandType.Text;

			cmd.Parameters.AddWithValue("@kalamars", kalamar);
			cmd.Parameters.AddWithValue("@kalamar1s", kalamar1);
			cmd.Parameters.AddWithValue("@kalamar2s", kalamar2);
			cmd.Parameters.AddWithValue("@kalamar3s", kalamar3);
			cmd.Parameters.AddWithValue("@kalamar4s", kalamar4);


			con.Open();

			dr = cmd.ExecuteReader();

			while (dr.Read())
			{
				Sorular soru = new Sorular();
				soru.id = (int)dr["id"];
				soru.englishWord = (string)dr["englishWord"];
				soru.turkishWorld = (string)dr["turkishWord"];
				soru.seviye = (int)dr["seviye"];

				sorular.Add(soru);
			}

			dr.Close();
			con.Close();
			cmd.Parameters.Clear();

			return sorular;
		}
		public List<string> RndAnswers()
		{
			List<string> rndAnswers = new List<string>();

			cmd.CommandText = "SELECT turkishWord FROM dataWords";
			cmd.CommandType = CommandType.Text;

			con.Open();
			dr = cmd.ExecuteReader();

			while (dr.Read())
			{

				rndAnswers.Add((string)dr["turkishWord"]);

			}

			dr.Close();
			con.Close();

			return rndAnswers;
		}
		public DataTable FillLearned()
		{
			try
			{

				cmd.CommandText = "Select * FROM dataWords WHERE seviye = 5";
				cmd.CommandType = CommandType.Text;
				con.Open();
				SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
				DataTable dataTable = new DataTable();
				dataAdapter.Fill(dataTable);
				con.Close();

				return dataTable;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (con != null)
				{
					con.Close();
				}
			}
		}

		public int StatisticYear(DateTime tarih1, DateTime tarih2)
		{
			int count;

			cmd.CommandText = "Select count(*) FROM dataWords WHERE seviye = 5 AND (tarih BETWEEN @dbtarih1 AND @dbtarih2)";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@dbtarih1", tarih1);
			cmd.Parameters.AddWithValue("@dbtarih2", tarih2);

			con.Open();
			count = (int)cmd.ExecuteScalar();

			con.Close();
			cmd.Parameters.Clear();

			tarih1.AddMonths(1);
			tarih2.AddMonths(1);


			return count;
		}
	}
}
