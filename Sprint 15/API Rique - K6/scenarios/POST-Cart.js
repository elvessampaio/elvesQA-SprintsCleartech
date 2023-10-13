import http from "k6/http";
import { check, sleep } from "k6";


export function testPostCart() {{
    let requestBody = {
      "produtoId": "271d5cc6-a97c-42fb-8767-b13ac832d5a0",
      "quantidade": 4
    };

    let headers = {
      "Content-Type": "application/json",
    };

    let response = http.post("https://localhost:7296/CarrinhoDeCompras", JSON.stringify(requestBody), { headers: headers });

    check(response, {
      "Status é 201 - POST Cart": (r) => r.status === 201,
      "Duração máxima - POST Cart": (r) => r.timings.duration < 1000,
    });

    let responseTime = response.timings.duration;

    let metricsPostCart = {
      min_response_time: responseTime,
      max_response_time: responseTime,
      avg_response_time: responseTime,
    };

    sleep(1); 
    return metricsPostCart;
  }
}