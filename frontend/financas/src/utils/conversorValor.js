function aplicarMascaraParaReal(valor){
    if(isNaN(valor)){
        return 0;
    }
    return Number(valor).toLocaleString('pt-BR', { minimumFractionDigits: 2 });
}

function aplicarMascaraParaRealComPrefixo(valor){
    if(isNaN(valor)){
        return 0;
    }
    return Number(valor).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL'});
}

export function removerMascaraDeReal(valorFormatado) {
    if (!valorFormatado) return 0;
    return Number(valorFormatado.replace(/[^\d,.-]/g, "").replace(",", "."));
  }

export default {
    aplicarMascaraParaReal,
    aplicarMascaraParaRealComPrefixo,
    removerMascaraDeReal
}