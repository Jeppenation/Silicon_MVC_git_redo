using Infrastructure.Entities;
using Infrastructure.Models;
using System.Diagnostics;

namespace Infrastructure.Factories;

public class AddressFactory
{

    //Generates an empty AddressEntity
    public static AddressEntity CreateAddress()
    {
        try
        {
            return new AddressEntity();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    //Generates an AddressEntity with the given parameters
    public static AddressEntity CreateAddress(string streetName, string postalCode, string city)
    {
        try
        {
            return new AddressEntity
            {
                StreetName = streetName,
                PostalCode = postalCode,
                City = city
            };
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    public static AddressModel CreateAddress(AddressEntity entity)
    {
        try
        {
            return new AddressModel
            {
                Id = entity.Id,
                StreetName = entity.StreetName,
                PostalCode = entity.PostalCode,
                City = entity.City
            };
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }
}
