using System.Collections.Generic;
using TabloidMVC.Models;
using Microsoft.Data.SqlClient;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
        void AddTag(Tag tag);
        void UpdateTag(Tag tag);
        Tag GetTagById(int id);
    }
}
