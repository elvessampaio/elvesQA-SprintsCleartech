const { spec } = require('pactum');

it('should get a response with status code 200', async () => {
  await spec()
    .get('https://www.servicos.gov.br/api/v1/servicos/10045')
    .expectStatus(200);
});
