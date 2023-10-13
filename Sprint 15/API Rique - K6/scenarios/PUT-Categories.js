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

export function testPutCategoria() {
  let categoryName = generateRandomName();
  let requestBody = {
    "Nome": categoryName,
  };

  let headers = {
    "Content-Type": "application/json",
  };

  let response = http.put("https://localhost:7296/Categoria/013c4c66-f5d2-43ec-80e3-4e48a7919f26", JSON.stringify(requestBody), { headers: headers });

  check(response, {
    "Status é 204 - PUT Categoria": (r) => r.status === 204,
    "Duração máxima - PUT Categoria": (r) => r.timings.duration < 1000,
  });

  let responseTime = response.timings.duration;

  let metricsPutCategoria = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); // Intervalo de espera entre as requisições
  return metricsPutCategoria;
}