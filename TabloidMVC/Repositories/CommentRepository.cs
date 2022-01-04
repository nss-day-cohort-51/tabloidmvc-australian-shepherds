using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }
        public List<Comment> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Select * FROM Comment";
                    var reader = cmd.ExecuteReader();

                    var comments = new List<Comment>();

                    while (reader.Read())
                    {
                        comments.Add(new Comment()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                        });
                    }
                    reader.Close();

                    return comments;
                }
            }
        }

        public List<Comment> GetCommentsByPostId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT *
                         FROM Comment c
                              LEFT JOIN UserProfile u ON c.UserProfileId = u.id
                              LEFT JOIN Post p ON c.PostId = p.id
                        WHERE c.PostId = @id
                        ORDER BY c.CreateDateTime";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var postComments = new List<Comment>();

                        while (reader.Read())
                        {
                            postComments.Add(NewCommentFromReader(reader));
                        }

                        return postComments;
                    }
                }
            }
        }

        public Comment GetSingleComment(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT *
                         FROM Comment c
                              LEFT JOIN UserProfile u ON c.UserProfileId = u.id
                              LEFT JOIN Post p ON c.PostId = p.id
                        WHERE c.Id = @id
                        ORDER BY c.CreateDateTime";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var comment = new Comment();

                        if (reader.Read())
                        {
                            comment = NewCommentFromReader(reader);
                        }

                        return comment;
                    }
                }
            }
        }

        private Comment NewCommentFromReader(SqlDataReader reader)
        {
            return new Comment()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Subject = reader.GetString(reader.GetOrdinal("Subject")),
                Content = reader.GetString(reader.GetOrdinal("Content")),
                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                Post = new Post()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title"))
                },
                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                UserProfile = new UserProfile()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                    DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                }
            };

        }

        public void Remove(int id)
        {
            using(var conn = Connection)
            {
                conn.Open();

                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Comment WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Add(Comment comment)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Comment (PostId, UserProfileId, Subject, Content, CreateDateTime)
                    OUTPUT INSERTED.ID
                    VALUES (@postId, @userProfileId, @subject, @content, @createDateTime)
                    ";
                    cmd.Parameters.AddWithValue("@postId", comment.PostId);
                    cmd.Parameters.AddWithValue("@userProfileId", comment.UserProfileId);
                    cmd.Parameters.AddWithValue("@subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@content", comment.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", comment.CreateDateTime);

                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
