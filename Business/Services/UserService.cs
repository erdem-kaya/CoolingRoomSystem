﻿using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models.Users;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserForm> CreateAsync(UserRegistrationForm form)
    {
        if(form == null)
            return null!;

        await _userRepository.BeginTransactionAsync();

        try
        {
            var userEntity = UserFactory.Create(form);
            var createdUser = await _userRepository.CreateAsync(userEntity);
            await _userRepository.CommitTransactionAsync();
            return createdUser != null ? UserFactory.Create(createdUser) : null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating user entity : {ex.Message}");
            await _userRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<IEnumerable<UserForm>> GetAllAsync()
    {
        try
        {
            var allUsers = await _userRepository.GetAllAsync();
            var result = allUsers.Select(UserFactory.Create).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all users : {ex.Message}");
            return null!;
        }
    }

    public async Task<UserForm> GetByIdAsync(int id)
    {
        try
        {
            var getUserWithId = await _userRepository.GetItemAsync(u => u.Id == id);
            var result = getUserWithId != null ? UserFactory.Create(getUserWithId) : null!;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting user by id : {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var deleteUser = await _userRepository.DeleteAsync(u => u.Id == id);
            if (!deleteUser)
                throw new Exception($"Error deleting user with ID {id}");
            
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting user : {ex.Message}");
            return false;
        }
    }

    public async Task<UserForm> UpdateAsync(UserUpdateForm form)
    {
        if (form == null)
            return null!;

        try
        {
            var findUser = await _userRepository.GetItemAsync(u => u.Id == form.Id) ?? throw new Exception($"User with ID {form.Id} does not exist.");
            UserFactory.Update(findUser, form);
            var updatedUser = await _userRepository.UpdateAsync(u => u.Id == form.Id, findUser);
            var result = updatedUser != null ? UserFactory.Create(updatedUser) : null!;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating user : {ex.Message}");
            return null!;
        }
    }

    public async Task<UserForm?> AuthenticateAsync(string email, string password)
    {
        try
        {
            var user = await _userRepository.GetItemAsync(u => u.Email == email) ?? throw new UnauthorizedAccessException("User not found.");
            if (user == null)
                return null;

            var isPasswordValid = UserFactory.VerifyPassword(user, password);
            if (!isPasswordValid)
                return null;

            return UserFactory.Create(user);


        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error authenticating user: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    {
        try
        {
            var user = await _userRepository.GetItemAsync(u => u.Id == userId) ?? throw new UnauthorizedAccessException("User not found.");
            if (user == null)
                return false;

            var isCurrentPasswordValid = UserFactory.VerifyPassword(user, currentPassword);
            if (!isCurrentPasswordValid)
                return false;

            var (hashedPassword, salt) = PasswordHasher.HashPassword(newPassword);
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = salt;

            var updatedUser = await _userRepository.UpdateAsync(u => u.Id == userId, user);
            return updatedUser != null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error changing password : {ex.Message}");
            return false;
        }
    }
}
