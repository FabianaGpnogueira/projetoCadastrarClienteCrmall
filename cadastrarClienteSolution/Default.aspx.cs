using cadastrarClienteSolution.Db;
using cadastrarClienteSolution.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cadastrarClienteSolution
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarListaCadastrados();
            }
        }

        protected void gravarPessoa_Click(object sender, EventArgs e)
        {
            try
            {
                MensagemLimpar();
                Pessoa pessoaCadastro = new Pessoa();
                PessoaDb pessoaDb = new PessoaDb();
                if (validarDadosInclusaoPessoa())
                {
                    pessoaCadastro = obterDadosPessoa();

                    if (pessoaCadastro.Id == 0)
                    {
                        pessoaCadastro.Id = pessoaDb.Incluir(pessoaCadastro);
                        CarregarListaCadastrados();
                    }
                    else
                    {
                        pessoaDb.AlterarPessoa(pessoaCadastro);
                        CarregarListaCadastrados();
                    }
                }
            }
            catch (Exception ex)
            {
                AlterarMensagem(ex.Message);
            }
        }

        protected Boolean validarDadosInclusaoPessoa()
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                AlterarMensagem("Campo Nome é obrigatório.");
                return false;
            }
            else if (string.IsNullOrEmpty(dtNascimentoDatePicker.Text))
            {
                AlterarMensagem("Campo Data de Nascimento é obrigatório.");
                return false;
            }
            else if(string.IsNullOrEmpty(sexoDropDown.SelectedValue))
            {
                AlterarMensagem("Campo Sexo é obrigatório.");
                return false;
            }
            else if (validarData())
            {
                AlterarMensagem("Formato de data inválido. Favor utilizar o formato dd/mm/aaaa");
                return false;
            }

            else
            {
                return true;
            }
        }

        protected void novoCadastroBtn_Click(object sender, EventArgs e)
        {
            LimparFormulario();
            multiView.ActiveViewIndex = 1;
        }

        protected void CarregarListaCadastrados()
        {
            PessoaDb pessoa = new PessoaDb();
            multiView.ActiveViewIndex = 0;
            clientesGrid.DataSource = pessoa.listarCadastrados();
            clientesGrid.DataBind();
        }

        protected void MensagemLimpar()
        {
            msgLbl.Visible = false;
        }

        protected void AlterarMensagem(string mensagem)
        {
            msgLbl.Visible = true;
            msgLbl.Text = mensagem;
        }

        protected void LimparFormulario()
        {
            MensagemLimpar();
            nomeTextBox.Text = string.Empty;
            sexoDropDown.SelectedIndex = 0;
            enderecoTextBox.Text = string.Empty;
            complementoTextBox.Text = string.Empty;
            numeroTextBox.Text = string.Empty;
            bairroTextBox.Text = string.Empty;
            cidadeTextBox.Text = string.Empty;
            cepTextBox.Text = string.Empty;
            dtNascimentoDatePicker.Text = string.Empty;
    
            ViewState["Id"] = 0;
        }

        protected void LimparEndereco()
        {
            this.enderecoTextBox.Text = string.Empty;
            this.complementoTextBox.Text = string.Empty;
            this.numeroTextBox.Text = string.Empty;
            this.bairroTextBox.Text = string.Empty;
            this.cidadeTextBox.Text = string.Empty;
            this.cepTextBox.Text = string.Empty;
        }

        protected void prepararFormulario()
        {
            LimparFormulario();
            sexoDropDown.SelectedIndex = 0;
            nomeTextBox.Focus();
        }

        protected void NovoCadastroClick()
        {
            multiView.ActiveViewIndex = 1;
            prepararFormulario();
        }

        protected Boolean validarData()
        {
            string[] format = new string[] { "dd/MM/yyyy" };
            DateTime result;

            if (DateTime.TryParseExact(dtNascimentoDatePicker.Text, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
            {
                return true;
            }
            else if (DateTime.TryParseExact(dtNascimentoDatePicker.Text, "dd-MM-yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void validarCepPreenchimento(object sender, EventArgs e)
        {
            Pessoa pessoaCadastro = new Pessoa();
            JObject retorno = pessoaCadastro.consultaCep(cepTextBox.Text);
            if (!pessoaCadastro.validarConsultaCep(retorno))
            {
                AlterarMensagem("CEP inválido.");
                LimparEndereco();
            }
            else
                preencherCamposCep(retorno);
        }

        protected void preencherCamposCep(JObject json)
        {
            enderecoTextBox.Text = json["logradouro"].ToString();
            bairroTextBox.Text = json["bairro"].ToString();
            cidadeTextBox.Text = json["localidade"].ToString();

            enderecoTextBox.Enabled = true;
            bairroTextBox.Enabled = true;
            cidadeTextBox.Enabled = true;
        }

        protected Pessoa obterDadosPessoa()
        {
            Pessoa cadastradoPessoa = new Pessoa();
            cadastradoPessoa.Id = Convert.ToInt32(ViewState["Id"]);
            cadastradoPessoa.Nome = nomeTextBox.Text;
            cadastradoPessoa.Sexo = sexoDropDown.SelectedValue;
            cadastradoPessoa.DtNascimento = dtNascimentoDatePicker.Text;
            cadastradoPessoa.Cep = cepTextBox.Text;
            cadastradoPessoa.Endereco = enderecoTextBox.Text;
            cadastradoPessoa.NumeroEndereco = numeroTextBox.Text;
            cadastradoPessoa.Complemento = complementoTextBox.Text;
            cadastradoPessoa.Bairro = bairroTextBox.Text;
            cadastradoPessoa.Cidade = cidadeTextBox.Text;
            return cadastradoPessoa;
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            PessoaDb pessoaDb = new PessoaDb();
            Pessoa pessoa = new Pessoa();
            MensagemLimpar();
            var gv = (GridView)sender;
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int id = (int)gv.DataKeys[rowIndex].Value;
            pessoa = pessoaDb.ObterPessoa(id);

            if (pessoa != null)
            {
                ExibirPessoa(pessoa);
            }
        }

        protected void ExibirPessoa(Pessoa pessoa)
        {
            ViewState["Id"] = pessoa.Id;
            this.nomeTextBox.Text = pessoa.Nome;
            this.sexoDropDown.SelectedIndex = pessoa.Sexo == "Feminino" ? 1 : 2;
            this.enderecoTextBox.Text = pessoa.Endereco;
            this.complementoTextBox.Text = pessoa.Complemento;
            this.numeroTextBox.Text = pessoa.NumeroEndereco;
            this.bairroTextBox.Text = pessoa.Bairro;
            this.cidadeTextBox.Text = pessoa.Cidade;
            this.cepTextBox.Text = pessoa.Cep;
            this.dtNascimentoDatePicker.Text = Convert.ToString(pessoa.DtNascimento);

            multiView.ActiveViewIndex = 1;
            excluirCadastroBtn.Visible = true;
            nomeTextBox.Focus();
        }

        protected void ExcluirPessoa()
        {
            MensagemLimpar();
            Pessoa pessoaExcluir = new Pessoa();
            PessoaDb pessoaDb = new PessoaDb();
            pessoaExcluir = obterDadosPessoa();
            pessoaDb.ExcluirPessoa(pessoaExcluir.Id);
            CarregarListaCadastrados();
        }

        protected void excluirPessoa_Click(object sender, EventArgs e)
        {
            try
            {
                ExcluirPessoa();
            }
            catch (Exception ex)
            {
                AlterarMensagem(ex.Message);
            }
        }

        protected void voltar_Click(object sender, EventArgs e)
        {
            multiView.ActiveViewIndex = 0;
            LimparFormulario();
        }
    }
}