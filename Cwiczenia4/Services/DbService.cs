using Cwiczenia4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cwiczenia4.Services
{
    public class DbService : IDbService
    {
        private readonly IConfiguration _configuration;

        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool AddAnimal(Animal animal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultDbCon")))
            {
                using (SqlCommand com = new SqlCommand()) 
                { 
                    com.Connection = con;
                    com.CommandText = "INSERT INTO Animal(Name, Description, Category, Area) VALUES(@name, @description, @category, @area)";
                    com.Parameters.AddWithValue("@name", animal.Name);
                    com.Parameters.AddWithValue("@description", animal.Description);
                    com.Parameters.AddWithValue("@category", animal.Category);
                    com.Parameters.AddWithValue("@area", animal.Area);
                    con.Open();
                    int rowsAffected = com.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }

        public bool DeleteAnimal(int idAnimal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultDbCon")))
            {
                using (SqlCommand com = new SqlCommand("DELETE Animal WHERE idAnimal=@id", con))
                {
                    com.Parameters.AddWithValue("@id", idAnimal);
                    con.Open();
                    int rowsAffected = com.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        return false;
                    }
                    return true;

                }
            }
        }

        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultDbCon")))
            {
                HashSet<string> orderByCategories = new HashSet<string> { "name", "description", "category", "area" };
                if (orderBy == null || !orderByCategories.Contains(orderBy.ToLower()))
                {
                    orderBy = "Name";
                }
                var animals = new List<Animal>();
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = $"SELECT * FROM Animal ORDER BY {orderBy} ASC";
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        animals.Add(new Animal
                        {
                            IdAnimal = int.Parse(dr["IdAnimal"].ToString()),
                            Name = dr["Name"].ToString(),
                            Description = dr["Description"].ToString(),
                            Category = dr["Category"].ToString(),
                            Area = dr["Area"].ToString()
                        });
                    }
                    return animals;
                }
            }
        }

        public bool UpdateAnimal(int idAnimal, Animal animal)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultDbCon")))
            {
                using (SqlCommand com = new SqlCommand
                    ("UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE idAnimal = @idAnimal", con))
                {
                    com.Parameters.AddWithValue("name", animal.Name);
                    com.Parameters.AddWithValue("description", animal.Description);
                    com.Parameters.AddWithValue("category", animal.Category);
                    com.Parameters.AddWithValue("area", animal.Area);
                    com.Parameters.AddWithValue("idAnimal", idAnimal);
                    con.Open();
                    int rowsAffected = com.ExecuteNonQuery();
                    if(rowsAffected == 0)
                    {
                        return false;
                    }
                    return true;

                }
            }
        }
    }
}
