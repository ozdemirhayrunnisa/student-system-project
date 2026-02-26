function loadStudents() {
    var userId = localStorage.getItem('userId');
    var url  ='https://localhost:7007/Instructor/GetInstructorStudents/' + userId;
        fetch(url)
        .then(response => response.json())
        .then(students => {
            const studentsContainer = document.getElementById('all_student');

            // Create the table element
            const table = document.createElement('table');
            table.setAttribute('border', '1'); // Add border to the table (optional)
            table.style.width = '100%'; // Set table width (optional)
            table.style.borderCollapse = 'collapse'; // Optional style for cleaner appearance

            // Create the table header
            const thead = document.createElement('thead');
            const headerRow = document.createElement('tr');

            const headers = ['Ad', 'Soyad', 'Email', 'Bölüm'];
            headers.forEach(headerText => {
                const th = document.createElement('th');
                th.textContent = headerText;
                th.style.padding = '8px'; // Optional padding for a cleaner look
                th.style.backgroundColor = '#f4f4f4'; // Optional background color for header
                th.style.textAlign = 'left';
                headerRow.appendChild(th);
            });
            thead.appendChild(headerRow);
            table.appendChild(thead);

            // Create the table body
            const tbody = document.createElement('tbody');

            students.forEach(student => {

                const row = document.createElement('tr');

                // Create table cells for each property
                const nameCell = document.createElement('td');
                nameCell.textContent = student.first_Name || '';
                nameCell.style.padding = '8px';
                row.appendChild(nameCell);

                const surnameCell = document.createElement('td');
                surnameCell.textContent = student.last_Name || '';
                surnameCell.style.padding = '8px';
                row.appendChild(surnameCell);

                const emailCell = document.createElement('td');
                emailCell.textContent = student.email || '';
                emailCell.style.padding = '8px';
                row.appendChild(emailCell);

                const departmentCell = document.createElement('td');
                departmentCell.textContent = student.major || '';
                departmentCell.style.padding = '8px';
                row.appendChild(departmentCell);

                tbody.appendChild(row);
            });

            table.appendChild(tbody);
            studentsContainer.appendChild(table);
        })
        .catch(error => console.error('Error loading students:', error));
    }

function loadCourses() {
    var userId = localStorage.getItem('userId');
    var url  ='https://localhost:7007/Instructor/GetInstructorCourses/' + userId;
        fetch(url)
            .then(response => response.json())
            .then(courses => {
                const coursesContainer = document.getElementById('all_course');
    
                // Create the table element
                const table = document.createElement('table');
                table.setAttribute('border', '1'); // Add border to the table (optional)
                table.style.width = '100%'; // Set table width (optional)
                table.style.borderCollapse = 'collapse'; // Optional style for cleaner appearance
    
                // Create the table header
                const thead = document.createElement('thead');
                const headerRow = document.createElement('tr');
    
                const headers = ['Kurs Adı', 'Kredisi', 'Öğretmen Adı/Soyadı'];
                headers.forEach(headerText => {
                    const th = document.createElement('th');
                    th.textContent = headerText;
                    th.style.padding = '8px'; // Optional padding for a cleaner look
                    th.style.backgroundColor = '#f4f4f4'; // Optional background color for header
                    th.style.textAlign = 'left';
                    headerRow.appendChild(th);
                });
                thead.appendChild(headerRow);
                table.appendChild(thead);
    
                // Create the table body
                const tbody = document.createElement('tbody');
    
                courses.forEach(course => {
                    const row = document.createElement('tr');
    
                    // Create table cells for each property
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
            .catch(error => console.error('Error loading students:', error));
        }
 
 window.onload = function() {
    loadStudents();
    loadCourses();

    const userName = localStorage.getItem('userName');
    
    if (userName ) {
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