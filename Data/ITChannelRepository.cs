using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreCodeTChannel.Data
{
    public interface ITChannelRepository
    {
        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // TChannels
        Task<TChannel[]> GetAllTChannelsAsync(bool includeDetails = false);
        Task<TChannel> GetTChannelAsync(string id, bool includeDetails = false);


    }
}