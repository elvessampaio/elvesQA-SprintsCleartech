import { test, expect } from '@playwright/test';
const { generateRandomEmail, generateRandomPassword } = require('./createaccount.js'); 

test('Teste de carregamento da página', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  
  await page.waitForLoadState('domcontentloaded');
  const title = await page.title();
  expect(title).not.toBeNull();
});

test('Teste de comportamento responsivo', async ({ page }) => {
  const sizes = [320, 768, 900, 1024, 1200, 1300, 1420]; 

  for (const size of sizes) {
    await page.setViewportSize({ width: size, height: 800 });
    await page.goto('http://www.automationpractice.pl/index.php');
  }
});

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

test('Teste de login realizado com sucesso', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('a.login');
  await page.fill('#email', 'emoura@cleartech.dev');
  await page.fill('#passwd', '168618');
  await page.click('#SubmitLogin');
});

test('Teste de logout realizado com sucesso', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('a.login');
  await page.fill('#email', 'emoura@cleartech.dev');
  await page.fill('#passwd', '168618');
  await page.click('#SubmitLogin');
  await page.click('a.logout')
});

test('Teste de pesquisa de produto', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.fill('#search_query_top', 'T-SHIRTS');
  await page.press('#search_query_top', 'Enter');
});

test('Enviar Mensagem para suporte da loja SEM ANEXO', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('#contact-link');
  await page.selectOption('select#id_contact', 'Customer service');
  const randomEmail = generateRandomEmail();
  await page.fill('input#email', randomEmail);
  await page.fill('textarea#message', 'Mensagem de teste.');
  await page.click('button#submitMessage'); 
});

test('Enviar mensagem para suporte da loja COM ANEXO', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('#contact-link');
  await page.selectOption('select#id_contact', 'Webmaster');
  const randomEmail = generateRandomEmail();
  await page.fill('input#email', randomEmail);
  await page.fill('textarea#message', 'Mensagem de teste.');
  
  await page.setInputFiles('input#fileUpload', './tests/upload.png');

  await page.click('button#submitMessage'); 
});

test('Teste de seleção do produto e exclusão do carrinho', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');

  await page.fill('#search_query_top', 'T-SHIRTS');
  await page.press('#search_query_top', 'Enter');

  await page.click('img.replace-2x');

  await page.click('a#color_11');
  await page.selectOption('select[name="group_1"]', 'L'); 
  await page.fill('input[name="qty"]', '2');
  await page.click('button[name="Submit"]');

  await page.click('a[title="Close"]')

  await page.click('a[title="View my shopping cart"]');
  await page.click('a[title="Delete"]');
});

test('Teste para acessar seções da página', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.click('a[title="Women"]');
  //await page.click('a.home');
  //await page.click('a[title="Dresses"]:nth-child(2)');
  await page.click('a[title="Blog"]');
});

//FAZER TESTES DE FINALIZAÇÃO DE COMPRA 
