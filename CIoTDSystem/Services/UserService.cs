using CIoTDSystem.Data;
using CIoTDSystem.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CIoTDSystem.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dbContext;

        public UserService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var users = await _dbContext.User.ToListAsync();
                return users;
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User?> GetSingleUserAsync(int id)
        {
            try
            {
                var user = await _dbContext.User.FindAsync(id);
                return user;

            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _dbContext.User.Add(user);
                await _dbContext.SaveChangesAsync();

                return user;
            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }

        public async Task<User?> UpdateUserAsync(int id, User request)
        {
            try
            {
                var user = await _dbContext.User.FindAsync(id);
                if (user is not null)
                {
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.Email = request.Email;
                    user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                    user.UpdatedAt = DateTime.UtcNow;
                }
                await _dbContext.SaveChangesAsync();

                return user;
            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            } catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task<User?> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _dbContext.User.FindAsync(id);
                if (user is not null)
                {
                    _dbContext.User.Remove(user);
                }

                await _dbContext.SaveChangesAsync();

                return user;
            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
        }

        public async Task<User?> ChangeStatusAsync(int id)
        {
            try
            {
                var user = await _dbContext.User.FindAsync(id);
                if (user is not null)
                {
                    user.Status = !user.Status;
                    user.UpdatedAt = DateTime.UtcNow;
                }
                await _dbContext.SaveChangesAsync();

                return user;
            } catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            } catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
