﻿using Exercise4.Models;
using Exercise4.Models.DTOs;
using System.Data.SqlClient;

namespace Exercise4.Repositories
{

    public interface IAnimalRepository
    {
        Task<bool> Exists(int id);
        Task Create(Animal animal);
        Task Delete(int id);
        Task Update(int index, UpdateAnimal animal);

    }

    public class AnimalRepository: IAnimalRepository
    {
        private readonly IConfiguration _configuration;
        public AnimalRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Exists(int id)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = $"select id from animal where id = @1";
                sqlCommand.Parameters.AddWithValue("@1", id);
                await sqlConnection.OpenAsync();
                if (await sqlCommand.ExecuteScalarAsync() is not null)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task Create(Animal animal)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = $"insert into animal (id, name, description, category, area) values (@1, @2, @3, @4, @5)";
                sqlCommand.Parameters.AddWithValue("@1", animal.ID);
                sqlCommand.Parameters.AddWithValue("@2", animal.Name);
                sqlCommand.Parameters.AddWithValue("@3", animal.Description);
                sqlCommand.Parameters.AddWithValue("@4", animal.Category);
                sqlCommand.Parameters.AddWithValue("@5", animal.Area);
                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
            }
        }
        public async Task Delete(int index)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = $"Delete From animal WHERE id = {index}";
                sqlCommand.Parameters.AddWithValue("@1", index);
                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
            }
        }
        public async Task Update(int index, UpdateAnimal animal)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = $"UPDATE animal SET name = @2,description = @3, category = @4, area = @5 WHERE id = {index}" ;
                sqlCommand.Parameters.AddWithValue("@2", animal.Name);
                sqlCommand.Parameters.AddWithValue("@3", animal.Description);
                sqlCommand.Parameters.AddWithValue("@4", animal.Category);
                sqlCommand.Parameters.AddWithValue("@5", animal.Area);
                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
            }
        }
    }
}
