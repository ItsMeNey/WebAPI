using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface IComment
    {
        Task<List<Comment>> GetCommentsAsync();
        Task<Comment> GetCommentAsync(int id);
        Task<bool> CommentExistAsync(int id);
        Task<bool> CreateCommentAsync(Comment comment);
        Task<bool> UpdateCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(Comment comment);
        Task<bool> PatchCommentAsync(Comment comment);
        Task<bool> SaveAsync();
    }
}
