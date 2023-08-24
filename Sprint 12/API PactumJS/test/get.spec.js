const { spec } = require('pactum');


it('Teste 1 - GET', async () => {
  let {responseTime, statusCode} = await spec()
    .get('https://api.dicebear.com/6.x/pixel-art/svg?seed=Elves')
    .expectResponseTime(1000)
    .expectStatus(200)
    
    console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 2 - GET (Informando uma referência)', async () => {
  let {body, responseTime, statusCode} = await spec()
    .get('http://numbersapi.com/1993/year')
    .expectResponseTime(1000)
    .expectStatus(200)
    .expectBodyContains("1993 is the year")
    
    console.log(body, 'Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 3 - GET (Sem informar referência)', async () => {
  let {responseTime, statusCode} = await spec()
    .get('http://numbersapi.com/')
    .expectResponseTime(1000)
    .expectStatus(200)
    
    console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 4 - GET (Verificação de body)', async () => {
  let {body, responseTime, statusCode} = await spec()
    .get('http://api.open-notify.org/iss-now.json')
    .expectResponseTime(1000)
    .expectStatus(200)
    .expectBodyContains("success")
    
    console.log(body, 'Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 5 - GET (ID não cadastrado)', async () => {
  let {statusCode} = await spec()
    .get('https://swapi.dev/api/people/150')
    .expectStatus(404)
    .expectResponseTime(5000)
    .expectBodyContains("Not found");

    console.log('Status Code:', statusCode)
});


it('Teste 6 - GET (Sem as credencias definidas)', async () => {
  let {responseTime, statusCode} = await spec()
    .get('http://localhost:8080/risco/7041386')
    .expectResponseTime(1000)
    .expectStatus(401)
    .expectBodyContains("Unauthorized")
    
    console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 7 - GET (Credencias definidas corretamente)', async () => {
  let {body, responseTime, statusCode} = await spec()
    .get('http://localhost:8080/risco/7041386')
    .withAuth('aluno', 'senha')
    .expectResponseTime(1000)
    .expectStatus(200)

    console.log(body, 'Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 8 - GET (Credencias definidas incorretamente)', async () => {
  let {responseTime, statusCode} = await spec()
    .get('http://localhost:8080/risco/7041386')
    .withAuth('senha', 'aluno')
    .expectResponseTime(1000)
    .expectStatus(401)
    .expectBodyContains("Unauthorized")

    console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 8 - GET (Com as credencias definidas incorretas)', async () => {
  let {responseTime, statusCode} = await spec()
    .get('http://localhost:8080/risco/7041386')
    .withAuth('senha', 'aluno')
    .expectResponseTime(1000)
    .expectStatus(401)
    .expectBodyContains("Unauthorized")

    console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});