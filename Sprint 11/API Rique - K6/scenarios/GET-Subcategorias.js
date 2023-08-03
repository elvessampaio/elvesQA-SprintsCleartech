import http from "k6/http";
import { check, sleep } from "k6";

// Cenário 1 - Teste das categorias
export function testSubcategories() {
  let response = http.get("https://localhost:7161/SubCategoria");

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
export function testPageLoadTimeSubcategories() {
  let pageUrls = [
    "https://localhost:7161/SubCategoria"
   ];

  let randomPageUrl = pageUrls[Math.floor(Math.random() * pageUrls.length)];
  let response = http.get(randomPageUrl);

  check(response, {
    "Status é 200 - Subcategoria": (r) => r.status === 200,
    "Duração máxima - Subcategoria": (r) => r.timings.duration < 1000,
  });

  // Calcula o tempo de resposta
  let responseTime = response.timings.duration;

  // Salva as métricas para o relatório
  let metricsSubcategories = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(2);
  return metricsSubcategories;
}

// Cenário 3 - Teste de cache desabilitado
export function testCacheSubcategoriaDisabled() {
  // Realize uma solicitação para obter os dados com o cache desabilitado
  let response = http.get("https://localhost:7161/SubCategoria", {
    headers: {
      "Cache-Control": "no-cache",
    },
  });

  // Verifica o status code da resposta
  check(response, {
    "Status é 200 - Subcategoria - Chace Desabilitado": (r) => r.status === 200,
    "Duração máxima - Subcategoria - Chace Desabilitado": (r) => r.timings.duration < 1000,
  });

  // Calcula o tempo de resposta
  let responseTime = response.timings.duration;

  // Salva as métricas para o relatório
  let metricsSubcategoriesDisabled = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); // Intervalo de espera entre as requisições
  return metricsSubcategoriesDisabled;
}

// Cenário  - Teste de cache habilitado
export function testCacheSubcategoriaEnabled() {
  // Realize uma solicitação para obter os dados com o cache desabilitado
  let response = http.get("https://localhost:7161/SubCategoria", {
  });

  // Verifica o status code da resposta
  check(response, {
    "Status é 200 - Subcategoria - Chace Habilitado": (r) => r.status === 200,
    "Duração máxima - Subcategoria - Chace Habilitado": (r) => r.timings.duration < 1000,
  });

  // Calcula o tempo de resposta
  let responseTime = response.timings.duration;

  // Salva as métricas para o relatório
  let metricsSubcategoriesEnabled = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); // Intervalo de espera entre as requisições
  return metricsSubcategoriesEnabled;
}