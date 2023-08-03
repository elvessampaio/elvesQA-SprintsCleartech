import http from "k6/http";
import { check, sleep } from "k6";

// Cenário 1 - Teste das categorias
export function testProducts() {
  let response = http.get("https://localhost:7161/Produto/4");

  // Verifica o status code da resposta
  check(response, {
    "Status é 200": (r) => r.status === 200,
    "Duração máxima": (r) => r.timings.duration < 1000,
  });

  // Calcula o tempo de resposta
  let responseTime = response.timings.duration;

  // Salva as métricas para o relatório
  let metrics = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  // Gera quantidade aleatória de acessos por minuto e segundo
  sleep(Math.random() * 2 + 1); // Intervalo de espera entre as requisições
  sleep(1); // Intervalo de espera entre as requisições
  sleep(Math.random() * 2 + 1); // Intervalo de espera entre as requisições

  return metrics;
}

// Cenário 2 - Teste do tempo de carregamento das páginas
export function testPageLoadTimeProducts() {
  let pageUrls = [
    "https://localhost:7161/Produto/4"
   ];

  let randomPageUrl = pageUrls[Math.floor(Math.random() * pageUrls.length)];
  let response = http.get(randomPageUrl);

  check(response, {
    "Status é 200 - Produto": (r) => r.status === 200,
    "Duração máxima - produto": (r) => r.timings.duration < 1000,
  });

  // Calcula o tempo de resposta
  let responseTime = response.timings.duration;

  // Salva as métricas para o relatório
  let metricsProducts = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(2);
  return metricsProducts;
}

// Cenário 3 - Teste de cache desabilitado
export function testCacheProductsDisabled() {
  // Realize uma solicitação para obter os dados com o cache desabilitado
  let response = http.get("https://localhost:7161/Produto/4", {
    headers: {
      "Cache-Control": "no-cache",
    },
  });

  // Verifica o status code da resposta
  check(response, {
    "Status é 200 - Produto - Chace Desabilitado": (r) => r.status === 200,
    "Duração máxima - Produto - Chace Desabilitado": (r) => r.timings.duration < 1000,
  });

  // Calcula o tempo de resposta
  let responseTime = response.timings.duration;

  // Salva as métricas para o relatório
  let metricsProductsDisabled = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); // Intervalo de espera entre as requisições
  return metricsProductsDisabled;
}

// Cenário  - Teste de cache habilitado
export function testCacheProductsEnabled() {
  // Realize uma solicitação para obter os dados com o cache desabilitado
  let response = http.get("https://localhost:7161/Produto/4", {
  });

  // Verifica o status code da resposta
  check(response, {
    "Status é 200 - Produto - Chace Habilitado": (r) => r.status === 200,
    "Duração máxima - Produto - Chace Habilitado": (r) => r.timings.duration < 1000,
  });

  // Calcula o tempo de resposta
  let responseTime = response.timings.duration;

  // Salva as métricas para o relatório
  let metricsProductsEnabled = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); // Intervalo de espera entre as requisições
  return metricsProductsEnabled;
}