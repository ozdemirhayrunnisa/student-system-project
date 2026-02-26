using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Context;
using StudentSystem.Server.Model;
using StudentTeacherSystemProject.Repository.Abstracts;

namespace StudentTeacherSystemProject.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Course> _dbSet;


        public CourseRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Course>();
        }

        public async Task<IEnumerable<Course>> GetAllAsync() => await _dbSet.Include(c => c.Instructor).ToListAsync();

        public async Task<Course> GetByIdAsync(int id) => await _dbSet.FindAsync(id);


        public async Task AddAsync(Course entity)
        {
            
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
