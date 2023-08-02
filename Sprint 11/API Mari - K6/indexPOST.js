import {testPostCategoria} from "./scenarios/POST-Categories.js";
import {testPostSubcategoria} from "./scenarios/POST-Subcategories.js";
import {testPostCD} from "./scenarios/POST-CD.js";
import {testPostProduct} from "./scenarios/POST-Produtos.js";
import {check, group} from "k6";

// Cenário 1 - Teste
export function testPostCategoriesScenario() {
  group("Teste POST das categorias", () => {
    let metricsPostCategoria = testPostCategoria();
    checkMetrics(metricsPostCategoria);
  });
}

export function testPostSubcategoriesScenario() {
    group("Teste POST das subcategorias", () => {
      let metricsPostSubcategoria = testPostSubcategoria();
      checkMetrics(metricsPostSubcategoria);
    });
  }

  export function testPostCDScenario() {
    group("Teste POST dos CD", () => {
      let metricsPostCD = testPostCD();
      checkMetrics(metricsPostCD);
    });
  }

  export function testPostProductsScenario() {
    group("Teste POST dos Produtos", () => {
      let metricsPostProdutos = testPostProduct();
      checkMetrics(metricsPostProdutos);
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
  testPostCategoriesScenario();
  testPostSubcategoriesScenario();
  testPostCDScenario();
  testPostProductsScenario();
}