$(document).ready(function () {
    $('.cep').mask('00000-000', { reverse: true });
    $('.telefone').mask('0000-0000', { reverse: true });
    $('.uf').mask('SS');
    $('.ddd').mask('(00)');
    $('.data').mask('00/00/0000');
    $('.hora').mask('00:00:00');
    $('.cpf').mask('000.000.000-00', { reverse: true });
    $('.cnpj').mask('000.000.000/0000-00', { reverse: true });
    $('.moeda').mask('00.000,00', { reverse: true });
});