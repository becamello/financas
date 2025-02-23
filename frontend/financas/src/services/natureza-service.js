import api from "./api";

function cadastrar(natureza) {
  return new Promise((resolve, reject) => {
    return api
      .post(`/naturezasdelancamento/`, natureza)
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}
function obterTodos() {
  return new Promise((resolve, reject) => {
    return api
      .get("/naturezasdelancamento")
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}
function obterPorId(id) {
  return new Promise((resolve, reject) => {
    console.log("Código da Natureza:", id);

    return api
      .get(`/naturezasdelancamento/${id}`)
      
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}
function atualizar(natureza) {
  return new Promise((resolve, reject) => {
    return api
      .put(`/naturezasdelancamento/${natureza.id}`, natureza) 
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}
function inativar(natureza) {
  return new Promise((resolve, reject) => {
    return api
      .delete(`/naturezasdelancamento/${natureza.id}`, natureza) 
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
