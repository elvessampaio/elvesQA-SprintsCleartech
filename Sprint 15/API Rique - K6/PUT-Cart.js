import http from "k6/http";
import { check, sleep } from "k6";

function generateRandomNumber() {
  return Math.floor(Math.random() * 9000) + 1000;
}

export function testPutCartAndress() {
  let number = generateRandomNumber(); 
  let requestBody = {
    "cep": "02133040",
    "numero": number,
    "complemento": number
  };

  let headers = {
    "Content-Type": "application/json",
  };

  let response = http.put("https://localhost:7296/carrinhodeCompras/0827d0bc-e7bd-4f48-91a1-7709b3cf83f8/enderecodeentrega", JSON.stringify(requestBody), { headers: headers });

  check(response, {
    "Status é 204 - PUT Carrinho de compras": (r) => r.status === 204,
    "Duração máxima - PUT Carrinho de compras": (r) => r.timings.duration < 1000,
  });

  let responseTime = response.timings.duration;

  let metricsPutCart = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); 
  return metricsPutCart;
}