using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class ReactionRepository : BaseRepository, IReactionRepository
    {
        public ReactionRepository(IConfiguration config) : base(config) { }

        public List<Reaction> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Reaction";

                    var reader = cmd.ExecuteReader();
                    var reactions = new List<Reaction>();

                    while (reader.Read())
                    {
                        reactions.Add(new Reaction()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            ImageLocation = reader.GetString(reader.GetOrdinal("ImageLocation"))
                        });
                    }
                    reader.Close();

                    return reactions;
                }
            }
        }

        public void AddReaction(Reaction reaction)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Reaction ([Name], ImageLocation)
                                        OUTPUT INSERTED.ID
                                        VALUES (@name, @imageLocation);";

                    cmd.Parameters.AddWithValue("name", reaction.Name);
                    cmd.Parameters.AddWithValue("imageLocation", reaction.ImageLocation);

                    reaction.Id = (int)cmd.ExecuteScalar();
                   
                }
            }
        }
    }
}
