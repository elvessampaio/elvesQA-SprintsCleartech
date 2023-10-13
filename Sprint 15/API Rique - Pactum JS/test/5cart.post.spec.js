const { spec } = require('pactum');

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

it('Teste 1 - POST (Criando carrinho de  compras)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CarrinhoDeCompras')
    .withBody({
      "produtoId": "9755e521-07c9-4760-959d-ae40c0011aab",
      "quantidade": 2
    })
    .expectResponseTime(1000)
    .expectStatus(201)
    .expectBodyContains("R$ 31,00")

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 2 - POST (Adicionando endereço de entrega no carrinho)', async () => {
  let {responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CarrinhoDeCompras/bb27a223-f0b7-45fa-87c6-08e34de58818/enderecoDeEntrega')
    .withBody({
      "cep": "02133040",
      "numero": 383,
      "complemento": "Casa 1"
  })
    .expectResponseTime(1000)
    .expectStatus(200)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 3 - POST (Adicionando mais produtos ao carrinho)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CarrinhoDeCompras/bb27a223-f0b7-45fa-87c6-08e34de58818')
    .withBody({
      "produtoId": "6babc370-d0f0-4814-87fe-a80d5ed259a7",
      "quantidade": 3
    })
    .expectResponseTime(1000)
    .expectStatus(200)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 4 - POST (Produto já adicionado ao carrinho)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CarrinhoDeCompras/bb27a223-f0b7-45fa-87c6-08e34de58818')
    .withBody({
      "produtoId": "f1863f9b-0685-4c04-904e-154c72e89a89",
      "quantidade": 3
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O Id de produto informado já está no carrinho.")

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 5 - POST (Sem quantidade suficiente de estoque)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CarrinhoDeCompras/bb27a223-f0b7-45fa-87c6-08e34de58818')
    .withBody({
      "produtoId": "a8ebc02b-1383-4be0-b835-49bed1b4db94",
      "quantidade": 11
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("Não existe estoque para o produto informado.")

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 6 - POST (Adicionando produto inativo no carrinho)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CarrinhoDeCompras/bb27a223-f0b7-45fa-87c6-08e34de58818')
    .withBody({
      "produtoId": "de06972f-df55-452f-8acb-e80e6baf53e7",
      "quantidade": 1
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O produto informado está inativo e não pode ser adicionado ao carrinho.")

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});