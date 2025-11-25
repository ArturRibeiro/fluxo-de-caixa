Feature: Lançar Movimentação Financeira
  Como um comerciante
  Quero registrar lançamentos de crédito e débito
  Para controlar meu fluxo de caixa diário

#  Scenario: Registrar uma movimentação de crédito com sucesso
#    Given que exista uma requisição válida para lançamento de movimentação financeira
#    And o tipo informado é "Credito"
#    And o valor informado é 150.75
#    And a data informada é a data atual
#    And a descrição informada é "Venda de produto"
#    When eu enviar a requisição para o endpoint POST /api/movimentacoes
#    Then o sistema deve retornar o status 201
#    And deve retornar um identificador único da movimentação


  Scenario: Registrar uma movimentação de crédito com sucesso
    Given um lançamento de movimentação financeira
    And informo o tipo de movimentação "Crédito"
    And informo o valor "150.75"
    And informo a descricao "Valor recebido do produto vendido"
    Then o sistema deve registrar a entrada corretamente