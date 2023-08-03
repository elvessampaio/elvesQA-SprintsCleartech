import {testDelCategoria} from "./scenarios/DEL-Categories.js";
import {testDelSubcategoria} from "./scenarios/DEL-Subcategories.js";
import {testDelProduto} from "./scenarios/DEL-Products.js";
import {testDelCentroDistribuicao} from "./scenarios/DEL-CD.js";
import {check, group} from "k6";


// Cenário 1 - Teste
export function testDelCategoriesScenario() {
  group("Teste DEL das categorias", () => {
    let metricsDelCategoria = testDelCategoria();
    checkMetrics(metricsDelCategoria);
  });
}

export function testDelSubcategoriesScenario() {
    group("Teste DEL das subcategorias", () => {
      let metricsDelSubcategoria = testDelSubcategoria();
      checkMetrics(metricsDelSubcategoria);
    });
  }

  export function testDelProductsScenario() {
    group("Teste DEL das produtos", () => {
      let metricsDelProduto = testDelProduto();
      checkMetrics(metricsDelProduto);
    });
  }

  export function testDelCDScenario() {
    group("Teste DEL Centro de Distribuição", () => {
      let metricsDelCD = testDelCentroDistribuicao();
      checkMetrics(metricsDelCD);
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
  testDelCategoriesScenario();
  testDelSubcategoriesScenario();
  testDelProductsScenario();
  testDelCDScenario();
}

