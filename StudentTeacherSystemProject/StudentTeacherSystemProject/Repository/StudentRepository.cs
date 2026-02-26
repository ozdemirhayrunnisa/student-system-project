using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Context;
using StudentSystem.Server.Model;
using StudentTeacherSystemProject.Repository.Abstracts;

namespace StudentTeacherSystemProject.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Student> _dbSet;


        public StudentRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Student>();
        }


        public async Task<IEnumerable<Student>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<Student> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(Student entity)
        {

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
