using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Models;
using System.Diagnostics;

namespace Infrastructure.Factories;

public class UserFactory
{
    public static UserEntity Create(SignUpModel model)
    {
        try
        {
            var date = DateTime.Now;
            var (securityKey, password) = PasswordHasher.GeneratePasswordHash(model.Password);

            return new UserEntity 
            { 
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.EmailAddress,
                PasswordHash = password,
                Created = date,
                Modified = date,
                //SecurityKey = securityKey

            };
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    public static UserEntity Create()
    {
        try
        {
            var date = DateTime.Now;

            return new UserEntity 
            {
                Id = Guid.NewGuid().ToString(),
                Created = date,
                Modified = date
            };
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }
}
