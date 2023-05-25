const body = document.querySelector('body');
const account = document.querySelector('.account');
const accountSelection = document.querySelector('.account-selection');

body.addEventListener('click', (event) => {
    if (!account.contains(event.target)) {
        account.classList.remove('active');
    }
});

account.addEventListener('click', (event) => {
    event.stopPropagation();
    account.classList.toggle('active');
});