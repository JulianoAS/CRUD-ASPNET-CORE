using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace asnet_core.Models
{
    public class FuncionarioDAL : IFuncionarioDAL
    {
        

        private IConfiguration Configuration { get; }

        public FuncionarioDAL(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IEnumerable<Funcionario> GetAllFuncionarios()
        {
            List<Funcionario> lstFuncionario = new List<Funcionario>();

            using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("Con")))
            {
                SqlCommand cmd = new SqlCommand("SELECT FuncionarioID, Nome, Cidade, Departamento, Sexo FROM Funcionarios", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Funcionario funcionario = new Funcionario();

                    funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioID"]);
                    funcionario.Nome = rdr["Nome"].ToString();
                    funcionario.Cidade = rdr["Cidade"].ToString();
                    funcionario.Departamento = rdr["Departamento"].ToString();
                    funcionario.Sexo = rdr["Sexo"].ToString();

                    lstFuncionario.Add(funcionario);
                }

                con.Close();

            }

            return lstFuncionario;

        }

        public void AddFuncionario(Funcionario funcionario)
        {
            using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("Con")))
            {
                string comandoSQL = "Insert into Funcionarios (Nome,Cidade,Departamento,Sexo)Values(@Nome, @Cidade, @Departamento, @Sexo)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                cmd.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
                cmd.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateFuncionario(Funcionario funcionario)
        {
            using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("Con")))
            {
                string comandoSQL = "Update Funcionarios set Nome = @Nome, Cidade = @Cidade, Departamento = @Departamento, Sexo = @Sexo where FuncionarioId = @FuncionarioId";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FuncionarioId", funcionario.FuncionarioId);
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                cmd.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
                cmd.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Funcionario GetFuncionario(int? id)
        {
            Funcionario funcionario = new Funcionario();
            using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("Con")))
            {
                string sqlQuery = "SELECT * FROM Funcionarios WHERE FuncionarioId= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                    funcionario.Nome = rdr["Nome"].ToString();
                    funcionario.Cidade = rdr["Cidade"].ToString();
                    funcionario.Departamento = rdr["Departamento"].ToString();
                    funcionario.Sexo = rdr["Sexo"].ToString();
                }
            }
            return funcionario;
        }
        public void DeleteFuncionario(int? id)
        {
            using (SqlConnection con = new SqlConnection(Configuration.GetConnectionString("Con")))
            {
                string comandoSQL = "Delete from Funcionarios where FuncionarioId = @FuncionarioId";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FuncionarioId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

       
    }
}
