function addCourse(event) {
    event.preventDefault();
   const courseName = document.getElementById('courseName').value;
   const credits = document.getElementById('courseCredits').value;
   const teacherId = parseInt(localStorage.getItem('userId'));

   if (credits <=0 ) {
    Swal.fire({
        title: "Kurs kredisi minimum 1 olmalıdır",
        icon: "error",
        timer: 2000,
        showConfirmButton: false
      });
     
       return;
   }
   else{
       fetch('https://localhost:7007/api/Course/courses', {
   method: 'POST',
   headers: {
       'Content-Type': 'application/json',
   },
   body: JSON.stringify({
       Course_Name: courseName, // URL parametre yerine body içinde gönderim
       Credits: credits,
       Instructor_ID: teacherId
   })
   }).then(response => {
           if (!response.ok) {
               Swal.fire({
                   title: "Kurs Eklenemedi",
                   icon: "error",
                   timer: 2000,
            showConfirmButton: false
                 });
           }else{
               Swal.fire({
                   title: "Kurs Eklendi",
                   icon: "success",
                   timer: 2000,
                   showConfirmButton: false
                 }).then(x => {
                    location.reload();
                 });
           }
       })
   }

}


document.getElementById('submit_course').addEventListener("click", function(e) {
   addCourse(e);
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
        document.getElementById('login-user-name').innerHTML = `Hoş geldiniz, ${userName}`;
    }
};