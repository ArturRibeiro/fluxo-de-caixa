namespace Financeiro.App.Services;

public class MovimentacaoApiService(HttpClient http)
{
    public async Task<bool> RegistrarMovimentacaoAsync(LancamentoInputModel model)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/movimentacao") { Content = JsonContent.Create(model) };
        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        var response = await http.SendAsync(request);
        return response.IsSuccessStatusCode;
    }
}