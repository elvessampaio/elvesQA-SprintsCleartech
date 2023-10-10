const { spec } = require('pactum');

it('Teste 1 - DEL (Sem informar ID)', async () => {
    let {body, responseTime, statusCode} = await spec()
      .delete('https://jsonplaceholder.typicode.com/posts/')
      .expectResponseTime(1000)
      .expectStatus(404);
      
      console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
  });
  
  it('Teste 2 - DEL (informando o ID)', async () => {
    let {body, responseTime, statusCode} = await spec()
      .delete('https://jsonplaceholder.typicode.com/posts/1')
      .expectResponseTime(1000)
      .expectStatus(200);
      
      console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
  });
