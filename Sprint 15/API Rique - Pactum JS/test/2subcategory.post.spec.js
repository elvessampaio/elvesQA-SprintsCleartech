const { spec } = require('pactum');

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

it('Teste 1 - POST (Nome de subcategoria correta com autenticação)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Subcategoria')
    .withHeaders({
      'Authorization':'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
        "nome": "Teeeeeeeeeee",
        "status": true,
        "categoriaId": "1015de8d-155b-484e-befd-7d12b6116ad3"
    })
    .expectResponseTime(1000)
    .expectStatus(201)
    .expectBodyContains("Teeeeeeeeeee");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 2 - POST (Subcategoria já cadastrada)', async () => {
  let {responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Subcategoria')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "Teeeeeeeeeee",
      "status": true,
      "categoriaId": "1015de8d-155b-484e-befd-7d12b6116ad3"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome 'Teeeeeeeeeee' já existe.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 3 - POST (Subcategoria com mais de 128 caracteres)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Subcategoria')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      "status": true,
      "categoriaId": "1015de8d-155b-484e-befd-7d12b6116ad3"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome não pode conter mais de 128 caracteres.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 4 - POST (Subcategoria com caracteres especiais)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Subcategoria')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "L3ite",
      "status": true,
      "categoriaId": "1015de8d-155b-484e-befd-7d12b6116ad3"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome não aceita caracteres especiais.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 5 - POST (Cadastro da subcategoria com palavras usando acentos ou “ç”.)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Subcategoria')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": "Maçanetá",
      "status": true,
      "categoriaId": "1015de8d-155b-484e-befd-7d12b6116ad3"
    })
    .expectResponseTime(1000)
    .expectStatus(201)
    .expectBodyContains("Maçanetá");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 6 - POST (Subcategoria em branco)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Subcategoria')
    .withHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWQiOiJkYjgyZGM5NS1iYjc1LTQ3YmEtODRhNi04NTQ3ZGRiN2M5NDgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTY5NzA1MjMzMH0.n4Vfqgi82fZIKD_wKzCjnulNUHp_64K67NeSVzxBN2M'
    })
    .withBody({
      "nome": " ",
      "status": true,
      "categoriaId": "1015de8d-155b-484e-befd-7d12b6116ad3"
    })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Nome é obrigatório.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});