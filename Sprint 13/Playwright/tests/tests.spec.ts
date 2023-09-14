import { test, expect } from '@playwright/test';
import { generateRandomEmail, generateRandomPassword } from './createaccount';

//TESTE 1
test('Teste de carregamento da página', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  
  await page.waitForLoadState('domcontentloaded');
  const title = await page.title();
  expect(title).not.toBeNull();
});

//TESTE 2
test('Teste de comportamento responsivo', async ({ page }) => {
  const sizes = [320, 768, 900, 1024, 1200, 1300, 1420]; 

  for (const size of sizes) {
    await page.setViewportSize({ width: size, height: 800 });
    await page.goto('http://www.automationpractice.pl/index.php');
  }
});

// TESTE 3
test('Teste de cadastro', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('a.login');
  const randomEmail = generateRandomEmail(); 
  await page.fill('#email_create', randomEmail); 
  await page.click('#SubmitCreate');
  await page.fill('#customer_firstname', 'Usuário');
  await page.fill('#customer_lastname', 'Teste');
  const randomPassword = generateRandomPassword(); 
  await page.fill('#passwd', randomPassword); 
  await page.selectOption('#days', '10');
  await page.selectOption('#months', '5');
  await page.selectOption('#years', '1990');
  await page.click('#submitAccount');
});

//TESTE 4
test('Teste de login realizado com sucesso', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('a.login');
  await page.fill('#email', 'emoura@cleartech.dev');
  await page.fill('#passwd', '168618');
  await page.click('#SubmitLogin');
});

//TESTE 5
test('Teste de logout realizado com sucesso', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('a.login');
  await page.fill('#email', 'emoura@cleartech.dev');
  await page.fill('#passwd', '168618');
  await page.click('#SubmitLogin');
  await page.click('a.logout')
});


//TESTE 6
test('Teste de pesquisa de produto', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.fill('#search_query_top', 'T-SHIRTS');
  await page.press('#search_query_top', 'Enter');
});


//TESTE 7
test('Enviar Mensagem para suporte da loja SEM ANEXO', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('#contact-link');
  await page.selectOption('select#id_contact', 'Customer service');
  const randomEmail = generateRandomEmail();
  await page.fill('input#email', randomEmail);
  await page.fill('textarea#message', 'Mensagem de teste.');
  await page.click('button#submitMessage'); 
});


//TESTE 8
test('Enviar mensagem para suporte da loja COM ANEXO', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('#contact-link');
  await page.selectOption('select#id_contact', 'Webmaster');
  const randomEmail = generateRandomEmail();
  await page.fill('input#email', randomEmail);
  await page.fill('textarea#message', 'Mensagem de teste.');
  
  await page.setInputFiles('input#fileUpload', './anexos/upload.png');

  await page.click('button#submitMessage'); 
});


//TESTE 9
test('Teste de seleção do produto e exclusão do carrinho', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');

  await page.fill('#search_query_top', 'T-SHIRTS');
  await page.press('#search_query_top', 'Enter');

  await page.click('img.replace-2x');

  await page.click('a#color_11');
  await page.selectOption('select[name="group_1"]', 'L'); 
  await page.fill('input[name="qty"]', '2');
  await page.click('button[name="Submit"]');

  await page.click('a[title="Close"]');

  await page.click('a[title="View my shopping cart"]');
  await page.click('a[title="Delete"]');
});


//TESTE 10
test('Teste para acessar seções da página', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('a[title="Women"]');
  //await page.click('a.home');
  //await page.click('a[title="Dresses"]:nth-child(2)');
  await page.click('a[title="Blog"]');
});


//TESTE 11
test('Teste para inscrição de Newsletter', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  const randomEmail = generateRandomEmail(); 

  await page.evaluate(() => {
    window.scrollBy(0, 800); 
  });
  await page.fill('#newsletter-input', randomEmail);
  await page.click('button.btn.btn-default.button.button-small');
});


//TESTE 12
test('Teste de botões redes sociais - Facebook', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('.facebook');
  await Promise.all([page.waitForEvent('popup')]);
});


//TESTE 13
test('Teste de botões redes sociais - Twitter X', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('.twitter');
  await Promise.all([page.waitForEvent('popup')]);
});

//TESTE 14
test('Teste de botões redes sociais -RSS', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('.rss');
  await Promise.all([page.waitForEvent('popup')]);
});


//TESTE 15
test('Teste completo do processo de compra', async ({ page }) => {
  //LOGIN
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('a.login');
  const randomEmail = generateRandomEmail(); 
  await page.fill('#email_create', randomEmail); 
  await page.click('#SubmitCreate');
  await page.fill('#customer_firstname', 'Usuário');
  await page.fill('#customer_lastname', 'Teste');
  const randomPassword = generateRandomPassword(); 
  await page.fill('#passwd', randomPassword); 
  await page.selectOption('#days', '10');
  await page.selectOption('#months', '5');
  await page.selectOption('#years', '1990');
  await page.click('#submitAccount');
  
  //PESQUISA DO PRODUTO
  await page.fill('#search_query_top', 'T-SHIRTS');
  await page.press('#search_query_top', 'Enter');
  await page.click('img.replace-2x')
  await page.click('a#color_8');
  await page.fill('input[name="qty"]', '4');
  await page.selectOption('select[name="group_1"]', 'M'); 
  await page.click('button[name="Submit"]');
  await page.click('a[title="Close"]');

  //ACESSANDO CARRINHO
  await page.click('a[title="View my shopping cart"]');
  await page.click('a.button.btn.btn-default.standard-checkout.button-medium');

  //CADASTRANDO ENDEREÇO
  await page.fill('input#address1', 'Rua Clear');
  await page.fill('input#city', 'São Paulo');
  await page.selectOption('select#id_state', 'Ohio');
  await page.fill('input#postcode', '43001');
  await page.fill('input#phone', '00');
  await page.fill('input#phone_mobile', '99999999');
  await page.click('button[name="submitAddress"]');

  //FINALIZANDO
  await page.click('button.button.btn.btn-default.button-medium');

  //PÁGINA DO CHECKOUT NÃO FUNCIONA
});
