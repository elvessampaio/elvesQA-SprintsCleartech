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

// Função para realizar o POST com o ID da categoria obtido no GET
export function testPostProduct() {
  let productName = generateRandomName(); // Gerar um nome aleatório
    let requestBody = {
      "nome": productName,
      "descricao": productName,
      "categoriaId": 20,
      "subcategoriaId": 4,
      "centroDistribuicaoId": 1,
    };

    let headers = {
      "Content-Type": "application/json",
    };

    let response = http.post("https://localhost:7161/Produto", JSON.stringify(requestBody), { headers: headers });

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