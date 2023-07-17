const calculaComissaoVenda = require('.')

it('Se a lista estiver vazia, a comissão é zero', () => {
    const resultadoAtual = calculaComissaoVenda([])
    const resultadoEsperado = 0

    expect(resultadoAtual).toBe(resultadoEsperado)
})

it('Calcula comissão quando só tem 1 item na lista', () => {
    const resultadoAtual = calculaComissaoVenda([{
        id: 'PROD-123',
        precoUnitario: 1000,
        quantidadeVendida: 1
    }])
    const resultadoEsperado = 50

    expect(resultadoAtual).toBe(resultadoEsperado)
})

it('Calcula comissão quando há mais de um item na lista', () => {
    const resultadoAtual = calculaComissaoVenda ([
        {
            id:'PROD-123',
            precoUnitario: 1000,
            quantidadeVendida:1
        },
        {
            id:'PROD-456',
            precoUnitario: 100,
            quantidadeVendida: 5
        }
    ])
    const resultadoEsperado = 75

    expect(resultadoAtual).toBe(resultadoEsperado)
})

it ('Calcula comissão de 10%', () => {
    const resultadoAtual = calculaComissaoVenda ([{
        id: 'PROD-789',
        precoUnitario: 2000,
        quantidadeVendida: 1
    }])
    const resultadoEsperado = 200
    
    expect(resultadoAtual).toBe(resultadoEsperado)
})

it ('Calcula comissão de 15%', () => {
    const resultadoAtual = calculaComissaoVenda ([{
        id: 'PROD-789',
        precoUnitario: 2000,
        quantidadeVendida: 3
    }])
    const resultadoEsperado = 900
    
    expect(resultadoAtual).toBe(resultadoEsperado)
})


it ('Calcula comissão especial', () =>{
    const resultadoAtual = calculaComissaoVenda( [
        {
            id: 'PROD-321',
            precoUnitario: 5000,
            quantidadeVendida: 2
        },
        {
            id: 'XP-0101',
            precoUnitario: 10000,
            quantidadeVendida: 4
        }
    ])
    const resultadoEsperado = 10000

    expect(resultadoAtual).toBe(resultadoEsperado)
})


// Testes extras 

it ('Calcula comissão anual de 1% (Extra)', () => {
    const resultadoAtual = calculaComissaoVenda ([{
        precoUnitario: 10000,
        quantidadeVendida: 2
    }])
    const resultadoEsperado = 200
    
    expect(resultadoAtual).toBe(resultadoEsperado)
})

it ('Calcula comissão Anual de 2% (Extra)', () => {
    const resultadoAtual = calculaComissaoVenda ([{
        precoUnitario: 10000,
        quantidadeVendida: 5
    }])
    const resultadoEsperado = 1000
    
    expect(resultadoAtual).toBe(resultadoEsperado)
})

    it ('Ganhou almoço ou jantar (Extra)', () => {
      const items = [
        {
          id: 'PROD-123',
          precoUnitario: 1000,
          quantidadeVendida: 10,
        },
        {
          id: 'PROD-456',
          precoUnitario: 2000,
          quantidadeVendida: 5,
        },
      ];
  
      const totalSales = 15000;
  
      const resultadoAtual = calculaComissaoVenda(items, totalSales);
      const resultadoEsperado = 'Ganha almoço ou jantar';
  
      expect(resultadoAtual).toBe(resultadoEsperado);
    });