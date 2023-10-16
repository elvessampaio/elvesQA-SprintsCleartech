import {testPutCategoria} from "../scenarios/PUT-Categories.js";
import {testPutSubcategoria} from "../scenarios/PUT-Subcategories.js";
import {testPutProduto} from "../scenarios/PUT-Product.js";
import {testPutCD} from "../scenarios/PUT-CD.js";
import {testPutCart} from "../PUT-Cart.js";
import {check, group} from "k6";
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js"
export function handleSummary(data) {
return {
 "summary.html": htmlReport(data),
};
}

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

  export function testPutCartScenario() {
    group("Teste PUT cart", () => {
      let metricsPutCart = testPutCart();
      checkMetrics(metricsPutCart);
    });
  }

function checkMetrics(metrics) {
  check(metrics.min_response_time, (val) => val !== null);
  check(metrics.max_response_time, (val) => val !== null);
  check(metrics.avg_response_time, (val) => val !== null);
}

// Executa os cenários de teste
export default function () {
    testPutCategoriesScenario();
    testPutSubcategoriesScenario();
    testPutProductsScenario();
    testPutCDScenario();
    //testPutCartScenario();
}

