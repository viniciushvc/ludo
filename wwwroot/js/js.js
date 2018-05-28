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
        url: 'Home/TotalJogador',
        type: "POST",
        data: { total: $('#total').val() },
        success: function (data) {

            $('.boas-vindas').fadeOut('fast', function () {
                $('.ludo').slideDown('fast');
            });
        }
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
        }
    });
});

$('#add').click(function () {

    $.ajax({
        url: 'Home/TotalJogadores',
        type: "POST",
        success: function (data) {

            $('#total').val(data);
        }
    });
});