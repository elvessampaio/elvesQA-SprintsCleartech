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

export function testPostSubcategoria() {{
    let subcategoryName = generateRandomName(); 
    let requestBody = {
    "nome": subcategoryName,
     "status": true,
     "categoriaId": "1015de8d-155b-484e-befd-7d12b6116ad3"
};

    let headers = {
      "Content-Type": "application/json",
    };

    let response = http.post("https://localhost:7296/Subcategoria", JSON.stringify(requestBody), { headers: headers });

    check(response, {
      "Status é 201 - POST Subcategoria": (r) => r.status === 201,
      "Duração máxima - POST Subcategoria": (r) => r.timings.duration < 1000,
    });

    let responseTime = response.timings.duration;

    let metricsPostSubcategoria = {
      min_response_time: responseTime,
      max_response_time: responseTime,
      avg_response_time: responseTime,
    };

    sleep(1); 
    return metricsPostSubcategoria;
  }
}