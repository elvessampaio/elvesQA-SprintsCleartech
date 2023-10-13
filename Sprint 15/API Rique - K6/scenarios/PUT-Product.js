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
  let productName = generateRandomName(); 
  let requestBody = {
    "nome": productName,
    "descricao": productName,
    "peso": 4,
    "altura": 54,
    "largura": 45,
    "comprimento": 10,
    "valor": 150.45,
    "quantidadeEmEstoque": 20,
    "status": true
  };

  let headers = {
    "Content-Type": "application/json",
  };

  let response = http.put("https://localhost:7296/Produto/04f6a60e-2ca5-49db-8066-e009d1cf86b0", JSON.stringify(requestBody), { headers: headers });

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

  sleep(1); 
  return metricsPutProduto;
}