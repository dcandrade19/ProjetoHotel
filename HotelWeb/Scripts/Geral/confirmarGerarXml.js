$(document).ready(function () {
    var nomeArquivo = '';
    var testeNome = /^[\w\- ]+$/;
    $('.baixar-xml').on('click', function () {
        swal({
            title: 'Informe o nome do arquivo.',
            input: 'text',
            showCancelButton: true,
            confirmButtonColor: '#5cb85c',
            cancelButtonColor: '#d33',
            confirmButtonText: '<div class="glyphicon glyphicon-download-alt"></div> Baixar',
            cancelButtonText: 'Cancelar',
            inputValidator: (value) => {
                if (value) {
                    if (testeNome.test(value)) {
                        var file_path = controllerNome + '/ExportarExcel/?nome=' + value;
                        var a = document.createElement('A');
                        a.href = file_path;
                        a.download = file_path.substr(file_path.lastIndexOf('/') + 1);
                        document.body.appendChild(a);
                        a.click();
                        document.body.removeChild(a);
                        nomeArquivo = value;
                    } else {
                        return 'O nome do arquivo não é válido.'
                    }   
                } else {
                    return 'Informe o nome do arquivo.'
                }
            }
        }).then((result) => {
            if (result.value) {
                swal({
                    title: 'Arquivo Gerado!',
                    html: 'Se o download não começar ' +
                        '<a href="' + controllerNome + '/ExportarExcel/?nome=' + nomeArquivo + '">clique aqui</a>. ',
                    type:'success'
                })
            }
        })
    });
});