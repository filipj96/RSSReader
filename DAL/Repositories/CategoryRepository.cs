﻿using DAL.Serialize;
using Microsoft.SqlServer.Server;
using Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : Feed<Category>, IDataHandler<Category>
    {
        public CategoryRepository() : base(new List<Category>())
        {
            objectSerializer = new XMLSerializer<Category>();
        }
        public override void Create(Category entity)
        {
            base.Create(entity);
        }
        public override void Delete(Category entity)
        {
            base.Delete(entity);
        }
        public override List<Category> GetAll()
        {
            return base.GetAll();
        }
        public override void Update(string currentCategory, string newCategory)
        {
            foreach (Category aCategory in list)
            {
                if (aCategory.namn.Equals(currentCategory))
                {
                    aCategory.namn = newCategory;
                }
            }
        }
        public void SaveChanges()
        {
            objectSerializer.Serialize(list, Constants.category.Value, true);
        }
        public List<Category> GetAllData()
        {
            List<Category> test = objectSerializer.DeserializeList(Constants.category.Value);
            list = test.ToList();

            return test;
        }
    }
}
