import moment from "moment";

function aplicarMascaraEmDataIso(data){
    if(data){
        return moment(data).locale('pt-br').format('DD/MM/YYYY');
    }
    return undefined;
}

function aplicarMascaraDataHoraEmDataIso(data){
    if(data){ 
        return moment(data).locale('pt-br').format('DD/MM/YYYY HH:mm:ss');
    }
    return undefined;
}

function formatarDataParaMesAno(data) {
    if (!data) return "";
  
    if (data.length === 7) {
      return moment(data, "YYYY-MM").format("MM/YYYY");
    }
  
    const dateObj = moment(data, "DD/MM/YYYY");
  
    if (!dateObj.isValid()) {
      console.log("Data inválida:", data);
      return "";
    }
  
    return dateObj.format("MM/YYYY");
  }

export default {
    aplicarMascaraEmDataIso,
    aplicarMascaraDataHoraEmDataIso,
    formatarDataParaMesAno
}