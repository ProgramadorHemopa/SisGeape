const Utils = {
    somenteNumeros: function (e) {
        var charCode = e.charCode ? e.charCode : e.keyCode;
        if (!(Utils.temNumero(e.key) || charCode === 8 || charCode === 46 || charCode === 37 || charCode === 39)) {
            return false;
        }
    },
    temNumero: function (data) {
        let regexp = new RegExp(/[^\d]+/g);
        let NaoExisteNumero = regexp.test(data);
        if (!(!NaoExisteNumero || data === undefined)) { return false; } else { return true; }
    }
}