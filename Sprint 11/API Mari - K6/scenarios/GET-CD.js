import http from "k6/http";
import { check, sleep } from "k6";

// Cenário 1 - Teste das categorias
export function testCD() {
  let response = http.get("https://localhost:7161/CentroDistribuicao/1");

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
export function testPageLoadTimeCD() {
  let pageUrls = [
    "https://localhost:7161/CentroDistribuicao/1"
   ];

  let randomPageUrl = pageUrls[Math.floor(Math.random() * pageUrls.length)];
  let response = http.get(randomPageUrl);

  check(response, {
    "Status é 200 - Centro de Distribuição": (r) => r.status === 200,
    "Duração máxima - Centro de Distribuição": (r) => r.timings.duration < 1000,
  });

  // Calcula o tempo de resposta
  let responseTime = response.timings.duration;

  // Salva as métricas para o relatório
  let metricsCD = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(2);
  return metricsCD;
}

// Cenário 3 - Teste de cache desabilitado
export function testCacheCDDisabled() {
  // Realize uma solicitação para obter os dados com o cache desabilitado
  let response = http.get("https://localhost:7161/CentroDistribuicao/1", {
    headers: {
      "Cache-Control": "no-cache",
    },
  });

  // Verifica o status code da resposta
  check(response, {
    "Status é 200 - Centro de Distribuição - Chace Desabilitado": (r) => r.status === 200,
    "Duração máxima - Centro de Distribuição - Chace Desabilitado": (r) => r.timings.duration < 1000,
  });

  // Calcula o tempo de resposta
  let responseTime = response.timings.duration;

  // Salva as métricas para o relatório
  let metricsCDDisabled = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); // Intervalo de espera entre as requisições
  return metricsCDDisabled;
}

// Cenário  - Teste de cache habilitado
export function testCacheCDEnabled() {
  // Realize uma solicitação para obter os dados com o cache desabilitado
  let response = http.get("https://localhost:7161/CentroDistribuicao/1", {
  });

  // Verifica o status code da resposta
  check(response, {
    "Status é 200 - Centro de Distribuição - Chace Habilitado": (r) => r.status === 200,
    "Duração máxima - Centro de Distribuição - Chace Habilitado": (r) => r.timings.duration < 1000,
  });

  // Calcula o tempo de resposta
  let responseTime = response.timings.duration;

  // Salva as métricas para o relatório
  let metricsCDEnabled = {
    min_response_time: responseTime,
    max_response_time: responseTime,
    avg_response_time: responseTime,
  };

  sleep(1); // Intervalo de espera entre as requisições
  return metricsCDEnabled;
}