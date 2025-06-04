import { checkToken } from './auth.js';
import { API_CONSTANTS } from './consts.js';

checkToken();

export async function loadUsers() {
    const token = localStorage.getItem('token');
    const msg = document.getElementById('usersMsg');
    msg.style.display = 'none';
    msg.textContent = '';

    try {
        const response = await fetch(
            `${API_CONSTANTS.baseUrl}${API_CONSTANTS.usersEndpoint}/get-users`,
            {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Accept': 'application/json'
                }
            }
        );

        const data = await response.json();

        if (!data.isSuccess) {
            throw new Error(data.message || 'No se pudieron cargar los usuarios');
        }

        const users = data.usersDTO || [];
        const tbody = document.querySelector('#usersTable tbody');
        tbody.innerHTML = '';

        users.forEach(user => {
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>${user.dni || ''}</td>
                <td>${user.username || ''}</td>
                <td>${user.surname || ''}</td>
                <td>${user.email || ''}</td>
                <td>${user.stringRole || ''}</td>
                <td>${user.inscriptionDate ? new Date(user.inscriptionDate).toLocaleDateString() : ''}</td>
            `;
            tbody.appendChild(tr);
        });
    } catch (error) {
        msg.style.display = 'block';
        msg.textContent = error.message;
    }
}

export async function createAdmin(e) {
    e.preventDefault();
    const msg = document.getElementById('adminMsg');
    msg.style.display = 'none';
    msg.textContent = '';
    msg.className = 'mt-3 text-center';

    const token = localStorage.getItem('token');
    const dni = document.getElementById('dni').value;
    const username = document.getElementById('username').value;
    const surname = document.getElementById('surname').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    try {
        const response = await fetch(
            `${API_CONSTANTS.baseUrl}${API_CONSTANTS.usersEndpoint}/create-admin`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    Dni: dni,
                    Username: username,
                    Surname: surname,
                    Email: email,
                    Password: password,
                    ConfirmPassword: confirmPassword
                })
            }
        );

        const data = await response.json();

        if (data.isSuccess) {
            msg.style.display = 'block';
            msg.classList.add('text-success');
            msg.textContent = data.message || 'Administrador creado correctamente.';
            e.target.reset();
        } else {
            throw new Error(data.message || 'No se pudo crear el administrador');
        }
    } catch (error) {
        msg.style.display = 'block';
        msg.classList.add('text-danger');
        msg.textContent = error.message;
    }
}

export async function deleteUser(e) {
    e.preventDefault();
    const msg = document.getElementById('deleteMsg');
    msg.style.display = 'none';
    msg.textContent = '';
    msg.className = 'mt-3 text-center';

    const token = localStorage.getItem('token');
    const email = document.getElementById('deleteEmail').value;

    try {
        const response = await fetch(
            `${API_CONSTANTS.baseUrl}${API_CONSTANTS.usersEndpoint}/delete-user`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                    'Accept': 'application/json'
                },
                body: JSON.stringify({ Email: email })
            }
        );

        const data = await response.json();

        msg.style.display = 'block';
        msg.textContent = data.message || '';
        if (data.isSuccess) {
            msg.classList.remove('text-danger');
            msg.classList.add('text-success');
            e.target.reset();
        } else {
            msg.classList.remove('text-success');
            msg.classList.add('text-danger');
        }
    } catch (error) {
        msg.style.display = 'block';
        msg.classList.remove('text-success');
        msg.classList.add('text-danger');
        msg.textContent = error.message;
    }
}

export async function createUser(e) {
    e.preventDefault();
    const msg = document.getElementById('userMsg');
    msg.style.display = 'none';
    msg.textContent = '';
    msg.className = 'mt-3 text-center';

    const token = localStorage.getItem('token');
    const dni = document.getElementById('dni').value;
    const username = document.getElementById('username').value;
    const surname = document.getElementById('surname').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    try {
        const response = await fetch(
            `${API_CONSTANTS.baseUrl}${API_CONSTANTS.usersEndpoint}/create-user`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    Dni: dni,
                    Username: username,
                    Surname: surname,
                    Email: email,
                    Password: password,
                    ConfirmPassword: confirmPassword
                })
            }
        );

        const data = await response.json();

        msg.style.display = 'block';
        msg.textContent = data.message || '';
        if (data.isSuccess) {
            msg.classList.remove('text-danger');
            msg.classList.add('text-success');
            e.target.reset();
        } else {
            msg.classList.remove('text-success');
            msg.classList.add('text-danger');
        }
    } catch (error) {
        msg.style.display = 'block';
        msg.classList.remove('text-success');
        msg.classList.add('text-danger');
        msg.textContent = error.message;
    }
}

export async function findUserByEmail(e) {
    e.preventDefault();
    const email = document.getElementById('findEmail').value;
    const msg = document.getElementById('findMsg');
    const updateForm = document.getElementById('updateUserForm');
    msg.style.display = 'none';
    msg.textContent = '';
    updateForm.style.display = 'none';

    const token = localStorage.getItem('token');

    try {
        const response = await fetch(
            `${API_CONSTANTS.baseUrl}${API_CONSTANTS.usersEndpoint}/get-user-by-email`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                    'Accept': 'application/json'
                },
                body: JSON.stringify({ Email: email })
            }
        );

        const data = await response.json();

        msg.style.display = 'block';
        msg.textContent = data.message || '';
        if (!data.isSuccess || !data.userDTO) {
            msg.classList.remove('text-success');
            msg.classList.add('text-danger');
            return;
        }

        msg.classList.remove('text-danger');
        msg.classList.add('text-success');

        // Rellenar los campos del formulario de actualización
        document.getElementById('dni').value = data.userDTO.dni || '';
        document.getElementById('username').value = data.userDTO.username || '';
        document.getElementById('surname').value = data.userDTO.surname || '';
        document.getElementById('email').value = data.userDTO.email || '';
        document.getElementById('originalEmail').value = data.userDTO.email || '';

        updateForm.style.display = 'block';
    } catch (error) {
        msg.style.display = 'block';
        msg.classList.remove('text-success');
        msg.classList.add('text-danger');
        msg.textContent = error.message;
    }
}

export async function updateUser(e) {
    e.preventDefault();
    const msg = document.getElementById('updateMsg');
    msg.style.display = 'none';
    msg.textContent = '';
    msg.className = 'mt-3 text-center';

    const token = localStorage.getItem('token');
    const originalEmail = document.getElementById('originalEmail').value;
    const dni = document.getElementById('dni').value;
    const username = document.getElementById('username').value;
    const surname = document.getElementById('surname').value;
    const email = document.getElementById('email').value;

    try {
        const response = await fetch(
            `${API_CONSTANTS.baseUrl}${API_CONSTANTS.usersEndpoint}/update-user`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    OriginalEmail: originalEmail,
                    DniToBeFound: dni,
                    Username: username,
                    Surname: surname,
                    Email: email
                })
            }
        );

        const data = await response.json();

        msg.style.display = 'block';
        msg.textContent = data.message || '';
        if (data.isSuccess) {
            msg.classList.remove('text-danger');
            msg.classList.add('text-success');
        } else {
            msg.classList.remove('text-success');
            msg.classList.add('text-danger');
        }
    } catch (error) {
        msg.style.display = 'block';
        msg.classList.remove('text-success');
        msg.classList.add('text-danger');
        msg.textContent = error.message;
    }
}

document.addEventListener('DOMContentLoaded', loadUsers);