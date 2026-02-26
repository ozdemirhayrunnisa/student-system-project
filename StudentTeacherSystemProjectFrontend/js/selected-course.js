let selectedCourses = [];

function loadCourses() {
    const userId = parseInt(localStorage.getItem('userId'));
    fetch(`https://localhost:7007/student/StudentCourses/${userId}`)
            .then(response => response.json())
            .then(courses =>  {

                const coursesContainer = document.getElementById('selected-courses');
                const table = document.createElement('table');
                table.setAttribute('border', '1'); 
                table.style.width = '100%'; 
                table.style.borderCollapse = 'collapse'; 
    
                const thead = document.createElement('thead');
                const headerRow = document.createElement('tr');
    
                const headers = ['Kurs Adı', 'Kredisi', 'Öğretmen Adı/Soyadı'];
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
    
                    const nameCell = document.createElement('td');
                    nameCell.textContent = course.course_Name || '';
                    nameCell.style.padding = '8px';
                    row.appendChild(nameCell);
    
                    const creditsCell = document.createElement('td');
                    creditsCell.textContent = course.credits || '';
                    creditsCell.style.padding = '8px';
                    row.appendChild(creditsCell);

                    const instructorCell = document.createElement('td');
                    instructorCell.textContent = course.instructor_Full_Name || '';
                    instructorCell.style.padding = '8px';
                    row.appendChild(instructorCell);
    
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