import http from "k6/http";
import { check, sleep } from "k6";
import { idList } from "../Extras/idsCategoria.js";

// Função para realizar o DELETE
export function testDelCategoria(){
  let ids = idList;

  // Analisa o array de IDs e realiza a requisição DELETE para cada 
  for (let i = 0; i < ids.length; i++) {
    let response = http.del(`https://localhost:7161/Categoria/${ids[i]}`);

    check(response, {
      [`Status é 204 - DELETE Categoria ID ${ids[i]}`]: (r) => r.status === 204,
      [`Duração máxima - DELETE Categoria ID ${ids[i]}`]: (r) => r.timings.duration < 1000,
    });

    let responseTime = response.timings.duration;

    let metricsDelCategoria = {
      min_response_time: responseTime,
      max_response_time: responseTime,
      avg_response_time: responseTime,
    };

    sleep(1); // Intervalo de espera entre as requisições
    return metricsDelCategoria;
  }
}

