import { test, expect } from '@playwright/test';
const { generateRandomEmail, generateRandomPassword } = require('./createaccount.js'); 

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

test('Teste de pesquisa de produto', async ({ page }) => {
  await page.goto('http://www.automationpractice.pl/index.php');
  await page.fill('#search_query_top', 'T-SHIRTS');
  await page.press('#search_query_top', 'Enter');
});

