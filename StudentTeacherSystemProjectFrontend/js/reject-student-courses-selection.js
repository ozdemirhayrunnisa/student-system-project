let selectedCourses = [];

function loadCourses() {
    const userId = parseInt(localStorage.getItem('userId'));
    fetch(`https://localhost:7007/GetAllRejectedCourse/${userId}`)
            .then(response => response.json())
            .then(courses =>  {

                const coursesContainer = document.getElementById('all_course');
                const table = document.createElement('table');
                table.setAttribute('border', '1'); 
                table.style.width = '100%'; 
                table.style.borderCollapse = 'collapse'; 
    
                const thead = document.createElement('thead');
                const headerRow = document.createElement('tr');
    
                const headers = ['Kurs Adı', 'Öğrenci Adı'];
                headers.forEach(headerText => {
                    const th = document.createElement('th');
                    th.textContent = headerText;
                    th.style.padding = '8px'; 
                    th.style.backgroundColor = '#f4f4f4'; 
                    th.style.textAlign = 'left';
                    headerRow.appendChild(th);
                });
                thead.appendChild(headerRow);
                table.appendChild(thead);
    
                const tbody = document.createElement('tbody');
    
                courses.forEach(course => {
                    const row = document.createElement('tr');
    
                    const courseNameCell = document.createElement('td');
                    courseNameCell.textContent = course.course_Name || '';
                    courseNameCell.style.padding = '8px';
                    row.appendChild(courseNameCell);
    
                    const studentNameCell = document.createElement('td');
                    studentNameCell.textContent = course.student_Full_Name || '';
                    studentNameCell.style.padding = '8px';
                    row.appendChild(studentNameCell);

    
                    tbody.appendChild(row);
                });
    
                table.appendChild(tbody);
                coursesContainer.appendChild(table);
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