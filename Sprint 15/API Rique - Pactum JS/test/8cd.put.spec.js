const { spec } = require ('pactum');
const { generateRandomName } = require ('../Extras/random.js');

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

it('Teste 1 - PUT (Alterando dados do CD)', async () => {
  let cdName = generateRandomName()
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/CentroDeDistribuicao/8110ed97-bc11-4017-9c9d-b4aaeed26530')
    .withBody({
      "nome": cdName,
      "numero": 32,
      "complemento": "Loja 10",
      "cep": "02133040",
      "status": true
    })
    .expectResponseTime(1000)
    .expectStatus(204)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 2 - PUT (Alterando staus do CD)', async () => {
  let {responseTime, statusCode } = await spec()
    .put('https://localhost:7296/CentroDeDistribuicao/8110ed97-bc11-4017-9c9d-b4aaeed26530')
    .withBody({
      "nome": "CD Zona oeste",
      "numero": 32,
      "complemento": "Loja 10",
      "cep": "02133040",
      "status": false
    })
    .expectResponseTime(1000)
    .expectStatus(204)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 3 - PUT (CD com mais de 128 caracteres)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/CentroDeDistribuicao/8110ed97-bc11-4017-9c9d-b4aaeed26530')
    .withBody({
      "nome": "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      "numero": 32,
      "complemento": "Loja 10",
      "cep": "02133040",
      "status": true
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Nome deve possuir um tamanho máximo de 128 caracteres");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 4 - PUT (Nome CD em branco)', async () => {
  let { responseTime, statusCode } = await spec()
    .put('https://localhost:7296/CentroDeDistribuicao/8110ed97-bc11-4017-9c9d-b4aaeed26530')
    .withBody({
      "nome": "",
      "numero": 32,
      "complemento": "Loja 10",
      "cep": "02133040",
      "status": true
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Nome é obrigatório.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});