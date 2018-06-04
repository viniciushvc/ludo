let dado;

let primeraVez = true;

/**
 * Previnir F5 na página
 */
 $(window).bind('beforeunload', function () {
     return 'A partida será resetada';
 });

/**
 * Inicia jogo
 */
$('#start').click(function () {

    $.ajax({
        url: 'Home/TotalJogadores',
        type: "POST",
        data: { total: $('#total').val() },
        success: function (data) {

            if (data) {

                $('.boas-vindas').fadeOut('fast', function () {
                    $('.ludo').slideDown('fast');
                });
            }
        }
    });
});

function JogarDado() {

    $.ajax({
        url: 'Home/JogarDado',
        type: "GET",
        data: { primeiraVez: primeraVez },
        success: function (data) {

            primeraVez = false;

            $('.dado-numero').text(data);

            dado = data;

            if (data == 6 || data == 1) {
                $('.btn-info').fadeIn();
                $('.jogar-dado').attr('disabled', true);
            }

            $('#peca').focus();

            PossuiPeca();

            VezJogador();

            PecasJogador();
        }
    });
}

function MoverPeca() {

    if ($('#peca').val() != '') {

        $.ajax({
            url: 'Home/MoverPeca',
            type: "POST",
            data: { dado: dado, peca: $('#peca').val() },
            success: function (data) {

                $.each(data, function (index, val) {

                    if (index > 55)
                        $("#" + index).text('');

                    if (index < 56) {
                        $("#" + index).removeClass('verde amarelo azul vermelho').addClass('preto').text('');

                        if (val.pecas[0] !== undefined) {
                            num = val.pecas[0].numeroPeca + '-' + val.pecas.length;

                            $("#" + index).text(num);

                            if (val.pecas[0].jogador === 0)
                                $("#" + index).addClass('verde');

                            if (val.pecas[0].jogador === 1)
                                $("#" + index).addClass('amarelo');

                            if (val.pecas[0].jogador === 2)
                                $("#" + index).addClass('azul');

                            if (val.pecas[0].jogador === 3)
                                $("#" + index).addClass('vermelho');
                        }
                    }

                    else if (index > 55 && index < 62) {
                        if (val.pecas.length > 0) {
                            num = val.pecas[0].numeroPeca + '-' + val.pecas.length;
                            $("#" + index).text(num);
                        }
                    }

                    else if (index > 61 && index < 68) {
                        if (val.pecas.length > 0) {
                            num = val.pecas[0].numeroPeca + '-' + val.pecas.length;
                            $("#" + index).text(num);
                        }
                    }

                    else if (index > 67 && index < 74) {
                        if (val.pecas.length > 0) {
                            num = val.pecas[0].numeroPeca + '-' + val.pecas.length;
                            $("#" + index).text(num);
                        }
                    }

                    else if (index > 75 && index < 82) {
                        if (val.pecas.length > 0) {
                            num = val.pecas[0].numeroPeca + '-' + val.pecas.length;
                            $("#" + index).text(num);
                        }
                    }
                });

                VerificaGanhou();

                $('.btn-info').fadeOut();

                $('.btn-primary').fadeOut();

                $('#peca').val('').fadeOut();

                $('.jogar-dado').attr('disabled', false);
            }
        });
    } else
        alert('Informe a peça');
}

function RetirarPeca() {

    $.ajax({
        url: 'Home/RetirarPeca',
        type: "POST",
        data: { dado: dado },
        success: function (data) {

            $.each(data, function (index, val) {

                if (index < 56) {
                    $("#" + parseInt(index)).removeClass('verde amarelo azul vermelho').addClass('preto').text('');

                    if (val.pecas[0] !== undefined) {
                        num = val.pecas[0].numeroPeca + '-' + val.pecas.length;

                        $("#" + index).text(num);

                        if (val.pecas[0].jogador === 0)
                            $("#" + index).addClass('verde');

                        if (val.pecas[0].jogador === 1)
                            $("#" + index).addClass('amarelo');

                        if (val.pecas[0].jogador === 2)
                            $("#" + index).addClass('azul');

                        if (val.pecas[0].jogador === 3)
                            $("#" + index).addClass('vermelho');
                    }
                }

            });

            VerificaGanhou();

            $('.btn-info').fadeOut();

            $('.btn-primary').fadeOut();

            $('#peca').val('').fadeOut();

            $('.jogar-dado').attr('disabled', false);
        }
    });
}

function PossuiPeca() {

    $.ajax({
        url: 'Home/PossuiPeca',
        type: "GET",
        success: function (data) {

            if (data) {
                $('.jogar-dado').attr('disabled', true);
                $('.btn-primary').fadeIn();
                $('#peca').fadeIn();
            }

            else
                $('.jogar-dado').attr('disabled', false);

            return data;
        }
    });
}

function VerificaGanhou() {

    $.ajax({
        url: 'Home/VerificaGanhou',
        type: "GET",
        success: function (data) {

            if (data != null)
                alert(data);
        }
    });
}

function VezJogador() {

    $.ajax({
        url: 'Home/VezJogador',
        type: "GET",
        success: function (data) {

            $('.vez-jogador').text(data);
        }
    });
}

function PecasJogador() {

    $.ajax({
        url: 'Home/PecasJogador',
        type: "GET",
        success: function (data) {

            if (data == 4)
                $('.btn-info').fadeOut();
        }
    });
}