using System;
using System.Collections.Generic;
using System.Data;
using Models;
using Oracle.ManagedDataAccess.Client;

namespace Repository
{
  public class PokemonRepository
  {
    private string _connectionString;

    public PokemonRepository()
    {
      _connectionString = "Pooling=false;User Id=system;Password=oracle;Data Source=localhost:1521/xe;";
    }

    public void Add(Pokemon pokemon)
    {
      string sql = $"insert into pokemon (id, name, experience, height, weight) values ({pokemon.Id}";
      sql = $"{sql}, '{pokemon.Name}', {pokemon.Experience}, {pokemon.Height}, {pokemon.Weight})";
      using (OracleConnection conn = new OracleConnection(_connectionString))
      {
        using (OracleCommand cmd = conn.CreateCommand())
        {
          try
          {
            conn.Open();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
          }
          catch (System.Exception ex)
          {

          }
        }
      }
    }

    public Pokemon Get(int id) => new Pokemon();

    public List<Pokemon> GetAll(Filter filter)
    {
      var list = new List<Pokemon>();
      using (OracleConnection conn = new OracleConnection(_connectionString))
      {
        using (OracleCommand cmd = conn.CreateCommand())
        {
          try
          {
            cmd.CommandText = "list_pokemon";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("p_name", filter.SearchText);
            cmd.Parameters.Add("p_order_field", filter.OrderBy.ToLower());
            cmd.Parameters.Add("p_order_type", filter.OrderDir);
            cmd.Parameters.Add("p_start", filter.Start);
            cmd.Parameters.Add("p_length", filter.Length);
            conn.Open();
            var reader = cmd.ExecuteNonQuery();
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
              var p = new Pokemon();
              p.Id = Convert.ToInt32(row["id"]);
              p.Name = row["name"].ToString();
              p.Experience = Convert.ToInt32(row["experience"]);
              p.Height = Convert.ToInt32(row["height"]);
              p.Weight = Convert.ToInt32(row["weight"]);
              p.Total = Convert.ToInt32(row["total_lines"]);
              p.Filtered = Convert.ToInt32(row["filtered"]);
              list.Add(p);
            }

            // while (reader.Read())
            // {
            //   var p = new Pokemon();
            //   p.Id = Convert.ToInt32(reader["id"]);
            //   p.Name = reader["name"].ToString();
            //   p.Experience = Convert.ToInt32(reader["experience"]);
            //   p.Height = Convert.ToInt32(reader["height"]);
            //   p.Weight = Convert.ToInt32(reader["weight"]);
            //   list.Add(p);
            // }
            conn.Close();
          }
          catch (System.Exception ex)
          {

          }
        }
      }
      return list;
    }
  }
}