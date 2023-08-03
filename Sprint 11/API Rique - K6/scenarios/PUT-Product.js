import http from "k6/http";
import { check, sleep } from "k6";

let statusProduct = true;

function generateRandomName() {
  const alphabet = "abcdefghijklmnopqrstuvwxyz";
  let name = "";
  for (let i = 0; i < 10; i++) {
    const randomIndex = Math.floor(Math.random() * alphabet.length);
    name += alphabet[randomIndex];
  }
  return name;
}

export function testPutProduto() {
  let productName = generateRandomName(); // Gerar um nome aleatório
  let requestBody = {
    "Nome": productName,
    "Descricao": productName,
    "Status": statusProduct,
    "CategoriaID": 21,
    "SubcategoriaID": 5,
    "CentroDistribuicaoID": 2,
  };

  let headers = {
    "Content-Type": "application/json",
  };

  let response = http.put("https://localhost:7161/Produto/114", JSON.stringify(requestBody), { headers: headers });

  check(response, {
    "Status é 204 - PUT Produto": (r) => r.status === 204,
    "Duração máxima - PUT Produto": (r) => r.timings.duration < 1000,
  });

  let responseTime = response.timings.duration;

  let metricsPutProduto = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); // Intervalo de espera entre as requisições
  return metricsPutProduto;
}