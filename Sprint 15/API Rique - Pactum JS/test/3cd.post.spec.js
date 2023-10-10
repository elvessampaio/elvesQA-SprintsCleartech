const { spec } = require('pactum');

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

it('Teste 1 - POST (Nome do CD correta com autenticação)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CentroDeDistribuicao')
    .withHeaders({
      'Authorization':'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "CD Vila Maria",
      "cep": "02133040",
      "numero": 2,
      "complemento": "Loja 1"
    })
    .expectResponseTime(1000)
    .expectStatus(201)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 2 - POST (CD já cadastrada)', async () => {
  let {responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CentroDeDistribuicao')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "CD Vila Maria",
      "cep": "02133040",
      "numero": 2,
      "complemento": "Loja 1"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome 'Cd Vila Maria' já existe.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 3 - POST (CD com mais de 128 caracteres)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CentroDeDistribuicao')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "CD aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      "cep": "02133040",
      "numero": 2,
      "complemento": "Loja 1"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Nome deve possuir um tamanho máximo de 128 caracteres.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 4 - POST (CD com nome em branco)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CentroDeDistribuicao')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": " ",
      "cep": "02133040",
      "numero": 2,
      "complemento": "Loja 1"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Nome é obrigatório.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 5 - POST (CEP incorreto)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CentroDeDistribuicao')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "CD Vila Guilherme",
      "cep": "0213300",
      "numero": 2,
      "complemento": "Loja 1"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo CEP deve possuir um tamanho mínimo de 8 caracteres.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 6 - POST (Sem complemento)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/CentroDeDistribuicao')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "CD Vila Guilherme",
      "cep": "02133040",
      "numero": 2,
      "complemento": " "
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Complemento é obrigatório.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});