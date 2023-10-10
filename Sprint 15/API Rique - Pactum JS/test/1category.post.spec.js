const { spec } = require('pactum');

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

it('Teste 1 - POST (Login)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7221/Login')
    .withBody({
      "Email": "admin@admin.com",
      "Senha": "Admin.123!"
    })
    .expectResponseTime(1000)
    .expectStatus(200);

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode);
});

it('Teste 2 - POST (Nome de categoria correta com autenticação)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Categoria')
    .withHeaders({
      'Authorization':'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "Testeeeee"
    })
    .expectResponseTime(1000)
    .expectStatus(201)
    .expectBodyContains("Testeeeee");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 3 - POST (Categoria já cadastrada)', async () => {
  let {responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Categoria')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "Teste"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome 'Teste' já existe.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 4 - POST (Categoria com mais de 128 caracteres)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Categoria')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome não pode conter mais de 128 caracteres.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 5 - POST (Categoria com caracteres especiais)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Categoria')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "L3ite"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome não aceita caracteres especiais.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 5 - POST (Cadastro da categoria com palavras usando acentos ou “ç”.)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Categoria')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "Açúcar"
    })
    .expectResponseTime(1000)
    .expectStatus(201)
    .expectBodyContains("Açúcar");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 6 - POST (Categoria em branco)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Categoria')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": " "
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Nome é obrigatório.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});