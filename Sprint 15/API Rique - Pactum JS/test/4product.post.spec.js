const { spec } = require('pactum');

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

it('Teste 1 - POST (Nome do produto correto com autenticação)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Produto')
    .withBody({
        "nome": "Meias novas",
        "descricao": "Meias lindas e artiticas",
        "peso": 4,
        "altura": 2,
        "largura": 4,
        "comprimento": 4,
        "valor": 15.50,
        "quantidadeEmEstoque": 10,
        "centroDeDistribuicaoId": "51eeeb4f-a4e5-40f3-9901-fab4868bbc68",
        "subcategoriaId": "08db735c-0893-419e-8177-2fceaf2aec54"
    })
    .expectResponseTime(1000)
    .expectStatus(201)

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 2 - POST (Produto já cadastrada)', async () => {
  let {responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Produto')
    .withBody({
      "nome": "Meias novas",
      "descricao": "Meias lindas e artiticas",
      "peso": 4,
      "altura": 2,
      "largura": 4,
      "comprimento": 4,
      "valor": 15.50,
      "quantidadeEmEstoque": 10,
      "centroDeDistribuicaoId": "51eeeb4f-a4e5-40f3-9901-fab4868bbc68",
      "subcategoriaId": "08db735c-0893-419e-8177-2fceaf2aec54"
  })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O nome cadastrado já existe.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});


it('Teste 3 - POST (Produto com mais de 128 caracteres)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Produto')
    .withBody({
      "nome": "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
      "descricao": "Meias lindas e artiticas",
      "peso": 4,
      "altura": 2,
      "largura": 4,
      "comprimento": 4,
      "valor": 15.50,
      "quantidadeEmEstoque": 10,
      "centroDeDistribuicaoId": "51eeeb4f-a4e5-40f3-9901-fab4868bbc68",
      "subcategoriaId": "08db735c-0893-419e-8177-2fceaf2aec54"
  })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Nome deve possuir um tamanho máximo de 128 caracteres.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 4 - POST (Produto em branco)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Produto')
    .withBody({
      "nome": " ",
      "descricao": "Meias lindas e artiticas",
      "peso": 4,
      "altura": 2,
      "largura": 4,
      "comprimento": 4,
      "valor": 15.50,
      "quantidadeEmEstoque": 10,
      "centroDeDistribuicaoId": "51eeeb4f-a4e5-40f3-9901-fab4868bbc68",
      "subcategoriaId": "08db735c-0893-419e-8177-2fceaf2aec54"
  })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("O campo Nome é obrigatório.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

it('Teste 5 - POST (Produto sem preço)', async () => {
  let { responseTime, statusCode } = await spec()
    .post('https://localhost:7296/Produto')
    .withBody({
      "nome": "Novo teste",
      "descricao": "Meias lindas e artiticas",
      "peso": 4,
      "altura": 2,
      "largura": 4,
      "comprimento": 4,
      "valor": 0,
      "quantidadeEmEstoque": 10,
      "centroDeDistribuicaoId": "51eeeb4f-a4e5-40f3-9901-fab4868bbc68",
      "subcategoriaId": "08db735c-0893-419e-8177-2fceaf2aec54"
  })
    .expectResponseTime(1000)
    .expectStatus(400)
    .expectBodyContains("Valores informados para cadastro de produto não são válidos.");

  console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
});

