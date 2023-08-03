import http from "k6/http";
import { check, sleep } from "k6";
import { cepList } from "../ceps.js";

// Função para gerar nomes aleatórios com letras do alfabeto
function generateRandomName() {
  const alphabet = "abcdefghijklmnopqrstuvwxyz";
  let name = "";
  for (let i = 0; i < 5; i++) {
    const randomIndex = Math.floor(Math.random() * alphabet.length);
    name += alphabet[randomIndex];
  }
  return name;
}

// Função para selecionar um CEP aleatório da lista de CEPs existentes
function getRandomCep() {
  const randomIndex = Math.floor(Math.random() * cepList.length);
  return cepList[randomIndex];
}

// Função para gerar número de endereço aleatório
function generateRandomNumber() {
  return Math.floor(Math.random() * 1000) + 1;
}

// Função para realizar o POST
export function testPostCD() {
  let categoryName = generateRandomName(); // Gerar um nome aleatório
   let cep = cepList[Math.floor(Math.random() * cepList.length)]; // Selecionar um CEP aleatório da lista de CEPs existentes
  let number = generateRandomNumber(); // Gerar um número de endereço aleatório

  let requestBody = {
    "Nome": categoryName,
    "Cep": cep,
    "Numero": number,
    "Complemento": "Loja 1"
  };

  let headers = {
    "Content-Type": "application/json",
  };

  let response = http.post("https://localhost:7161/CentroDistribuicao", JSON.stringify(requestBody), { headers: headers });

  check(response, {
    "Status é 201 - POST CD": (r) => r.status === 201,
    "Duração máxima - POST CD": (r) => r.timings.duration < 1000,
  });

  let responseTime = response.timings.duration;

  let metricsPostCD = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); // Intervalo de espera entre as requisições
  return metricsPostCD;
}