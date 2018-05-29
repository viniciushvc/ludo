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

    $.ajax({
        url: 'Home/TotalJogadores',
        type: "POST",
        data: { total: $('#total').val() }
    }).done(function () {
        $('.boas-vindas').fadeOut('fast', function () {
            $('.ludo').slideDown('fast');
        });
    });
});

/**
 * Rodar o dado
 */
$('.jogar-dado').click(function () {

    $.ajax({
        url: 'Home/JogarDado',
        success: function (data) {

            $('.dado-numero').text(data);

            Jogada(data);
        }
    });
});

function Jogada(dado) {

    let peca = 1;

    $.ajax({
        url: 'Home/Jogada',
        type: "POST",
        data: { jogador: 1, dado: dado, peca: peca },
        success: function (data) {

            if (data !== 0)
                $('#' + data[peca - 1].posicao).addClass('bg-success');
        }
    });
}