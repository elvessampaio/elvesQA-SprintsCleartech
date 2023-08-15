const { spec } = require('pactum');

it('Teste 1 GET', async () => {
  let {body, responseTime, statusCode} = await spec()
    .get('http://numbersapi.com/1993/year')
    .expectResponseTime(1000)
    .expectStatus(200)
    ;
    console.log(body, 'Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 2 - POST', async () => {
  let {body, responseTime, statusCode} = await spec()
    .post('https://jsonplaceholder.typicode.com/posts')
    .withBody({
      "nome": "Elves",
      "idade": 29,
      "id": ""
    })
    .expectResponseTime(1000)
    .expectStatus(201)
    ;
    console.log(body, 'Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 3 - PUT', async () => {
  let {body, responseTime, statusCode} = await spec()
    .put('https://jsonplaceholder.typicode.com/posts/1')
    .withBody({
      "nome": "Elves",
      "idade": 30,
      "id": ""
    })
    .expectResponseTime(1000)
    .expectStatus(200)
    ;
    console.log(body, 'Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});