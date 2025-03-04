
export default class Usuario {
  constructor(obj = {}) {
    this.id = obj.id;
    this.email = obj.email;
    this.senha = obj.senha;
    this.token = obj.token;
    this.role = obj.role;
  }
}