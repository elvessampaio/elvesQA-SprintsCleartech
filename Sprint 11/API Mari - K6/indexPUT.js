import {testPutCategoria} from "./scenarios/PUT-Categories.js";
import {testPutSubcategoria} from "./scenarios/PUT-Subcategories.js";
import {testPutProduto} from "./scenarios/PUT-Product.js";
import {testPutCD} from "./scenarios/PUT-CD.js";
import {check, group} from "k6";

// Cenário 1 - Teste
export function testPutCategoriesScenario() {
  group("Teste PUT das categorias", () => {
    let metricsPutCategoria = testPutCategoria();
    checkMetrics(metricsPutCategoria);
  });
}

export function testPutSubcategoriesScenario() {
    group("Teste PUT das subcategorias", () => {
      let metricsPutSubcategoria = testPutSubcategoria();
      checkMetrics(metricsPutSubcategoria);
    });
  }

  export function testPutProductsScenario() {
    group("Teste PUT dos Produtos", () => {
      let metricsPutProduto = testPutProduto();
      checkMetrics(metricsPutProduto);
    });
  }

  export function testPutCDScenario() {
    group("Teste PUT dos CD", () => {
      let metricsPutCD = testPutCD();
      checkMetrics(metricsPutCD);
    });
  }

function checkMetrics(metrics) {
  // Realize aqui as verificações adicionais dos resultados, se necessário
  check(metrics.min_response_time, (val) => val !== null);
  check(metrics.max_response_time, (val) => val !== null);
  check(metrics.avg_response_time, (val) => val !== null);
}

// Executa os cenários de teste
export default function () {
    //testPutCategoriesScenario();
    //testPutSubcategoriesScenario();
    //testPutProductsScenario();
    testPutCDScenario();
}

