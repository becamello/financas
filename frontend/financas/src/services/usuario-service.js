import api from "./api";

function login(email, senha) {
  return new Promise((resolve, reject) => {
    return api
      .post(`/usuarios/login`, { email, senha })
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}

export default {
  login,
};
