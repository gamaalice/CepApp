using Microsoft.Data.Sqlite;
using CepApp.Models;

namespace CepApp.Services;

public class DatabaseService
{
    string connectionString = "Data Source=CepDb.db";

    public void CriarTabela()
    {
        using var conn = new SqliteConnection(connectionString);
        conn.Open();

        var sql = @"
        CREATE TABLE IF NOT EXISTS Enderecos (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Cep TEXT,
            Rua TEXT,
            Cidade TEXT,
            Uf TEXT
        );";

        using var cmd = new SqliteCommand(sql, conn);
        cmd.ExecuteNonQuery();
    }

    public void Salvar(CepModel cep)
    {
        using var conn = new SqliteConnection(connectionString);
        conn.Open();

        var sql = @"
        INSERT INTO Enderecos
        (Cep, Rua, Cidade, Uf)
        VALUES
        (@cep, @rua, @cidade, @uf)";

        using var cmd = new SqliteCommand(sql, conn);

        cmd.Parameters.AddWithValue("@cep", cep.cep ?? "");
        cmd.Parameters.AddWithValue("@rua", cep.logradouro ?? "");
        cmd.Parameters.AddWithValue("@cidade", cep.localidade ?? "");
        cmd.Parameters.AddWithValue("@uf", cep.uf ?? "");

        cmd.ExecuteNonQuery();
    }

    public List<string> Listar()
    {
        var lista = new List<string>();

        using var conn = new SqliteConnection(connectionString);
        conn.Open();

        var sql = "SELECT Cep, Cidade, Uf FROM Enderecos";

        using var cmd = new SqliteCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var item =
                $"{reader["Cep"]} - {reader["Cidade"]}/{reader["Uf"]}";

            lista.Add(item);
        }

        return lista;
    }
}