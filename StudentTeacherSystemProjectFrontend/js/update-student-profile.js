function loadSelectedStudent() {
    const userId = parseInt(localStorage.getItem('userId'));
    var url = 'https://localhost:7007/Student/GetStudentById/' +userId;
    fetch(url)
        .then(response => response.json())
        .then(student => {
            document.getElementById('firstName').value = student.first_Name;
            document.getElementById('lastName').value = student.last_Name;
            document.getElementById('password').value = student.password;
            document.getElementById('email').value = student.email;
            document.getElementById('major').value = student.major;
            document.getElementById('selectedCourses').value = student.courses_Selected;
        })
        .catch(error => console.error('Error loading students:', error));
    }

window.onload = function() {
loadSelectedStudent();
const userName = localStorage.getItem('userName');

if (userName) {
    document.getElementById('login-user-name').innerHTML = 
        `Hoş geldiniz, ${userName}`;
}
};


function updateStudent(event){
    event.preventDefault();
    const firstName = document.getElementById('firstName').value;
    const lastName = document.getElementById('lastName').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const newPassword = document.getElementById('newPassword').value;
    const major = document.getElementById('major').value;
    const selectedCourses = document.getElementById('selectedCourses').value

    if(password == newPassword){
        Swal.fire({
            title: "Eski şifreniz ile yeni şifreniz aynı olamaz!!",
            timer: 2000,
            showConfirmButton: false,
            icon: "error"
          })
    }

    const userId = parseInt(localStorage.getItem('userId'));
    var url = 'https://localhost:7007/Student/' +userId;
    fetch(url, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            First_Name: firstName, // URL parametre yerine body içinde gönderim
            Last_Name: lastName,
            Email: email,
            Password: newPassword != '' ?newPassword:password,
            Major: major,
         Courses_Selected: selectedCourses,
         Student_ID: userId
        })
    }).then(response => {
        if (!response.ok) {
            Swal.fire({
                title: "Bilgiler Güncellenemedi.",
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


document.getElementById('update_student').addEventListener("click", function(e) {
    updateStudent(e);
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
