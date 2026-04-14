using System.Text.Json;
using CepApp.Models;

namespace CepApp.Services;

public class ViaCepService
{
    public async Task<CepModel?> BuscarCep(string cep)
    {
        using var client = new HttpClient();

        var response = await client.GetStringAsync(
            $"https://viacep.com.br/ws/{cep}/json/"
        );

        return JsonSerializer.Deserialize<CepModel>(response);
    }
}