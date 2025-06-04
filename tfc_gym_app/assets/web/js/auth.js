import { API_CONSTANTS } from './consts.js';

async function login(email, password) {
    const response = await fetch(`${API_CONSTANTS.baseUrl}${API_CONSTANTS.authEndpoint}/login-web`, {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify({ 
            Email: email, 
            Password: password 
        })
    });

    const data = await response.json();

    if (data.isSuccess !== true) {
        throw new Error(data.message || 'Error de autenticación');
    }

    return data.bearerToken;
}

export async function checkToken() {
    const token = localStorage.getItem('token');
    if (!token) {
        window.location.href = 'index.html';
        return;
    }

    const response = await fetch(`${API_CONSTANTS.baseUrl}${API_CONSTANTS.authEndpoint}/check-token-status`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Token: token })
    });

    const data = await response.json();
    if (!data.isValid) {
        localStorage.removeItem('token');
        window.location.href = 'index.html';
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('loginForm');
    const email = document.getElementById('email');
    const password = document.getElementById('password');
    const msg = document.getElementById('loginMsg');

    if (form) {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();
            msg.style.display = 'none';

            try {
                const token = await login(email.value, password.value);
                localStorage.setItem('token', token); 
                window.location.href = 'home.html';
            } catch (error) {
                msg.style.display = 'block';
                // Mostrar exactamente el mensaje de la API
                msg.textContent = error.message;
                console.error('Error:', error);
            }
        });
    }
});