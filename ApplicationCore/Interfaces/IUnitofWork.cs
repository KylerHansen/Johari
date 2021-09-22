
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUnitofWork
    {       
        public IGenericRepository<Adjective> Adjective { get; }
        public IGenericRepository<Client> Client { get; }
        public IGenericRepository<ClientResponse> ClientResponse { get; }
        public IGenericRepository<Friend> Friend { get; }
        public IGenericRepository<FriendResponse> FriendResponse { get; }

        public IGenericRepository<ApplicationUser> ApplicationUser { get; }


        /// <summary>
        /// Commits items. 
        /// </summary>
        /// <returns>Returns number of rows affected.</returns>
        int Commit();

        /// <summary>
        /// Commits asynchronously
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
    }
}
