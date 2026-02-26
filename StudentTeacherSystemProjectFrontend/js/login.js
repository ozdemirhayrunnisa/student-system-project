document.getElementById('login-form').addEventListener('submit', function(e) {
    e.preventDefault(); // Formun varsayılan submit işlemini engelliyoruz
debugger
    var email = document.getElementById('email').value;
    var password = document.getElementById('password').value;
    var role = document.querySelector('.role-option.selected');
    if (!role) {
        Swal.fire({
            title: "Lütfen kullanıcı tipi seçin",
            icon: "error"
        });
        return;
    }
    role = role.getAttribute('data-role');
   

    fetch('https://localhost:7007/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            Email: email, // URL parametre yerine body içinde gönderim
            Password: password,
            Role: role
        })
    })
  .then(response => {
    if (!response.ok) {
        return response.text().then(err => {
            Swal.fire({
                icon: 'error',
                title: 'Hatalı Giriş',
                text: err || 'Kullanıcı Adı ya da şifre hatalı.'
            });
            throw new Error(err || 'Hatalı Giriş');
        });
    }
    return response.json(); 
    })

    .then(data => {
        if (data.token) {
            localStorage.setItem('token', data.token);
            localStorage.setItem('userName', data.userName);
            localStorage.setItem('userId', data.userId);
            localStorage.setItem('userRole', data.userRole);
    
            Swal.fire({
                icon: 'success',
                title: 'Giriş Başarılı',
                text: `Hoşgeldiniz, ${data.userName}!`,
                timer: 2000,
                showConfirmButton: false
            });
    
            if(role === "Student"){
                window.location.href = '/student-dashboard.html';
        }
            else if(role === "Instructor"){
                window.location.href = '/teacher-dashboard.html';
            }
            else if(role === "Admin"){
                window.location.href = '/admin-dashboard.html';
            }
            // Burada kullanıcıyı yönlendirebilirsiniz:
           
        }
    })
    .catch(error => {
        console.error('Error:', error);
    });
    // Kullanıcı adı ve şifre kontrolü sonrası doğru panele yönlendirme yapacağız
    
});

window.onload = function(){
    const roleOptions = document.querySelectorAll('.role-option');
    let role = null;
    roleOptions.forEach(option => {
        option.addEventListener('click', () => {
            roleOptions.forEach(opt => opt.classList.remove('selected'));
            option.classList.add('selected');
            role = option.getAttribute('data-role');
        });
    });
}