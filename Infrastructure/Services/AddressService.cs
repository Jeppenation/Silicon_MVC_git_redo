using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AddressService(AddressRepository repository, DataContext context)
{
    private readonly AddressRepository _addressRepository = repository;

    private readonly DataContext _context = context;



    //public async Task<ResponseResult> GetOrCreateAddressAsync(string streetName, string postalCode, string city)
    //{
    //    try
    //    {
    //        var result = await GetAddressAsync(streetName, postalCode, city);
    //        if(result.StatusCode == StatusCodes.NotFound)
    //        {
    //            return await CreateAddressAsync(streetName, postalCode, city);
    //        }
    //        else
    //        {
    //            return result;
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.WriteLine(e);
    //        return ResponseFactory.Error(e.Message);
    //    }
    //}



    //public async Task<ResponseResult> CreateAddressAsync(string streetName, string postalCode, string city)
    //{
    //    try
    //    {
    //        var exists = await _addressRepository.AlreadyExistsAsync(x => x.StreetName == streetName && x.PostalCode == postalCode && x.City == city);
    //        if (exists == null)
    //        {
    //            var result = await _addressRepository.CreateAsync(AddressFactory.CreateAddress(streetName, postalCode, city));

    //            if(result.StatusCode == StatusCodes.Ok)
    //            {
    //                return ResponseFactory.Ok(AddressFactory.CreateAddress((AddressEntity)result.ContentResult!));

    //            }
    //            else
    //            {
    //                return result;
    //            }
    //        }
    //        else
    //        {
    //            return ResponseFactory.Exists();
    //        }


    //    }
    //    catch (Exception e)
    //    {
    //        Debug.WriteLine(e);
    //        return ResponseFactory.Error(e.Message);
    //    }
    //}

    public async Task<AddressEntity> GetAddressAsync(string userId)
    {
        try
        {
            var addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == userId);
            return addressEntity!;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    public async Task<bool> CreateAddressAsync(AddressEntity entity)
    {
        try
        {
            _context.Addresses.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> UpdateAddressAsync(AddressEntity entity)
    {
        try
        {
            var existing = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == entity.UserId);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return false;
        }
    }
}
