const { spec } = require ('pactum');
const { generateRandomName } = require ('../Extras/random.js');
const { generateRandomNameWithCedilla } = require ('../Extras/random2.js');

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

it('Teste 1 - PUT (Alterando dados do produto)', async () => {
  let productName = generateRandomName()
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Produto/9755e521-07c9-4760-959d-ae40c0011aab')
    .withBody({
      "nome": productName,
      "descricao": productName,
      "peso": 1,
      "altura": 10,
      "largura": 5,
      "comprimento": 4,
      "valor": 25.50,
      "quantidadeEmEstoque": 15,
      "status": true
    })
    .expectResponseTime(1000)
    .expectStatus(204)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 2 - PUT (Alterando o status do produto)', async () => {
  let {responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Produto/9755e521-07c9-4760-959d-ae40c0011aab')
    .withBody({
      "nome": "Novas meias",
      "descricao": "meias longas",
      "peso": 1,
      "altura": 10,
      "largura": 5,
      "comprimento": 4,
      "valor": 25.50,
      "quantidadeEmEstoque": 15,
      "status": false
    })
    .expectResponseTime(1000)
    .expectStatus(204)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 3 - PUT (Produto com mais de 128 caracteres)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Produto/9755e521-07c9-4760-959d-ae40c0011aab')
    .withBody({
      "nome": "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      "descricao": "meias longas",
      "peso": 1,
      "altura": 10,
      "largura": 5,
      "comprimento": 4,
      "valor": 25.50,
      "quantidadeEmEstoque": 15,
      "status": true
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Nome deve possuir um tamanho máximo de 128 caracteres.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 4 - PUT (Produto com caracteres especiais)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Produto/9755e521-07c9-4760-959d-ae40c0011aab')
    .withBody({
      "nome": "L3eiteir)",
      "descricao": "meias longas",
      "peso": 1,
      "altura": 10,
      "largura": 5,
      "comprimento": 4,
      "valor": 25.50,
      "quantidadeEmEstoque": 15,
      "status": true
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Nome contém caracteres não permitidos.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 5 - PUT (Alteração do produto com palavras usando acentos ou “ç”.)', async () => {
  let categoryNameCedilla = generateRandomNameWithCedilla()
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Produto/9755e521-07c9-4760-959d-ae40c0011aab')
    .withBody({
      "nome": categoryNameCedilla,
      "descricao": "meias longas",
      "peso": 1,
      "altura": 10,
      "largura": 5,
      "comprimento": 4,
      "valor": 25.50,
      "quantidadeEmEstoque": 15,
      "status": true
    })
    .expectResponseTime(1000)
    .expectStatus(204)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 6 - PUT (Atualizando produto com os mesmos dados)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Produto/9755e521-07c9-4760-959d-ae40c0011aab')
    .withBody({
      "nome": "Onteço",
      "descricao": "meias longas",
      "peso": 1,
      "altura": 10,
      "largura": 5,
      "comprimento": 4,
      "valor": 25.50,
      "quantidadeEmEstoque": 15,
      "status": true
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("A edição não foi feita, pois os valores não foram alterados.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});