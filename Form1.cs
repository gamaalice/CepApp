using CepApp.Services;
using CepApp.Models;

namespace CepApp;

public partial class Form1 : Form
{
    TextBox txtCep = null!;
    Button btnBuscar = null!;
    Button btnSalvar = null!;

    Label lblRua = null!;
    Label lblCidade = null!;
    Label lblUf = null!;

    ListBox lista = null!;

    CepModel? ultimoCep;

    public Form1()
    {
        InitializeComponent();
        CriarInterface();

        var db = new DatabaseService();
        db.CriarTabela();

        CarregarLista();
    }

    private void CriarInterface()
    {
        txtCep = new TextBox();
        txtCep.Top = 20;
        txtCep.Left = 20;
        txtCep.Width = 150;

        btnBuscar = new Button();
        btnBuscar.Text = "Buscar";
        btnBuscar.Top = 18;
        btnBuscar.Left = 180;
        btnBuscar.Width = 80;
        btnBuscar.Height = 30;
        btnBuscar.Click += BuscarCep;

        lblRua = new Label();
        lblRua.Top = 70;
        lblRua.Left = 20;
        lblRua.Width = 400;
        lblRua.Text = "Rua:";

        lblCidade = new Label();
        lblCidade.Top = 100;
        lblCidade.Left = 20;
        lblCidade.Width = 400;
        lblCidade.Text = "Cidade:";

        lblUf = new Label();
        lblUf.Top = 130;
        lblUf.Left = 20;
        lblUf.Width = 400;
        lblUf.Text = "UF:";

        btnSalvar = new Button();
        btnSalvar.Text = "Salvar";
        btnSalvar.Top = 170;
        btnSalvar.Left = 20;
        btnSalvar.Width = 80;
        btnSalvar.Height = 30;
        btnSalvar.Click += Salvar;

        lista = new ListBox();
        lista.Top = 220;
        lista.Left = 20;
        lista.Width = 300;
        lista.Height = 150;

        this.Controls.Add(txtCep);
        this.Controls.Add(btnBuscar);
        this.Controls.Add(lblRua);
        this.Controls.Add(lblCidade);
        this.Controls.Add(lblUf);
        this.Controls.Add(btnSalvar);
        this.Controls.Add(lista);
    }

    private async void BuscarCep(object? sender, EventArgs e)
    {
        var service = new ViaCepService();
        var dados = await service.BuscarCep(txtCep.Text);

        if (dados != null)
        {
            ultimoCep = dados;

            lblRua.Text = $"Rua: {dados.logradouro}";
            lblCidade.Text = $"Cidade: {dados.localidade}";
            lblUf.Text = $"UF: {dados.uf}";
        }
    }

    private void Salvar(object? sender, EventArgs e)
    {
        if (ultimoCep == null) return;

        var db = new DatabaseService();
        db.Salvar(ultimoCep);

        CarregarLista();

        MessageBox.Show("Salvo no SQLite!");
    }

    private void CarregarLista()
    {
        var db = new DatabaseService();

        lista.Items.Clear();

        var itens = db.Listar();

        foreach (var item in itens)
        {
            lista.Items.Add(item);
        }
    }
}