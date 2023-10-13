const { spec } = require ('pactum');
const { generateRandomName } = require ('../Extras/random.js');
const { generateRandomNameWithCedilla } = require ('../Extras/random2.js');

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

it('Teste 1 - PUT (Alterando status da subcategoria)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Subcategoria/aea43c0a-df50-4dc6-80a1-aed34dbef454')
    .withBody({
      "nome": "Armazém nível senior",
      "status": false
  })
    .expectResponseTime(1000)
    .expectStatus(204)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 2 - PUT (Alterando o nome da subcategoria)', async () => {
  let subcategoryName = generateRandomName()
  let {responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Subcategoria/aea43c0a-df50-4dc6-80a1-aed34dbef454')
    .withBody({
      "nome": subcategoryName,
      "status": true
  })
    .expectResponseTime(1000)
    .expectStatus(204)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 3 - PUT (Subcategoria com mais de 128 caracteres)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Subcategoria/aea43c0a-df50-4dc6-80a1-aed34dbef454')
    .withBody({
      "nome": "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      "status": true
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome não pode conter mais de 128 caracteres.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 4 - PUT (Subcategoria com caracteres especiais)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Subcategoria/aea43c0a-df50-4dc6-80a1-aed34dbef454')
    .withBody({
      "nome": "L3ite"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome não aceita caracteres especiais.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 5 - PUT (Alteração da subcategoria com palavras usando acentos ou “ç”.)', async () => {
  let categoryNameCedilla = generateRandomNameWithCedilla()
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Subcategoria/aea43c0a-df50-4dc6-80a1-aed34dbef454')
    .withBody({
      "nome": categoryNameCedilla
    })
    .expectResponseTime(1000)
    .expectStatus(201)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 6 - PUT (Subcategoria em branco)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/Subcategoria/aea43c0a-df50-4dc6-80a1-aed34dbef454')
    .withBody({
      "nome": " "
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("The Nome field is required.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});