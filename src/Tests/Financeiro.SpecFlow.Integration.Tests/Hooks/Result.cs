namespace Financeiro.SpecFlow.Integration.Tests.Hooks;

public record Result(bool IsSuccess, HttpStatusCode StatusCode, List<string>? Errors = null);