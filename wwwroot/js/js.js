function clickDado() {

    let randomNumber = Math.floor(Math.random() * 6) + 1;

    $('.dado-numero').text(randomNumber);
}