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

// Função para realizar o GET e obter os IDs das categorias cadastradas
export function testGetCategorias() {
  let response = http.get("https://localhost:7161/Categoria");

  check(response, {
    "Status é 200 - GET Categorias": (r) => r.status === 200,
    "Duração máxima - GET Categorias": (r) => r.timings.duration < 1000,
  });

  let responseJson = JSON.parse(response.body);
  return responseJson.map((categoria) => categoria.id);
}

// Função para realizar o POST com o ID da categoria obtido no GET
export function testPostSubcategoria() {
  let categoriaIDs = testGetCategorias();

  for (let categoriaID of categoriaIDs) {
    let subcategoryName = generateRandomName(); // Gerar um nome aleatório
    let requestBody = {
      "Nome": subcategoryName,
      "CategoriaID": categoriaID,
    };

    let headers = {
      "Content-Type": "application/json",
    };

    let response = http.post("https://localhost:7161/Subcategoria", JSON.stringify(requestBody), { headers: headers });

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

    sleep(1); // Intervalo de espera entre as requisições
    return metricsPostSubcategoria;
  }
}