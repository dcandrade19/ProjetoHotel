var inputOcupantes = $('#Ocupantes');
var inputCrianca = $('#Criancas');
var inputDiaria = $('#ValorDiaria');
$(document).ready(function () {
    var dropDownQuartos = $('#QuartoId');

    gerarInputs($('#QuartoId option:selected').val());

    $('#Chegada').on('change', function () {
        var dataMin = $('#Chegada').val();
        $('#Partida').attr('min', dataMin);
    });

    $('#HotelId').on('change', function () {
        gerarInputs($('#QuartoId option:selected').val());
        gerarValorDiaria();
    });

    dropDownQuartos.on('change', function () {
        gerarInputs($('#QuartoId option:selected').val());
        gerarValorDiaria();
    });

    $("#Ocupantes").on('mouseup keyup', function () {
        gerarValorDiaria();
    });

    $("#Criancas").on('change', function () {
        gerarValorDiaria();
    });

    gerarValorDiaria();
});

function gerarInputs(id) {
    if (id > 0) {
        inputOcupantes.val(1);
        $.getJSON('/Reserva/GetOcupantes', { id }, function (result) {
            inputOcupantes.attr('max', result);
        });
    }  
}

function gerarValorDiaria() {
    var id = $('#QuartoId option:selected').val();
    if (id > 0) {
        var qtdOcupantes = inputOcupantes.val();
        var crianca = $('#Criancas option:selected').val();
        $.getJSON('/Reserva/GerarDiaria', { id, qtdOcupantes, crianca }, function (result) {
            inputDiaria.val(result.toString().replace('.', ','));
        });
    } else {
       inputDiaria.val('0,00');
    }
}