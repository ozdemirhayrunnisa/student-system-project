function addTeachert(event) {
    event.preventDefault();
    const firstName = document.getElementById('firstName').value;
    const lastName = document.getElementById('lastName').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const major = document.getElementById('major').value;

    fetch('https://localhost:7007/Instructor', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            First_Name: firstName, // URL parametre yerine body içinde gönderim
            Last_Name: lastName,
            Email: email,
            Password: password,
            Department: major,
        })
    }).then(response => {
        if (!response.ok) {
            Swal.fire({
                title: "Öğretmen Eklenemedi",
                timer: 2000,
            showConfirmButton: false,
                icon: "error"
              })
        }else{
            Swal.fire({
                title: "Öğretmen Eklendi",
                timer: 2000,
            showConfirmButton: false,
                icon: "success"
              }).then(x => {
                location.reload();
             });
        }
    })
}

document.getElementById('submit_teacher').addEventListener("click", function(e) {
    addTeachert(e);
 });

 document.getElementById('log-out').addEventListener('click', () => {
    // localStorage temizleme
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    localStorage.removeItem('userId');

    // Login sayfasına yönlendirme
    window.location.href = '/login.html';
});

document.addEventListener('DOMContentLoaded', () => {
    const token = localStorage.getItem('token');
    if (!token) {
        Swal.fire({
            icon: 'error',
            title: 'Lütfen önce giriş yapın',
            timer: 2000,
            showConfirmButton: false
        }).then(x => {
            window.location.href = '/login.html';

        });

    }
});

window.onload = function() {
    const userName = localStorage.getItem('userName');
    
    if (userName) {
        document.getElementById('login-user-name').innerHTML = 
            `Hoş geldiniz, ${userName}`;
    }
};