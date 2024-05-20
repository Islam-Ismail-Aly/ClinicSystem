document.getElementById('loginForm').addEventListener('submit', function (e) {
    e.preventDefault();
    login();
});

function login() {
    var email = document.getElementById('Email').value;
    var password = document.getElementById('Password').value;
    var rememberMe = document.getElementById('RememberMe').checked;

    if (email === "" || password === "") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Email and password are required!',
        });
        return;
    }

    event.preventDefault();

    $.ajax({
        type: 'POST',
        url: '/Account/Login',
        headers: {
            RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() // Include anti-forgery token
        },
        data: {
            Email: email,
            Password: password,
            RememberMe: rememberMe
        },
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Login Successfully',
                    text: 'Welcome to Clinic System.',
                });
                window.location.href = '/Home/Index';
            } else {
                // If login fails, show error message
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: response.message,
                });
            }
        },
        error: function (xhr, status, error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'An error occurred while processing your request.',
            });
        }
    });
}