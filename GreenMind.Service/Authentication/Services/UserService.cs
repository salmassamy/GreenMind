using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenMind.Service.Authentication.Services
{
    using GreenMind.Presistance.Data.DbContexts;
    using GreenMind.ServiceAbstraction.DTOs;
    using GreenMind.ServiceAbstraction.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasherService _hasher;

        public UserService(ApplicationDbContext context, IPasswordHasherService hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public async Task<UserProfileDto> GetProfileAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                throw new Exception("User not found");

            return new UserProfileDto
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                ProfilePic = user.ProfilePic
            };
        }

        public async Task<UserProfileDto> UpdateProfileAsync(int userId, UpdateUserProfileDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                throw new Exception("User not found");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Name is required");

            if (!string.IsNullOrWhiteSpace(dto.Gender) &&
                dto.Gender != "Male" &&
                dto.Gender != "Female")
                throw new Exception("Gender must be Male or Female");

            if (!string.IsNullOrWhiteSpace(dto.Phone))
            {
                var phoneExists = await _context.Users
                    .AnyAsync(x => x.Id != userId && x.Phone == dto.Phone);

                if (phoneExists)
                    throw new Exception("Phone already exists");
            }

            user.Name = dto.Name.Trim();
            user.Phone = dto.Phone?.Trim();
            user.Gender = dto.Gender?.Trim();
            user.ProfilePic = dto.ProfilePic?.Trim();

            await _context.SaveChangesAsync();

            return new UserProfileDto
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                ProfilePic = user.ProfilePic
            };
        }

        public async Task<string> ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                throw new Exception("User not found");

            if (string.IsNullOrWhiteSpace(dto.CurrentPassword))
                throw new Exception("Current password is required");

            if (string.IsNullOrWhiteSpace(dto.NewPassword))
                throw new Exception("New password is required");

            if (dto.NewPassword != dto.ConfirmPassword)
                throw new Exception("Passwords do not match");

            if (!_hasher.Verify(user.PasswordHash, dto.CurrentPassword))
                throw new Exception("Current password is incorrect");

            user.PasswordHash = _hasher.Hash(dto.NewPassword);
            await _context.SaveChangesAsync();

            return "Password changed successfully.";
        }
    }
}
