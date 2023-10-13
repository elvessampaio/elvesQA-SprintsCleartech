import http from "k6/http";
import { check, sleep } from "k6";

function generateRandomName() {
  const alphabet = "abcdefghijklmnopqrstuvwxyz";
  let name = "";
  for (let i = 0; i < 5; i++) {
    const randomIndex = Math.floor(Math.random() * alphabet.length);
    name += alphabet[randomIndex];
  }
  return name;
}

function generateRandomNumber() {
  return Math.floor(Math.random() * 9000) + 1000;
}

export function testPostCD() {
  let cdName = generateRandomName(); 
  let number = generateRandomNumber(); 

  let requestBody = {
    "Nome": cdName,
    "Cep": "02133040",
    "Numero": number,
    "Complemento": number
  };

  let headers = {
    "Content-Type": "application/json",
  };

  let response = http.post("https://localhost:7296/CentroDeDistribuicao", JSON.stringify(requestBody), { headers: headers });

  check(response, {
    "Status é 201 - POST CD": (r) => r.status === 201,
    "Duração máxima - POST CD": (r) => r.timings.duration < 2000,
  });

  let responseTime = response.timings.duration;

  let metricsPostCD = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1);
  return metricsPostCD;
}