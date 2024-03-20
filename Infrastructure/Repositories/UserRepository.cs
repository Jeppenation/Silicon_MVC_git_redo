using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserRepository(DataContext context) : Repo<UserEntity>(context)
{
    public readonly DataContext _context = context;


    public override async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            IEnumerable<UserEntity> result = await _context.Users
                .Include(x => x.Address)
                .ToListAsync();
            return ResponseFactory.Ok(result);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return ResponseFactory.Error(e.Message);
        }
    }

    public override async Task<ResponseResult> GetOneAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<UserEntity>()
                .Include(x => x.Address)
                .FirstOrDefaultAsync(predicate);
            if (result == null)
            {
                return ResponseFactory.NotFound();
            }

            return ResponseFactory.Ok(result);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return ResponseFactory.Error(e.Message);
        }
    }
}
