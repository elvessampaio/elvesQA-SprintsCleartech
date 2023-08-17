const { spec } = require('pactum');

it('Teste 1 - PUT', async () => {
    let {body, responseTime, statusCode} = await spec()
      .put('https://jsonplaceholder.typicode.com/posts/1')
      .withBody({
        "nome": "Elves",
        "idade": 30,
        "id": ""
      })
      .expectResponseTime(1000)
      .expectStatus(200)
      .expectBody({nome: 'Elves', idade: 30, id: 1})
  
    console.log(body, 'Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode);
  });

  it('Teste 2 - PUT (URL que aceita apenas GET)', async () => {
    let {body, responseTime, statusCode} = await spec()
      .put('http://numbersapi.com')
      .withBody({
        "nome": "Elves",
        "idade": 29,
      })
      .expectResponseTime(1000)
      .expectStatus(404)
      
      console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
  });
  