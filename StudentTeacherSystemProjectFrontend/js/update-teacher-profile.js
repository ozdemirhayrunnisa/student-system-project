function loadSelectedInstructor() {
    const userId = parseInt(localStorage.getItem('userId'));
    var url = 'https://localhost:7007/Instructor/GetInctructorById/' + userId;
    fetch(url)
        .then(response => response.json())
        .then(instructor => {
            debugger
            document.getElementById('firstName').value = instructor.first_Name;
            document.getElementById('lastName').value = instructor.last_Name;
            document.getElementById('password').value = instructor.password;
            document.getElementById('email').value = instructor.email;
            document.getElementById('department').value = instructor.department;
        })
        .catch(error => console.error('Error loading teachers:', error));
    }

window.onload = function() {
loadSelectedInstructor();
const userName = localStorage.getItem('userName');

if (userName) {
    document.getElementById('login-user-name').innerHTML = 
        `Hoş geldiniz, ${userName}`;
}
};


function updateInstructor(event){
    event.preventDefault();
    const firstName = document.getElementById('firstName').value;
    const lastName = document.getElementById('lastName').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const newPassword = document.getElementById('newPassword').value;
    const department = document.getElementById('department').value;


    if(password == newPassword){
        Swal.fire({
            title: "Eski şifreniz ile yeni şifreniz aynı olamaz!!",
            timer: 2000,
        showConfirmButton: false,
            icon: "error"
          })
    }

    const userId = parseInt(localStorage.getItem('userId'));
    var url = 'https://localhost:7007/Instructor/' +userId;
    fetch(url, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            First_Name: firstName, // URL parametre yerine body içinde gönderim
            Last_Name: lastName,
            Email: email,
            Password: newPassword,
            Department: department,
            Instructor_ID: userId
        })
    }).then(response => {
        if (!response.ok) {
            debugger
            Swal.fire({
                title: "Bilgiler Güncellenemedi.",
                Text: response.message,
                timer: 2000,
            showConfirmButton: false,
                icon: "error"
              })
        }else{
            Swal.fire({
                title: "Bilgiler Başarılı Bir Şekilde Güncellendi.",
                timer: 2000,
            showConfirmButton: false,
                icon: "success"
              }).then(x => {
                location.reload();
             });
        }
    })
}


document.getElementById('update_instructor').addEventListener("click", function(e) {
    updateInstructor(e);
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
