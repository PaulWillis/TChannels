using AutoMapper;
using CoreCodeTChannel.Data;
using CoreCodeTChannel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCodeTChannel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TChannelsController : ControllerBase
    {
        private readonly ITChannelRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public TChannelsController(ITChannelRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }
        [HttpGet]
        public async Task<ActionResult<TChannelModel[]>> Get(bool includeDetails = false)
        {
            try
            {
                var results = await _repository.GetAllTChannelsAsync(includeDetails);
                return _mapper.Map<TChannelModel[]>(results);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TChannelModel>> Get(string id) {

            try
            {
                var result = await _repository.GetTChannelAsync(id);
                if (result == null) return NotFound();

                return _mapper.Map<TChannelModel>(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
         
        public async Task<ActionResult<TChannelModel>> Post(TChannelModel model) {
            try
            {

                var existing = await _repository.GetTChannelAsync(model.Id);

                if (existing != null)
                {
                    return BadRequest("Id in use");
                }

                //var location = _linkGenerator.GetPathByAction("Get", "TChannels",new  { id = model.Id });

                //if (string.IsNullOrWhiteSpace(location)) {
                //    return BadRequest("Could not use current Moniker");
                //}

                // Create a new tChannel
                var tChannel = _mapper.Map<TChannel>(model);
                _repository.Add(tChannel);

                if (await _repository.SaveChangesAsync()) {
                    return Created($"/api/tChannels/{tChannel.ChannelName}", _mapper.Map<TChannelModel>(tChannel));
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
            return BadRequest();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<TChannelModel>> Post(string id, TChannelModel model)
        {

            try
            { 
                var tChannel = _mapper.Map<TChannel>(model); 

                var channelInRepo = await _repository.GetTChannelAsync(id, false);
                if (channelInRepo != null) return BadRequest("Record already exists");

                var channelInRepoFromBody = await _repository.GetTChannelAsync(model.Id, false);
                if (channelInRepoFromBody != null) return BadRequest($"Record {model.Id.ToString()} already exists");

                _repository.Add(tChannel);

                if (await _repository.SaveChangesAsync())
                {
                    var url = _linkGenerator.GetPathByAction(HttpContext, "Get", values: new
                    {
                        id,
                        i1d = tChannel.Id
                    });
                    return Created(url, _mapper.Map<TChannelModel>(tChannel));
                }
                else
                {
                    return BadRequest("Failed to save new record.");
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to connect to database");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TChannelModel>> Put(string  id, TChannelModel model)
        {
            try
            {
                var record = await _repository.GetTChannelAsync(id,false);
                if (record == null) return NotFound("Couldn't find the record");


                _mapper.Map(model, record);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<TChannelModel>(record);
                }
                return BadRequest();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to connect to database");
            }
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id) {

            try
            {
                var oldTChannel = await _repository.GetTChannelAsync(id);
                if (oldTChannel == null) return NotFound();

                _repository.Delete(oldTChannel);
                if (await _repository.SaveChangesAsync()) {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
            return BadRequest();
        }
    }
}
