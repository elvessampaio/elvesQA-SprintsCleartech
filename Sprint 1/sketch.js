// variaveis das posições do botão
let xCaixinha = 350;
let yCaixinha = 375;
let xAbrir = 380; 
let yAbrir = 400; 

//variavies das tabelas
let posicaoMultiplicacao = 20;
let posicaoAdicao = 200;
let posicaoSubtracao = 400;
let posicaoDivisao = 600;
    
    
function setup() {
  somDaTrilha.loop();
  createCanvas(850, 400);
 
  
  // campo de digitar o número
  input = createInput();
  
  //verificação do input
  input.input(checkInput);
  
  input.position(xCaixinha, yCaixinha);
  
  // botão de abrir a tabuada
  let button = createButton('Abrir a tabuada');
  button.position(xAbrir, yAbrir);
  button.mousePressed(gerarTabuada);
}

//verificaçãop se o número é inteiro
function checkInput(){
  let value = input.value();
  if (value !== "" && !Number.isInteger(Number(value))){somErro.play();
    alert("Insira apenas números, que sejam inteiros de 1 ao 10");
   console.log(value);
// limpar input se não for número intero
    input.value("");
  }
}

function gerarTabuada() {
  
  //verificação do número digitado
  let numero = parseInt(document.querySelector('input').value);
                 
  //condições do "Se"
  if (numero <= 0 || numero >= 11 ){somErro.play();
    alert("Fui programado apenas para a tabuada do 1 ao 10");
  input.value("");
  }else
   
  {
    // limpar tabuada ao pesquisar
    clear();
    somDaTabuada.play();
    
    // gera a tabuada de 1 a 10 para o número digitado
    for (let i = 1; i <= 10; i++) {{
      let resultado = numero * i;
      textSize(20);
      fill(254, 41, 120);
      text(numero + " x " + i + " = " + resultado, posicaoMultiplicacao, i * 28);{
      let resultado = numero + i;
      textSize(20);
      fill(254, 41, 120);
      text(numero + " + " + i + " = " + resultado, posicaoAdicao, i * 28);
        {
      let resultado = numero - i;
      textSize(20);
      fill(254, 41, 120);
      text(numero + " - " + i + " = " + resultado, posicaoSubtracao, i * 28);{
      let resultado = numero / i;
      textSize(20);
      fill(254, 41, 120);
      text(numero + " ÷ " + i + " = " + resultado, posicaoDivisao, i * 28)
      }}}}}}
  
}





alert("Tabuada criada por Elves Sampaio Moura - ClearTech 2023")



















