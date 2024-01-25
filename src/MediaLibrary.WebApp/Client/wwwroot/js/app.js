document.addEventListener('DOMContentLoaded', function () {
    loadCountries(); // Cargar países cuando el documento esté listo
    document.getElementById('registerForm').addEventListener('submit', registerUser);
});

function loadCountries() {
    // Aquí deberías cargar los países desde tu API y actualizar el select de países
    fetch('/api/Countries/combo')
        .then(response => response.json())
        .then(data => {
            const countrySelect = document.getElementById('country');
            data.forEach(country => {
                const option = document.createElement('option');
                option.value = country.id;
                option.textContent = country.name;
                countrySelect.appendChild(option);
            });
        })
        .catch(error => console.error('Error al cargar países:', error));
}

function registerUser(event) {
    event.preventDefault();

    // Crear un objeto FormData para enviar el archivo de la foto y los demás datos del formulario
    const formData = new FormData(event.target);

    // Imprimir cada par clave/valor para depurar
    for (let [key, value] of formData.entries()) {
        console.log(key, value);
    }
    const password = formData.get("password")
    const passwordConfim = formData.get("passwordConfirm")
    if (password !== passwordConfim) {
        alert("Las contraseñas no coinciden")
        return;
    }
    let userObject = {};
    for (let [key, value] of formData.entries()) {
        if (key !== 'photo' && key !== 'passwordConfirm') {
            if (key === 'name') {
                userObject['FirstName'] = value; // Asegurar de que los nombres de las propiedades coincidan con tu backend
            } else if (key === 'email') {
                userObject['Email'] = value;
                userObject['UserName'] = value; // Si UserName es el Email
                userObject['FullName'] = value; // Si UserName es el Email
            } else if (key === 'country') {
                userObject['CountryId'] = parseInt(value); // Convertir a número
            } else if (key === 'password') {
                userObject['PasswordConfirm'] = value; // Convertir a número
                userObject['Password'] = value;
                userObject['PasswordHash'] = null; 
            } else {
                userObject[key] = value; // Para los demás campos que coinciden directamente
            }
        }
    }

    // Establecer UserType. El Usuario siempre será 1, que es User
    userObject['UserType'] = 1; 
    userObject['AccessFailedCount'] = 0; 
    userObject['Country'] = null; 
    userObject['EmailConfirmed'] = false; 
    userObject['LockoutEnd'] = null; 
    userObject['NormalizedUserName'] = null; 
    userObject['NormalizedEmail'] = null; 
    userObject['PhoneNumber'] = null; 
    userObject['PhoneNumberConfirmed'] = false; 
    userObject['TwoFactorEnabled'] = false; 
    userObject['Photo'] = null; 

    if (formData.get('photo').size > 0) {
        const reader = new FileReader();
        reader.readAsDataURL(formData.get('photo'));
        reader.onloadend = function () {
            const base64String = reader.result
                .replace('data:', '')
                .replace(/^.+,/, '');

            userObject.photo = base64String;

            // Enviar la solicitud aquí para asegurarse de que se envía después de que la foto esté lista
            sendUserRegistration(userObject);
        }
    } else {
        // Enviar sin foto
        sendUserRegistration(userObject);
    }
}

function sendUserRegistration(userData) {
    fetch('/api/Accounts/CreateUser', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(userData),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al registrar el usuario');
            }
            return response.json();
        })
        .then(data => {
            console.log('Usuario registrado:', data);
            // Aquí redirigir al usuario o limpiar el formulario
            // Limpia el formulario
            resetForm();
            window.location.href = '/'; 
        })
        .catch(error => console.error('Error al registrar:', error));
}


function cancelRegistration() {
    // Lógica para manejar la cancelación del registro
    resetForm();
    window.location.href = '/'; // Redirige al usuario a la página principal
}


function previewFile() {
    const preview = document.getElementById('photo-preview');
    const file = document.querySelector('input[type=file]').files[0];
    const reader = new FileReader();

    reader.addEventListener("load", function () {
        // convertir imagen file a cadena base64 y mostrarla
        preview.src = reader.result;
        preview.style.display = 'block'; // mostrar la imagen
    }, false);

    if (file) {
        reader.readAsDataURL(file); // leer el archivo y enviarlo a onload
    }
}


function resetForm() {
    // Resetear el formulario
    document.getElementById('registerForm').reset();

    // Ocultar la vista previa de la imagen y restablecer el src
    const photoPreview = document.getElementById('photo-preview');
    photoPreview.style.display = 'none';
    photoPreview.src = '';

    const name = document.getElementById('name');
    name.style.display=""
    const email = document.getElementById('email');
    email.style.display = ""
    const password = document.getElementById('password');
    password.style.display = ""
}
