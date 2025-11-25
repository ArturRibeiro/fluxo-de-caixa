using Financeiro.Consolidado.App.Models;
using Financeiro.Lancamento.Consolidado.App.Services;
using Microsoft.AspNetCore.Components;

namespace Financeiro.Lancamento.Consolidado.App.Components.Pages;

public partial class Home
{
    [Inject] public ConsolidadoApiService Service { get; set; } = default!;

    private ConsolidadoModel? Consolidado = new();

    [Parameter] 
    public DateOnly DataSelecionada { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    private List<DateOnly> DatasRetroativas { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        int dias = 30;
        var hoje = DateOnly.FromDateTime(DateTime.Now);

        for (int i = 0; i <= dias; i++)
            DatasRetroativas.Add(hoje.AddDays(-i));

        await GetConsolidados();
    }

    private async Task GetConsolidados()
    {
        Consolidado = await Service.GetConsolidado(DataSelecionada);
        StateHasChanged();
    }

    private async Task OnDataChange(DateOnly data)
    {
        DataSelecionada = data;

        await GetConsolidados();
    }
    
    
    private string SelectedValue { get; set; } = "";

    private void HandleValueChanged(string newValue)
    {
        SelectedValue = newValue;
        // You can add additional logic here to react to the change
        Console.WriteLine($"New value selected: {newValue}");
    }
}