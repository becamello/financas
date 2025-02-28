import conversorData from "@/utils/conversorData";

export default class Titulo {
  constructor(obj = {}) {
    this.id = obj.id;
    this.idUsuario = obj.idUsuario;
    this.idNaturezaDeLancamento = obj.idNaturezaDeLancamento;
    this.naturezaDeLancamentoDescricao = obj.naturezaDeLancamentoDescricao;
    this.descricao = obj.descricao;
    
    this.tipoTitulo = obj.tipoTitulo !== undefined && obj.tipoTitulo !== null ? obj.tipoTitulo : null; 
    
    this.dataCadastro = conversorData.aplicarMascaraEmDataIso(obj.dataCadastro);
    this.dataInativacao = obj.dataInativacao;
    this.observacao = obj.observacao;
    this.valorOriginal = obj.valorOriginal;
    this.dataPagamento = obj.dataPagamento;
  }

  static tipoTituloMap = {
    0: "Gasto",
    1: "Recebimento",
  };

  get tipoTituloDescricao() {
    return Titulo.tipoTituloMap[this.tipoTitulo] || "Desconhecido";
  }

  modeloValidoParaCadastro() {
    return !!(this.descricao && this.naturezaDeLancamentoDescricao && this.valorOriginal && this.dataPagamento && this.tipoTitulo !== undefined);
  }

  modeloValidoParaAtualizar() {
    return !!(this.descricao && this.naturezaDeLancamentoDescricao && this.valorOriginal && this.dataPagamento && this.tipoTitulo !== undefined);
  }
}
