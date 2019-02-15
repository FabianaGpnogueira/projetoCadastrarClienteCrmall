using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace cadastrarClienteSolution.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DtNascimento { get; set; }
        public string Sexo { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string NumeroEndereco { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }

        public JObject consultaCep(string cep)
        {
            HttpWebRequest requisicao = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + cep + "/json/");
            requisicao.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            string results = string.Empty;

            try
            {
                using (HttpWebResponse resposta = requisicao.GetResponse() as HttpWebResponse)
                using (Stream stream = resposta.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    results = reader.ReadToEnd();
                }

                JObject joResponse = JObject.Parse(results);
                return joResponse;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Boolean validarConsultaCep(JObject json)
        {
            if (json.ToString().Contains("erro"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}