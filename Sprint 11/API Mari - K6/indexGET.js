import {testCategories} from "./scenarios/GET-Categories.js";
import {testSubcategories} from "./scenarios/GET-Subcategorias.js";
import {testProducts} from "./scenarios/GET-Products.js";
import {testCD} from "./scenarios/GET-CD.js";
import {testPageLoadTimeCategories} from "./scenarios/GET-Categories.js";
import {testPageLoadTimeSubcategories} from "./scenarios/GET-Subcategorias.js";
import {testPageLoadTimeProducts} from "./scenarios/GET-Products.js";
import {testPageLoadTimeCD} from "./scenarios/GET-CD.js";
import {testCacheCategoriaDisabled, testCacheCategoriaEnabled} from "./scenarios/GET-Categories.js";
import {testCacheSubcategoriaDisabled, testCacheSubcategoriaEnabled} from "./scenarios/GET-Subcategorias.js";
import {testCacheProductsDisabled, testCacheProductsEnabled} from "./scenarios/GET-Products.js";
import {testCacheCDDisabled, testCacheCDEnabled} from "./scenarios/GET-CD.js";
import {check, group} from "k6";

// Cenário 1 - Teste
export function testCategoriesScenario() {
  group("Teste consulta das categorias", () => {
    let metrics = testCategories();
    checkMetrics(metrics);
  });
}

export function testSubcategoriesScenario() {
  group("Teste consulta das subcategorias", () => {
    let metrics = testSubcategories();
    checkMetrics(metrics);
  });
}

export function testProductsScenario() {
  group("Teste consulta produtos", () => {
    let metrics = testProducts();
    checkMetrics(metrics);
  });
}

export function testCDScenario() {
  group("Teste consulta Centro de Distribuições", () => {
    let metrics = testCD();
    checkMetrics(metrics);
  });
}

// Cenário 2 - Teste do tempo de carregamento das páginas
export function testPageLoadTimeScenario() {
  group("Teste do tempo de carregamento das páginas", () => {
    let metricsCategories = testPageLoadTimeCategories();
    let metricsSubcategories = testPageLoadTimeSubcategories();
    let metricsProducts = testPageLoadTimeProducts();
    let metricsCD = testPageLoadTimeCD();
    checkMetrics(metricsCategories, metricsSubcategories, metricsProducts, metricsCD);
  });
}

// Cenário 3 - Teste de cache
export function testCacheScenario() {
  group("Teste de cache", () => {
    let metricsCategoriesCacheDisabled = testCacheCategoriaDisabled();
    let metricsCategoriesCacheEnabled = testCacheCategoriaEnabled();
    let metricsSubcategoriesDisabled = testCacheSubcategoriaDisabled();
    let metricsSubcategoriesEnabled = testCacheSubcategoriaEnabled();
    let metricsProductsDisabled = testCacheProductsDisabled();
    let metricsProductsEnabled = testCacheProductsEnabled();
    let metricsCDDisabled = testCacheCDDisabled();
    let metricsCDEnabled = testCacheCDEnabled();
    checkMetrics(metricsCategoriesCacheDisabled,metricsCategoriesCacheEnabled, metricsSubcategoriesDisabled, 
      metricsSubcategoriesEnabled, metricsProductsDisabled, metricsProductsEnabled, metricsCDDisabled, metricsCDEnabled);
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
  testCategoriesScenario();
  testSubcategoriesScenario();
  testProductsScenario();
  testCDScenario();
  testPageLoadTimeScenario();
  testCacheScenario();
}