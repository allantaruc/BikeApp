using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public interface IRepository
    {

    }
    public interface IBikesRepository
    {
        Task<List<Bike>> GetBikesAsync();
        Task<Bike> GetBikeAsync(Guid id);
        Task AddBikeAsync(Bike bike);
        Task UpdateBikeAsync(Bike bike);
        Task DeleteBikeAsync(Bike bike);
    }
    public class SqlRepository
    {

        public class Bikes : IRepository, IBikesRepository
        {
            private readonly IConfiguration _configuration;
            private readonly string _connectionString;
            private readonly IMapper _autoMapper;
            public Bikes(IConfiguration configuration, IMapper autoMapper)
            {
                _autoMapper = autoMapper;
                _configuration = configuration;
                _connectionString = _configuration.GetConnectionString("BikeAppDbConnection");
            }

            public Task AddBikeAsync(Bike bike)
            {

                throw new NotImplementedException();
            }

            public Task DeleteBikeAsync(Bike bike)
            {
                throw new NotImplementedException();
            }

            public async Task<Bike> GetBikeAsync(Guid id)
            {
                Bike returnObject = null;
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "dbo.BikesGet",
                        CommandType = System.Data.CommandType.StoredProcedure,
                        Parameters = {
                            new SqlParameter("Id", id)
                        }
                    })
                    {
                        connection.Open();    
                        using (var reader = await command.ExecuteReaderAsync()) {
                            if(await reader.ReadAsync()) {
                                returnObject = new Bike {
                                    Id = (Guid)reader.GetValue("Id"),
                                    CheckoutTime =reader.GetValue("CheckoutTime") == DBNull.Value ? null : (DateTime?)reader.GetValue("CheckoutTime"),
                                    CheckinTime = reader.GetValue("CheckinTime") == DBNull.Value ? null : (DateTime?)reader.GetValue("CheckinTime"),
                                    TotalTimeSpent = (int)reader.GetValue("TotalTimeSpent"),
                                    DateModified = (DateTime)reader.GetValue("DateModified"),
                                };   
                            }
                        }
                        connection.Close();
                    }
                }
                if(returnObject == null) throw new KeyNotFoundException($"Bike with bike id: {id} not found.");
                return returnObject;
            }

            public async Task<List<Bike>> GetBikesAsync()
            {

                var returnObject = new List<Bike>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "dbo.BikesGet",
                        CommandType = System.Data.CommandType.StoredProcedure
                    })
                    {
                        connection.Open();    
                        using (var reader = await command.ExecuteReaderAsync()) {
                            while(await reader.ReadAsync()) {
                                returnObject.Add(new Bike {
                                    Id = (Guid)reader.GetValue("Id"),
                                    CheckoutTime =reader.GetValue("CheckoutTime") == DBNull.Value ? null : (DateTime?)reader.GetValue("CheckoutTime"),
                                    CheckinTime = reader.GetValue("CheckinTime") == DBNull.Value ? null : (DateTime?)reader.GetValue("CheckinTime"),
                                    TotalTimeSpent = (int)reader.GetValue("TotalTimeSpent"),
                                    DateModified = (DateTime)reader.GetValue("DateModified"),
                                });   
                            }
                        }
                        connection.Close();
                    }
                }
                if(returnObject.Count == 0) throw new KeyNotFoundException($"Bikes not found.");
                return returnObject;
            }

            public Task UpdateBikeAsync(Bike bike)
            {
                throw new NotImplementedException();
            }
        }

    }

    public class EfRepository
    {
        public class Bikes : IRepository, IBikesRepository
        {
            private readonly DataContext _context;
            //private readonly Mapper _mapper;
            public Bikes(DataContext context
            //, Mapper mapper
            )
            {
                //_mapper = mapper;
                _context = context;
            }

            public async Task AddBikeAsync(Bike bike)
            {
                await _context.Bikes.AddAsync(bike);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteBikeAsync(Bike bike)
            {
                var bikeFromDb = await _context.Bikes.FindAsync(bike.Id);
                if (bikeFromDb != null)
                {
                    _context.Bikes.Remove(bikeFromDb);
                    await _context.SaveChangesAsync();
                }
                else throw new KeyNotFoundException($"Bike with bike id: {bike.Id} not found.");

            }

            public async Task<Bike> GetBikeAsync(Guid id)
            {
                var bikeFromDb = await _context.Bikes.FindAsync(id);
                if (bikeFromDb != null) return bikeFromDb;
                else throw new KeyNotFoundException($"Bike with bike id: {id} not found.");
            }

            public async Task<List<Bike>> GetBikesAsync()
            {
                var bikesFromDb = await _context.Bikes.ToListAsync();
                if (bikesFromDb.FirstOrDefault() != null) return bikesFromDb;
                else throw new KeyNotFoundException($"No bikes found.");
            }

            public Task UpdateBikeAsync(Bike bike)
            {
                // _mapper.Map()
                throw new NotImplementedException();
            }
        }

    }
}