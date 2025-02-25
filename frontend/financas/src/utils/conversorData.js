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
      return "";
    }
  
    return dateObj.format("MM/YYYY");
  }


  function aplicarMascaraIsoEmFormatoAmericano(data){
    if(data){ 
        let dataAmericana = moment(data).locale('pt-br').format('YYYY-MM-DD');
        return dataAmericana + "T00:00:00";
    }
    return undefined;
}
  function aplicarMascaraIsoEmFormatoAmericanoSemHorario(data){
    if(data){ 
        let dataAmericanaSemHorario = moment(data).locale('pt-br').format('YYYY-MM-DD');
        return dataAmericanaSemHorario;
    }
    return undefined;
}


export default {
    aplicarMascaraEmDataIso,
    aplicarMascaraDataHoraEmDataIso,
    formatarDataParaMesAno,
    aplicarMascaraIsoEmFormatoAmericano,
    aplicarMascaraIsoEmFormatoAmericanoSemHorario
}