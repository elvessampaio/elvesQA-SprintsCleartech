import GetCategoria from "./scenarios/Get-Categorias.js";
import GetCategoriaNome from "./scenarios/Get-CategoriaNome.js";
import { group, sleep } from "k6";
//import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

export function handleSummary(data) {
    return {
      "summary.html": htmlReport(data),
    };
  }

export default () => {
    group ('Todos as Categorias', () => {
        GetCategoria();
    });
    group ('Categorias por nome', () => {
        GetCategoriaNome();
    });

    sleep(1);
}