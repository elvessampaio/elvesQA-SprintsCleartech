import http from "k6/http";
import { check, sleep } from "k6";

// Função para gerar nomes aleatórios com letras do alfabeto
function generateRandomName() {
  const alphabet = "abcdefghijklmnopqrstuvwxyz";
  let name = "";
  for (let i = 0; i < 10; i++) {
    const randomIndex = Math.floor(Math.random() * alphabet.length);
    name += alphabet[randomIndex];
  }
  return name;
}

// Função para realizar o POST
export function testPostCategoria() {
  let categoryName = generateRandomName(); // Gerar um nome aleatório
  let requestBody = {
    "Nome": categoryName
  };

  let headers = {
    "Content-Type": "application/json",
  };

  let response = http.post("https://localhost:7161/Categoria", JSON.stringify(requestBody), { headers: headers });

  check(response, {
    "Status é 201 - POST Categoria": (r) => r.status === 201,
    "Duração máxima - POST Categoria": (r) => r.timings.duration < 1000,
  });

  let responseTime = response.timings.duration;

  let metricsPostCategoria = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); // Intervalo de espera entre as requisições
  return metricsPostCategoria;
}