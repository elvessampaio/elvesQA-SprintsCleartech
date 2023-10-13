import http from "k6/http";
import { check, sleep } from "k6";


function generateRandomName() {
  const alphabet = "abcdefghijklmnopqrstuvwxyz";
  let name = "";
  for (let i = 0; i < 10; i++) {
    const randomIndex = Math.floor(Math.random() * alphabet.length);
    name += alphabet[randomIndex];
  }
  return name;
}

export function testPutCD() {
  let cdName = generateRandomName(); 
  let requestBody = {
    "Nome": cdName,
    "Numero": 215,
    "Complemento": 4512,
    "Cep": "02133040", 
    "Status": true
  };

  let headers = {
    "Content-Type": "application/json",
  };

  let response = http.put("https://localhost:7296/CentroDeDistribuicao/0359a9fd-03e8-4d00-abf8-9d91471d8a31", JSON.stringify(requestBody), { headers: headers });

  check(response, {
    "Status é 204 - PUT CD": (r) => r.status === 204,
    "Duração máxima - PUT CD": (r) => r.timings.duration < 2000,
  });

  let responseTime = response.timings.duration;

  let metricsPutCD = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1);
  return metricsPutCD;
}