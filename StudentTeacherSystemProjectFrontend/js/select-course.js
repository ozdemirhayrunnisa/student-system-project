
let selectedCourses = [];

function loadCourses() {
    const userId = parseInt(localStorage.getItem('userId'));
    fetch(`https://localhost:7007/student/StudentUnselectedCourses/${userId}`)
            .then(response => response.json())
            .then(courses =>  {
debugger
                const coursesContainer = document.getElementById('course-list');

                courses.forEach(course => {
                    const courseElement = document.createElement('div');
                    courseElement.classList.add('course-item');
            
                    // Kurs başlığı
                    const courseTitle = document.createElement('h3');
                    courseTitle.textContent = "Kurs Adı: " + course.course_Name;
            
                    // Kurs açıklaması
                    const courseDesc = document.createElement('p');
                    courseDesc.textContent ="Kurs Kredisi: " + course.credits;

                    const courseInstructor = document.createElement('p');
                    courseInstructor.textContent ="Kursu Veren Akademisyen: " + course.instructor_Full_Name;
            
                    // Seçme butonu
                    const selectButton = document.createElement('button');
                    selectButton.textContent = 'Bu Kursu Seç';
                    selectButton.addEventListener('click', function() {
                        selectCourse(course.course_ID);
                    });
            
                    // Kurs öğesini kurs container'ına ekliyoruz
                    courseElement.appendChild(courseTitle);
                    courseElement.appendChild(courseDesc);
                    courseElement.appendChild(courseInstructor);
                    courseElement.appendChild(selectButton);
            
                    coursesContainer.appendChild(courseElement);
                });
            })
}

window.onload = function() {
    loadCourses();
    const userName = localStorage.getItem('userName');
    
    if (userName) {
        document.getElementById('login-user-name').innerHTML = 
            `Hoş geldiniz, ${userName}`;
    }
};
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

// // Ders seçimi yapılınca
function selectCourse(courseId) {
    debugger
    const userId = parseInt(localStorage.getItem('userId'));
    const url = `https://localhost:7007/Student/${userId}/courses`;
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(parseInt(courseId))
    }).then(response => {
        if (!response.ok) {
            Swal.fire({
                title: "Kurs Seçilirken Hata Meydana Geldi",
                timer: 2000,
            showConfirmButton: false,
                icon: "error"
              })
        }else{
            Swal.fire({
                title: "Kurs Seçildi",
                timer: 2000,
            showConfirmButton: false,
                icon: "success"
              }).then(x => {
                location.reload();
             });
        }
    })
    
}

// // Seçilen dersleri ekranda göster
function displaySelectedCourses() {
    const selectedCoursesContainer = document.getElementById('selected-courses');
    selectedCoursesContainer.innerHTML = ''; // Önceki dersleri temizle

    selectedCourses.forEach(course => {
        const courseItem = document.createElement('li');
        courseItem.textContent = `${course.name} - ${course.description}`;
        selectedCoursesContainer.appendChild(courseItem);
    });
}