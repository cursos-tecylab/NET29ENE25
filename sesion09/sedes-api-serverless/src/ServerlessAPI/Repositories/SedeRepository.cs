using Amazon.DynamoDBv2.DataModel;
using ServerlessAPI.Entities;

namespace ServerlessAPI.Repositories;

public class SedeRepository : ISedeRepository
{

    private readonly IDynamoDBContext _dynamoDB;

    public SedeRepository(IDynamoDBContext dynamoDB)
    {
        _dynamoDB = dynamoDB;
    }

    public async Task CreateAsync(Sede sede)
    {
       await _dynamoDB.SaveAsync(sede);
    }

    public async Task<Sede?> GetByIdAsync(string id)
    {
        return await _dynamoDB.LoadAsync<Sede>(id);
    }
}