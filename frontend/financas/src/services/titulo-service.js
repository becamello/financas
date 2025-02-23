import api from "./api";

function cadastrar(titulo) {
  return new Promise((resolve, reject) => {
    return api
      .post(`/titulos/`, titulo)
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}
function obterTodos() {
  return new Promise((resolve, reject) => {
    return api
      .get("/titulos")
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}
function obterPorId(id) {
  return new Promise((resolve, reject) => {
    console.log("Código do Titulo:", id);

    return api
      .get(`/titulos/${id}`)
      
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}
function atualizar(titulo) {
  return new Promise((resolve, reject) => {
    return api
      .put(`/titulos/${titulo.id}`, titulo) 
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}
function inativar(titulo) {
  return new Promise((resolve, reject) => {
    return api
      .delete(`/titulos/${titulo.id}`, titulo) 
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}

export default {
  obterTodos,
  obterPorId,
  cadastrar,
  atualizar,
  inativar
};
