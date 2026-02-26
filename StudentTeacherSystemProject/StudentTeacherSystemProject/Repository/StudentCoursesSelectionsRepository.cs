using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Context;
using StudentSystem.Server.Model;
using StudentTeacherSystemProject.Model;
using StudentTeacherSystemProject.Repository.Abstracts;

namespace StudentTeacherSystemProject.Repository
{
    public class StudentCoursesSelectionsRepository : IStudentCoursesSelectionsRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<StudentCoursesSelections> _dbSet;
        private readonly DbSet<Student> _dbStudentSet;


        public StudentCoursesSelectionsRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<StudentCoursesSelections>();
            _dbStudentSet = _context.Set<Student>();
        }


        public async Task<IEnumerable<StudentCoursesSelections>> GetAllAsync() => await _dbSet.Include(i => i.Student).Include(i => i.Course.Instructor).ToListAsync();

        public async Task<StudentCoursesSelections> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(StudentCoursesSelections entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(StudentCoursesSelections entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
