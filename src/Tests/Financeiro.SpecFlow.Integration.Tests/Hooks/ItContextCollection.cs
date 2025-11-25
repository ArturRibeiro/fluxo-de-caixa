namespace Financeiro.SpecFlow.Integration.Tests.Hooks;

// TODO : Compartilha um mesmo contexto de teste (CustomWebApplicationFactory) entre vários testes.
[CollectionDefinition("CustomWebApplicationFactory")]
public class ItContextCollection : ICollectionFixture<LancamentosWebApplicationFactory> { }