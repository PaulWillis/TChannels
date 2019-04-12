using System.Linq; 
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreCodeTChannel.Data
{
    public class TChannelRepository : ITChannelRepository
    {
        private readonly TChannelContext _context;
        private readonly ILogger<TChannelRepository> _logger;

        public TChannelRepository(TChannelContext context, ILogger<TChannelRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class 
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T: class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one record was changed.
            return (await _context.SaveChangesAsync()) > 0;
        }
         

        public async Task<TChannel[]> GetAllTChannelsAsync(bool includeDetails = false)
        {
            _logger.LogInformation($"Getting all records");

            IQueryable<TChannel> query = _context.TChannels;


            //Sort the records
            query = query.OrderByDescending(c => c.ChannelName);

            return await query.ToArrayAsync();
        }

        public async Task<TChannel> GetTChannelAsync(string id, bool includeDetails = false)
        {
            _logger.LogInformation($"Getting a record for {id}");

            IQueryable<TChannel> query = _context.TChannels ;
             
            // Query the records.
            query = query.Where(c => c.Id.ToString() == id);

            return await query.FirstOrDefaultAsync();
        }
         
    }
}
