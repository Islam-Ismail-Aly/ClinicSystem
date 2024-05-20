document.getElementById('registerForm').addEventListener('submit', function (e) {
    e.preventDefault();
    register();
});

function register() {
    var firstName = document.getElementById('FirstName').value;
    var lastName = document.getElementById('LastName').value;
    var email = document.getElementById('Email').value;
    var password = document.getElementById('Password').value;
    var confirmPassword = document.getElementById('ConfirmPassword').value;
    var address = document.getElementById('Address').value;
    var phone = document.getElementById('Phone').value;
    var roles = document.getElementById('Roles').value;

    if (firstName === "" || lastName === "" || email === "" || password === "" || address === "") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'First Name, Last Name, Email, Password, and Address are required!',
        });
        return;
    }

    if (password !== confirmPassword) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Password and Confirm Password do not match!',
        });
        return;
    }

    if (roles === '') {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please select a valid role!',
        });
        return;
    }

    $.ajax({
        type: 'POST',
        url: '/Account/Register',
        headers: {
            RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() // Include anti-forgery token
        },
        data: {
            FirstName: firstName,
            LastName: lastName,
            Address: address,
            Email: email,
            Password: password,
            ConfirmPassword: confirmPassword,
            Phone: phone,
            Roles: roles
        },
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    icon: 'success',
                    title: 'Registration has been Successfully',
                    text: 'Please try login!',
                });
                window.location.href = '/Home/Index';
            } else {
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