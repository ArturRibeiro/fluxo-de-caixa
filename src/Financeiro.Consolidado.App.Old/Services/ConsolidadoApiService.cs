using Financeiro.Consolidado.App.Models;

namespace Financeiro.Consolidado.App.Services;

public class ConsolidadoApiService(HttpClient http)
{
    private JsonSerializerOptions options = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };
    public async Task<ConsolidadoModel?> GetConsolidado(DateOnly data)
    {
        var url = $"/api/consolidado?data={data:yyyy-MM-dd}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        var response = await http.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        // return response.IsSuccessStatusCode 
        //     ? JsonSerializer.Deserialize<ConsolidadoModel>(json, options) 
        //     : new ConsolidadoModel();
        return (json == null || json == string.Empty) ? new ConsolidadoModel() : JsonSerializer.Deserialize<ConsolidadoModel>(json, options);
    }
}