import {testPostCategoria} from "../scenarios/POST-Categories.js";
import {testPostSubcategoria} from "../scenarios/POST-Subcategories.js";
import {testPostCD} from "../scenarios/POST-CD.js";
import {testPostProduct} from "../scenarios/POST-Products.js";
import {testPostCart} from "../scenarios/POST-Cart.js";
import {check, group} from "k6";

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

  export function testPostCartScenario() {
    group("Teste POST do carrinho de compras", () => {
      let metricsPostCart = testPostCart();
      checkMetrics(metricsPostCart);
    });
  }
  
function checkMetrics(metrics) {
  check(metrics.min_response_time, (val) => val !== null);
  check(metrics.max_response_time, (val) => val !== null);
  check(metrics.avg_response_time, (val) => val !== null);
}

export default function () {
  testPostCategoriesScenario();
  testPostSubcategoriesScenario();
  testPostCDScenario();
  testPostProductsScenario();
  testPostCartScenario();
}