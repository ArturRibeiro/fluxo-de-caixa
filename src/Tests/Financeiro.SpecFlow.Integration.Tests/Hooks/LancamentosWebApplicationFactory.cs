namespace Financeiro.SpecFlow.Integration.Tests.Hooks;

public class LancamentosWebApplicationFactory : WebApplicationFactory<Web.Api.Program>, IAsyncLifetime
{
    private JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    
     public ApplicationDbContext ApplicationDbContext =>
         this.Services
             .CreateScope()
             .ServiceProvider
             .GetRequiredService<ApplicationDbContext>();
     
     protected override void ConfigureWebHost(IWebHostBuilder builder)
     {
         builder.ConfigureAppConfiguration((context, configBuilder) =>
         {
             var currentDirectory = Directory.GetCurrentDirectory();
             configBuilder.SetBasePath(currentDirectory)
                 .AddJsonFile("appsettings.test.json");
                         
             var configurationRoot = configBuilder.Build();
             
              configBuilder.AddInMemoryCollection(new Dictionary<string, string>
              {
                  { "ConnectionStrings:DefaultConnection", configurationRoot.GetConnectionString("DefaultConnection") },
                  { "Environment", "Test" }
              }!);
         });
     }
    public async Task InitializeAsync() { }

    public new async Task DisposeAsync() { }
     
    public async Task<Result> SendAsync<T>(object obj, string uri)
    {
        var client = this.CreateClient();
        client.BaseAddress = new Uri("http://localhost:5279/api/");
        var response = await client.SendAsync(CreeateHttpRequestMessage(JsonSerializer.Serialize(obj), uri));
        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        return new Result(response.IsSuccessStatusCode, response.StatusCode);
    }

    private static Func<string, string, HttpRequestMessage> CreeateHttpRequestMessage = (json, uri) =>
    {
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = content;
        return request;
    };
}