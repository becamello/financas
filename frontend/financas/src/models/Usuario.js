export default class Usuario {
  constructor(obj = {}) {
    this.id = obj.id;
    this.email = obj.email;
    this.senha = obj.senha;
    this.dataInativacao = obj.dataInativacao;
    this.token = obj.token;
    this.role = obj.role;
  }

  static roleMap = {
    0: "Admin",
    1: "User",
  };

  get roleDescricao() {
    return Usuario.roleMap[this.role] || "Desconhecido";
  }

  modeloValidoParaCadastro() {
    return !!(this.email && this.senha);
  }

  modeloValidoParaAtualizar() {
    return !!(this.email && this.senha && this.role);
  }
}
