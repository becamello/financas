
export default class NaturezaDeLancamento {
    constructor(obj = {}) {
      this.id = obj.id;
      this.idUsuario = obj.idUsuario;
      this.descricao = obj.descricao;
      this.observacao = obj.observacao;
      this.dataCadastro = obj.dataCadastro;
      this.dataInativacao = obj.dataInativacao;
    }


    modeloValidoParaCadastro() {
      return !!(this.descricao);
    }
  
    modeloValidoParaAtualizar() {
      return !!(this.descricao);
    }
}
  