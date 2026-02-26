using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Context;
using StudentSystem.Server.Model;
using StudentTeacherSystemProject.Repository.Abstracts;

namespace StudentTeacherSystemProject.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Instructor> _dbSet;


        public InstructorRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Instructor>();
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<Instructor> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(Instructor entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

       public async Task UpdateAsync(Instructor entity)
        {
        _dbSet.Update(entity); 
        await _context.SaveChangesAsync(); 
        }
    }
}
