using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace Repositories.Repositories
{
    public class PublishersRepository : RepositoryBase<Publisher>, IPublishersRepository
    {
        public PublishersRepository():base("publishers"){}
        
    }
}