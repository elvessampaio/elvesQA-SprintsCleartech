import http from "k6/http";
import { check, sleep } from "k6";

let cep = "03041000";
let compl = "1"

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
  let cdName = generateRandomName(); // Gerar um nome aleatório
  let requestBody = {
    "Nome": cdName,
    "Cep": cep, 
    "Complemento": compl,
  };

  let headers = {
    "Content-Type": "application/json",
  };

  let response = http.put("https://localhost:7161/CentroDistribuicao/3", JSON.stringify(requestBody), { headers: headers });

  check(response, {
    "Status é 200 - PUT CD": (r) => r.status === 200,
    "Duração máxima - PUT CD": (r) => r.timings.duration < 1000,
  });

  let responseTime = response.timings.duration;

  let metricsPutCD = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); // Intervalo de espera entre as requisições
  return metricsPutCD;
}