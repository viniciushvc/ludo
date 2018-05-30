let total;

/**
 * Previnir F5 na página
 */
// $(window).bind('beforeunload', function () {
//     return 'A partida será resetada';
// });

/**
 * Inicia jogo
 */
$('#start').click(function () {

    total = parseInt($('#total').val());

    //Validação no front-end
    if (total > 1 && total < 5) {
        $.ajax({
            url: 'Home/TotalJogadores',
            type: "POST",
            data: { total: total },
            success: function (data) {

                if (data) {

                    $('.boas-vindas').fadeOut('fast', function () {
                        $('.ludo').slideDown('fast');
                    });
                } 
            }
        });
    } else
        alert('min 2 - máx 4');
});

/**
 * Jogar o dado
 */
$('.jogar-dado').click(function () {

    $.ajax({
        url: 'Home/JogarDado',
        success: function (data) {

            $('.dado-numero').text(data);

            //Dispara quando cai 1 ou 6
            if (parseInt(data) === 1 || parseInt(data) === 6) {

                //Pergunta quer mover ou retirar peça
                if (RestaTorre(0)) {

                    //Mostra botões
                    $('.acao').fadeIn('fast', function () {

                        //Clicou em retirar
                        $('#retirar').click(function () {
                            RetirarPeca(0);
                        });

                        //Clicou em mover

                    });
                }
            }

            Jogada(data);
        }
    });
});

/**
 * Mover peça
 * @param {any} dado
 */
function Jogada(dado) {

    let peca = 0;

    $.ajax({
        url: 'Home/MoverPeca',
        type: "POST",
        data: { jogador: 0, dado: dado, peca: peca },
        success: function (data) {

            if (data !== 0)
                $('#' + data[peca].posicao).addClass('bg-success');
        }
    });
}

function RestaTorre(jogador) {

    let count = $('.valida-' + jogador).length;

    if (count > 1)
        return true;

    return false;
}

function RetirarPeca(jogador) {

    $.ajax({
        url: 'Home/RetirarPeca',
        type: "POST",
        data: { jogador: 0 },
        success: function (data) {

            let peca = $('#' + jogador + '-' + data);

            let clone = peca.text();

            peca.removeClass('valida-' + jogador + ' torre-' + jogador).text('').addClass('verde-claro');

            $('#1').addClass;
        }
    });
}