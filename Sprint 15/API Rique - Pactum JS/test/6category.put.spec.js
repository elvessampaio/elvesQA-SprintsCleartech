const { spec } = require ('pactum');
const { generateRandomName } = require ('../Extras/random.js');
const { generateRandomNameWithCedilla } = require ('../Extras/random2.js');

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

it('Teste 1 - PUT (Alterando status da categoria)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Categoria/430f2f6a-e840-4206-8c84-fb0d5f7928c9')
    .withBody({
      "nome": "Armazém",
      "status": false
  })
    .expectResponseTime(1000)
    .expectStatus(204)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 2 - PUT (Alterando o nome da categoria)', async () => {
  let categoryName = generateRandomName()
  let {responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Categoria/430f2f6a-e840-4206-8c84-fb0d5f7928c9')
    .withBody({
      "nome": categoryName,
      "status": true
  })
    .expectResponseTime(1000)
    .expectStatus(204)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 3 - PUT (Categoria com mais de 128 caracteres)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Categoria/430f2f6a-e840-4206-8c84-fb0d5f7928c9')
    .withBody({
      "nome": "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      "status": true
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome não pode conter mais de 128 caracteres.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 4 - PUT (Categoria com caracteres especiais)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Categoria/430f2f6a-e840-4206-8c84-fb0d5f7928c9')
    .withBody({
      "nome": "L3ite"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome não aceita caracteres especiais.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 5 - PUT (Alteração da categoria com palavras usando acentos ou “ç”.)', async () => {
  let categoryNameCedilla = generateRandomNameWithCedilla()
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Categoria/430f2f6a-e840-4206-8c84-fb0d5f7928c9')
    .withBody({
      "nome": categoryNameCedilla
    })
    .expectResponseTime(1000)
    .expectStatus(201)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 6 - PUT (Categoria em branco)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Categoria/430f2f6a-e840-4206-8c84-fb0d5f7928c9')
    .withBody({
      "nome": " "
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Nome é obrigatório.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});