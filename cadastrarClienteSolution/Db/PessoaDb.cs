using cadastrarClienteSolution.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cadastrarClienteSolution.Db
{
    public class PessoaDb
    {
        public List<Pessoa> listarCadastrados()
        {
            String sqltext = "SELECT id, nome, dtNascimento, sexo, cep, endereco, numeroEndereco, complemento, bairro, cidade FROM Pessoa";
            MySqlConnection conn = new MySqlConnection(ConnectionDb.Conexao);
            MySqlCommand sqlComand = new MySqlCommand(sqltext, conn);
            
            List<Pessoa> pessoasCadastradas = new List<Pessoa>();
            conn.Open();
            MySqlDataReader reader = sqlComand.ExecuteReader();

            while (reader.Read())
            {
                Pessoa pessoaCadastro = new Pessoa();
                pessoaCadastro.Id = Convert.ToInt32(reader["id"]);
                pessoaCadastro.Nome = Convert.ToString(reader["nome"]);
                pessoaCadastro.DtNascimento = Convert.ToString(reader["dtNascimento"]);
                pessoaCadastro.Sexo = Convert.ToString(reader["sexo"]);
                pessoaCadastro.Cep = Convert.ToString(reader["cep"]);
                pessoaCadastro.Endereco = Convert.ToString(reader["endereco"]);
                pessoaCadastro.NumeroEndereco = Convert.ToString(reader["numeroEndereco"]);
                pessoaCadastro.Complemento = Convert.ToString(reader["complemento"]);
                pessoaCadastro.Bairro = Convert.ToString(reader["bairro"]);
                pessoaCadastro.Cidade = Convert.ToString(reader["cidade"]);

                pessoasCadastradas.Add(pessoaCadastro);
            }

            return pessoasCadastradas;
        }

        public int Incluir(Pessoa pessoa)
        {
            ValidarPessoa(pessoa);
            string sqlText = "INSERT INTO Pessoa (nome, dtNascimento, sexo, cep, endereco, numeroEndereco, complemento, bairro, cidade) VALUES (@Nome, @DtNascimento, @Sexo, @cep, @Endereco, @NumeroEndereco, @Complemento, @Bairro, @Cidade); SELECT LAST_INSERT_ID()";

            MySqlConnection conn = new MySqlConnection(ConnectionDb.Conexao);
            MySqlCommand command = new MySqlCommand(sqlText, conn);
            int id = 0;

            command.Parameters.AddWithValue("@Nome", pessoa.Nome);
            command.Parameters.AddWithValue("@DtNascimento", pessoa.DtNascimento);
            command.Parameters.AddWithValue("@Sexo", pessoa.Sexo);
            command.Parameters.AddWithValue("@Endereco", pessoa.Endereco);
            command.Parameters.AddWithValue("@NumeroEndereco", pessoa.NumeroEndereco);
            command.Parameters.AddWithValue("@Complemento", pessoa.Complemento);
            command.Parameters.AddWithValue("@Bairro", pessoa.Bairro);
            command.Parameters.AddWithValue("@Cidade", pessoa.Cidade);
            command.Parameters.AddWithValue("@cep", pessoa.Cep);
            conn.Open();

            id = Convert.ToInt32(command.ExecuteScalar());

            return id;
        }

        public int AlterarPessoa(Pessoa pessoa)
        {
            string sqlText = "UPDATE Pessoa SET nome=@Nome, dtNascimento=@DtNascimento, sexo=@Sexo, cep=@cep, endereco=@Endereco, NumeroEndereco=@NumeroEndereco, Complemento=@Complemento, Bairro=@Bairro, Cidade=@Cidade where id=@id";
            MySqlConnection conn = new MySqlConnection(ConnectionDb.Conexao);
            MySqlCommand command = new MySqlCommand(sqlText, conn);
            int totalAlterado = 0;
            command.Parameters.AddWithValue("@Nome", pessoa.Nome);
            command.Parameters.AddWithValue("@DtNascimento", pessoa.DtNascimento);
            command.Parameters.AddWithValue("@Sexo", pessoa.Sexo);
            command.Parameters.AddWithValue("@Endereco", pessoa.Endereco);
            command.Parameters.AddWithValue("@NumeroEndereco", pessoa.NumeroEndereco);
            command.Parameters.AddWithValue("@Complemento", pessoa.Complemento);
            command.Parameters.AddWithValue("@Bairro", pessoa.Bairro);
            command.Parameters.AddWithValue("@Cidade", pessoa.Cidade);
            command.Parameters.AddWithValue("@cep", pessoa.Cep);
            command.Parameters.AddWithValue("@id", pessoa.Id);

            conn.Open();
            totalAlterado = Convert.ToInt32(command.ExecuteScalar());

            return totalAlterado;
        }

        public Pessoa ObterPessoa(int id)
        {
            string sqlText = "select * from Pessoa where id=@ID";
            MySqlConnection conn = new MySqlConnection(ConnectionDb.Conexao);
            MySqlCommand command = new MySqlCommand(sqlText, conn);
            Pessoa pessoaCadastro = null;
            command.Parameters.AddWithValue("@ID", id);
            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                pessoaCadastro = new Pessoa();
                pessoaCadastro.Id = Convert.ToInt32(reader["id"]);
                pessoaCadastro.Nome = Convert.ToString(reader["nome"]);
                pessoaCadastro.DtNascimento = Convert.ToString(reader["dtNascimento"]);
                pessoaCadastro.Sexo = Convert.ToString(reader["sexo"]);
                pessoaCadastro.Cep = Convert.ToString(reader["cep"]);
                pessoaCadastro.Endereco = Convert.ToString(reader["endereco"]);
                pessoaCadastro.NumeroEndereco = Convert.ToString(reader["numeroEndereco"]);
                pessoaCadastro.Complemento = Convert.ToString(reader["complemento"]);
                pessoaCadastro.Bairro = Convert.ToString(reader["bairro"]);
                pessoaCadastro.Cidade = Convert.ToString(reader["cidade"]);
            }
            return pessoaCadastro;
        }

        public int ExcluirPessoa(int id)
        {
            string sqlText = "DELETE FROM Pessoa WHERE id= @ID";
            int total = 0;
            MySqlConnection conn = new MySqlConnection(ConnectionDb.Conexao);
            MySqlCommand command = new MySqlCommand(sqlText, conn);
            command.Parameters.AddWithValue("@ID", id);
            conn.Open();
            total = Convert.ToInt32(command.ExecuteNonQuery());
            return total;
        }

        public void ValidarPessoa(Pessoa pessoa)
        {
            if (string.IsNullOrEmpty(pessoa.Nome))
            {
                throw new Exception("Campo nome é obrigatório!");
            }

            if (string.IsNullOrEmpty(pessoa.Sexo))
            {
                throw new Exception("Campo Sexo é obrigatório!");
            }

            if (pessoa.DtNascimento == null)
            {
                throw new Exception("Campo data de nascimento é obrigatório!");
            }
        }
    }
}