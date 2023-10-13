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

export function testPostProduct() {
  let productName = generateRandomName(); 
    let requestBody = {
      "nome": productName,
      "descricao": productName,
      "peso": 4,
      "altura": 2,
      "largura": 4,
      "comprimento": 4,
      "valor": 150.50,
      "quantidadeEmEstoque": 10,
      "centroDeDistribuicaoId": "51eeeb4f-a4e5-40f3-9901-fab4868bbc68",
      "subcategoriaId": "08db735c-0893-419e-8177-2fceaf2aec54"
    };

    let headers = {
      "Content-Type": "application/json",
    };

    let response = http.post("https://localhost:7296/Produto", JSON.stringify(requestBody), { headers: headers });

    check(response, {
      "Status é 201 - POST Produto": (r) => r.status === 201,
      "Duração máxima - POST Produto": (r) => r.timings.duration < 1000,
    });

    let responseTime = response.timings.duration;

    let metricsPostProdutos = {
      min_response_time: responseTime,
      max_response_time: responseTime,
      avg_response_time: responseTime,
    };

    return metricsPostProdutos;
}