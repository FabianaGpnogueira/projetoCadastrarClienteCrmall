<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="cadastrarClienteSolution._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="jumbotron">
        <h1>Cadastro de Pessoas</h1>
        <asp:Label runat="server" ID="msgLbl" Visible="false"></asp:Label>
        <asp:MultiView ID="multiView" runat="server" ActiveViewIndex="0">
            <asp:View ID="listaClienteView" runat="server">
                <div class=”table-responsive”>
                    <asp:GridView ID="clientesGrid" CssClass="table table-striped" DataKeyNames ="Id" AutoGenerateColumns="False" runat="server" OnRowCommand="GridView_RowCommand"> 
                        <Columns>
                        <asp:ButtonField DataTextField="nome" CommandName="alterarPessoa" HeaderText="Nome" />
                        <asp:BoundField DataField="sexo" HeaderText="Sexo" />
                        <asp:BoundField DataField="dtNascimento" HeaderText="Data Nascimento" />
                        <asp:BoundField DataField="cep" HeaderText="CEP" />
                        <asp:BoundField DataField="endereco" HeaderText="Endereço" />
                        <asp:BoundField DataField="numeroEndereco" HeaderText="Número" />
                        <asp:BoundField DataField="complemento" HeaderText="Complemento" />
                        <asp:BoundField DataField="bairro" HeaderText="Bairro" />
                        <asp:BoundField DataField="cidade" HeaderText="Cidade" />
                        </Columns>
                    </asp:GridView>
                    </div>
                <asp:LinkButton ID="novoCadastroBtn" OnClick="novoCadastroBtn_Click" runat="server">Novo Cadastro</asp:LinkButton>
            </asp:View>

            <asp:View ID="formClienteView" runat="server">
                <h3>Formulario de cadastro</h3>
                <br />
                <asp:Label runat="server">Nome:</asp:Label>
                <asp:TextBox runat="server" id="nomeTextBox" style="margin-bottom:15px; margin-right:10px"></asp:TextBox>

                <asp:Label runat="server">Data de Nascimento:</asp:Label>
                <asp:TextBox id="dtNascimentoDatePicker" placeholder="dd/mm/aaaa" runat="server"></asp:TextBox>
                <asp:Label runat="server" style="margin-left:10px">Sexo:</asp:Label>
                <asp:DropDownList runat="server" ID="sexoDropDown" style="margin-bottom:15px; margin-right:10px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Feminino</asp:ListItem>
                    <asp:ListItem>Masculino</asp:ListItem>
                </asp:DropDownList>
                <asp:Label runat="server">CEP:</asp:Label>
                <asp:TextBox runat="server" id="cepTextBox" style="margin-bottom:15px; margin-right:10px"></asp:TextBox>
                <asp:LinkButton runat="server" OnClick="validarCepPreenchimento">Preencher</asp:LinkButton>
                <br />
                <asp:Label runat="server" Enabled="false">Endereço:</asp:Label>
                <asp:TextBox runat="server" id="enderecoTextBox" style="margin-bottom:15px; margin-right:10px"></asp:TextBox>

                <asp:Label runat="server" Enabled="false">Número:</asp:Label>
                <asp:TextBox runat="server" id="numeroTextBox" style="margin-bottom:15px; margin-right:10px"></asp:TextBox>

                <asp:Label runat="server" Enabled="false">Complemento:</asp:Label>
                <asp:TextBox runat="server" id="complementoTextBox" style="margin-bottom:15px; margin-right:10px"></asp:TextBox>
                <br />
                <asp:Label runat="server" Enabled="false">Bairro:</asp:Label>
                <asp:TextBox runat="server" id="bairroTextBox" style="margin-bottom:15px; margin-right:10px"></asp:TextBox>

                <asp:Label runat="server" Enabled="false">Cidade:</asp:Label>
                <asp:TextBox runat="server" id="cidadeTextBox" style="margin-bottom:15px; margin-right:10px"></asp:TextBox>
                <br />
                <asp:LinkButton OnClick="gravarPessoa_Click" ID="salvarCadastroBtn" style="margin-right:20px" runat="server">Salvar</asp:LinkButton>
                <asp:LinkButton OnClick="excluirPessoa_Click" runat="server" ID="excluirCadastroBtn" style="margin-right:20px" Visible="false">Excluir</asp:LinkButton>
                <asp:LinkButton OnClick="voltar_Click" runat="server" style="margin-right:20px" ID="voltarCadastroBtn">Voltar</asp:LinkButton>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
