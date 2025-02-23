import conversorData from "@/utils/conversorData";

export default class Titulo {
  constructor(obj = {}) {
    this.id = obj.id;
    this.idUsuario = obj.idUsuario;
    this.naturezaDeLancamentoDescricao = obj.naturezaDeLancamentoDescricao;
    this.descricao = obj.descricao;
    this.tipoTitulo = obj.tipoTitulo;
    this.dataCadastro = conversorData.aplicarMascaraEmDataIso(obj.dataCadastro);
    this.dataInativacao = obj.dataInativacao;
    this.observacao = obj.observacao;
    this.valorOriginal = obj.valorOriginal;
    this.dataPagamento = conversorData.aplicarMascaraEmDataIso(obj.dataPagamento);
  }

  static tipoTituloMap = {
    0: "Gasto",
    1: "Recebimento",
  };

  get tipoTituloDescricao() {
    return Titulo.tipoTituloMap[this.tipoTitulo] || "Desconhecido";
  }
}
