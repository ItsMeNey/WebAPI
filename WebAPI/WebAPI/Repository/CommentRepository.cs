using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class CommentRepository : IComment
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommentExistAsync(int id)
        {
            return await _context.Comment.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> CreateCommentAsync(Comment comment)
        {
            await _context.AddAsync(comment);
            return await SaveAsync();
        }

        public async Task<bool> DeleteCommentAsync(Comment comment)
        {
            _context.Remove(comment);
            return await SaveAsync();
        }

        public async Task<Comment> GetCommentAsync(int id)
        {
            return await _context.Comment.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Comment>> GetCommentsAsync()
        {
            return await _context.Comment.ToListAsync();
        }

        public async Task<bool> PatchCommentAsync(Comment comment)
        {
            _context.Update(comment);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateCommentAsync(Comment comment)
        {
            _context.Update(comment);
            return await SaveAsync();
        }
    }
}
