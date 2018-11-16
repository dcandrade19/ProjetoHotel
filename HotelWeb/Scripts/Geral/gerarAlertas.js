$(document).ready(function () {
    if (resultado == 'Erro') {
        swal({
            type: 'error',
            title: 'Não foi possivel realizar a operação!',
            showConfirmButton: true,
        })
    }
    if (resultado == 'Deletado') {
        swal(
            'Deletado!',
             controller + ' "' + nome + '" foi deletado com sucesso.',
            'success'
        )
    }
    if (resultado == 'Editado' || resultado == 'Salvo') {
        swal({
            type: 'success',
            title: controller + ' "' + nome + '" ' + resultado.toLowerCase() + ' com sucesso!',
            showConfirmButton: true,
            footer: '<a href="/' + controller + '/Detalhes/' + id + '">Ver Detalhes</a>',
        })
    }
});