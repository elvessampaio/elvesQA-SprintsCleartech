const { spec } = require('pactum');

it('Teste 1 - DEL (Deletando a categoria)', async () => {
    let {responseTime, statusCode} = await spec()
      .delete('https://localhost:7296/Categoria/01c3c009-44ca-4307-9384-7f937595228c')
      .expectResponseTime(1000)
      .expectStatus(204);
      
      console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
  });
  
  it('Teste 2 - DEL (Deletando a subcategoria)', async () => {
    let {responseTime, statusCode} = await spec()
      .delete('https://localhost:7296/Cubcategoria/89a03d70-32b5-499d-bd73-efbfe25a6e35')
      .expectResponseTime(1000)
      .expectStatus(204);
      
      console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
  });

  it('Teste 3 - DEL (Deletando produto)', async () => {
    let {responseTime, statusCode} = await spec()
      .delete('https://localhost:7296/Produto/12fb4f86-115a-4c1f-b419-a2b4fadc39ae')
      .expectResponseTime(1000)
      .expectStatus(204);
      
      console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
  });

  it('Teste 4 - DEL (Deletando CD)', async () => {
    let {responseTime, statusCode} = await spec()
      .delete('https://localhost:7296/CentroDeDistribuicao/e9e673c9-c1ce-4897-b964-f11683672bc9')
      .expectResponseTime(1000)
      .expectStatus(204);
      
      console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
  });

  it('Teste 5 - DEL (Deletando carrinho de compras)', async () => {
    let {responseTime, statusCode} = await spec()
      .delete('https://localhost:7296/CarrinhodeCompras/719b7146-2ab0-4224-ac6e-01c7f59b418b')
      .expectResponseTime(1000)
      .expectStatus(204);
      
      console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
  });

  it('Teste 6 - DEL (Deletando produto do carrinho de compras)', async () => {
    let {responseTime, statusCode} = await spec()
      .delete('https://localhost:7296/CarrinhodeCompras/9fc49b77-38e2-4ff8-988d-9fca04549be4/produto/46d4ca1a-abb8-42b1-8664-26944afd8f82')
      .expectResponseTime(1000)
      .expectStatus(204);
      
      console.log('Tempo de requisição:', responseTime, '|', 'Status Code:', statusCode)
  });