using System.Data;
using TaskManager.Core.Entities;
using TaskManager.Core.Models.User;

namespace TaskManager.Core.Extentions;

public static class UserExtension
{
    public static UserModel ToModel(this User user)
    {
        return new()
        {
            Id = user.Id,
            Phone = user.Phone,
            Email = user.Email,
            Username = user.Username,
            LastName = user.LastName,
            FirstName = user.FirstName,
            LastLoginData = user.LastLoginData,
            RegistrationDate = user.RegistrationDate,
        };
    }

    public static User ToEntity(this UserModel userModel)
    {
        return new()
        {
            Id = userModel.Id,
            Phone = userModel.Phone,
            Email = userModel.Email,
            Username = userModel.Username,
            LastName = userModel.LastName,
            FirstName = userModel.FirstName,
            LastLoginData = userModel.LastLoginData,
            RegistrationDate = userModel.RegistrationDate,
        };
    }
}
