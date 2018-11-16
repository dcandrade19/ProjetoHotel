   function limparCamposEndereco() {
        $('#Endereco').val('');
        $('#Cidade').val('');
        $('#Bairro').val('');
        $('#Uf').val('');
        $('#Complemento').val('');
};

    function ObterEndereco() {
        var cep = $('#Cep').val().replace(/\D/g, '');
        var validacep = /^[0-9]{8}$/;
        if (validacep.test(cep)) {
            var url = '/api2/HotelAPI/ObterEndereco?cep=' + cep;
            $.getJSON(url, function (dados) {
                if (dados) {
                    $('#Endereco').val(dados.Logradouro);
                    $('#Cidade').val(dados.Cidade);
                    $('#Bairro').val(dados.Bairro);
                    $('#Uf').val(dados.Uf);
                } else {
                    limparCamposEndereco();
                }
            });
        } else {
            limparCamposEndereco();
        }
    };