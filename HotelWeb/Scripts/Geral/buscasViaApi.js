    $(document).ready(function () {
        gerarDropDown();
    $('#HotelId').on('change', function () {
        gerarDropDown();
    });
});
        function gerarDropDown() {
            var hotelId = $('#HotelId option:selected').val();
            $.getJSON("/api2/HotelAPI/ObterQuartos?hotelId=" + hotelId, function (data) {
                if (data.length > 0) {
        $('#QuartoId').empty();
    $('#QuartoId').append('<option value="Selecione um quarto...">Selecione um quarto...</option>');
                    $.each(data, function (key, val) {
        $('#QuartoId').append('<option value="' + val.QuartoId + '">' + val.Titulo + '</option>');
    });
                } else {
        $('#QuartoId').empty();
    $('#QuartoId').append('<option>Sem quartos!</option>');
}
});
}

        function BuscarTurista() {
        $("#TuristaId").val('');
    let cpf = $("#TuristaCpf").val();
    $.getJSON("/api2/HotelAPI/BuscarTurista?turistaCpf=" + cpf,
                function (resp) {
                    if (resp) {
        $("#TuristaId").val(resp.TuristaId);
    swal({
        type: 'success',
    title: 'Turista Encontrado!',
    text: 'Nome: ' + resp.Nome,
    showConfirmButton: true,
                            footer: '<a href="/Turista/Detalhes/' + resp.TuristaId + '" target="_blank">Ver Detalhes</a>',
})
                    } else {
        swal({
            type: 'error',
            title: 'Nenhum turista encontrado com o CPF: ' + cpf,
            showConfirmButton: true,
            footer: '<a href="/Turista/CriarNovo/" target="_blank">Criar Novo</a>'
        })
    }
    });
}