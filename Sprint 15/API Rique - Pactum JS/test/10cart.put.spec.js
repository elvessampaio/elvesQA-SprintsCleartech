const { spec } = require ('pactum');

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

it('Teste 1 - PUT (Alterando endereço sem informar complemento)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/CarrinhoDeCompras/fad34385-c595-4051-a2ae-b33956416a57/enderecoDeEntrega')
    .withBody({
      "cep": "02133020",
      "numero": 383,
      "complemento": ""
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Complemento é obrigatório.")

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 2 - PUT (CEP incorreto)', async () => {
  let {responseTime, statusCode } = await spec()
    .put('https://localhost:7296/CarrinhoDeCompras/fad34385-c595-4051-a2ae-b33956416a57/enderecoDeEntrega')
    .withBody({
      "cep": "02133-040",
      "numero": 383,
      "complemento": "Casa 1"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo CEP deve possuir um tamanho máximo de 8 caracteres.")

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 3 - PUT (Sem informar CEP)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/CarrinhoDeCompras/fad34385-c595-4051-a2ae-b33956416a57/enderecoDeEntrega')
    .withBody({
      "cep": "",
      "numero": 383,
      "complemento": "Casa 1"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo CEP é obrigatório.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

