using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Trifoia.Module.AIVideoCoach.Repository;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Trifoia.Module.AIVideoCoach.Controllers;

[Route(ControllerRoutes.ApiRoute)]
public class AIVideoCoachController : ModuleControllerBase
{
    private readonly AIVideoCoachRepository _AIVideoCoachRepository;

    public AIVideoCoachController(AIVideoCoachRepository AIVideoCoachRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
    {
        _AIVideoCoachRepository = AIVideoCoachRepository;
    }

    // GET: api/<controller>?moduleid=x
    [HttpGet]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<IEnumerable<Models.AIVideoCoach>>> Get(){
        try{
            var data = _AIVideoCoachRepository.GetAIVideoCoachs();
            return Ok(data);
        }
        catch(Exception ex){
            var errorMessage = $"Repository Error Get Attempt AIVideoCoach";
            _logger.Log(LogLevel.Error, this, LogFunction.Read, errorMessage);
            return StatusCode(500);
        }
    }

    // GET api/<controller>/5
    [HttpGet("{id}")]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<Models.AIVideoCoach>> Get(int id)
    {
        try {
            var data = _AIVideoCoachRepository.GetAIVideoCoach(id);
            return Ok(data);
        }
        catch (Exception ex)       { 
            _logger.Log(LogLevel.Error, this, LogFunction.Read, "Failed AIVideoCoach Get Attempt {id}", id);
            return StatusCode(500);
        }
    }

    // POST api/<controller>
    [HttpPost]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<Models.AIVideoCoach>> Post([FromBody] Models.AIVideoCoach item)
    {
        if (ModelState.IsValid)
        {
            try{
                item = _AIVideoCoachRepository.AddAIVideoCoach(item);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "AIVideoCoach Added {AIVideoCoach}", item);
            }
            catch (Exception ex) {
                _logger.Log(LogLevel.Error, this, LogFunction.Read, "Failed AIVideoCoach Add Attempt {item} Message {Message} ", item, ex.Message);
                return StatusCode(500);
            }
        }
        else
        {
            _logger.Log(LogLevel.Error, this, LogFunction.Create, "Invaid AIVideoCoach Post Attempt {item}", item);
            return BadRequest();
        }
        return Ok(item);
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult<Models.AIVideoCoach>> Put(int id, [FromBody] Models.AIVideoCoach item)
    {
        if (ModelState.IsValid && _AIVideoCoachRepository.GetAIVideoCoach(item.AIVideoCoachId, false) != null)
        {
            item = _AIVideoCoachRepository.UpdateAIVideoCoach(item);
            _logger.Log(LogLevel.Information, this, LogFunction.Update, "AIVideoCoach Updated {item}", item);
            return Ok(item);
        }
        else
        {
            _logger.Log(LogLevel.Error, this, LogFunction.Update, "Unauthorized AIVideoCoach Put Attempt {item}", item);
            return BadRequest();
        }
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = RoleNames.Registered)]
    public async Task<ActionResult> Delete(int id)
    {
        var data = _AIVideoCoachRepository.GetAIVideoCoach(id);
        if (data is null)
        {
            _logger.Log(LogLevel.Error, this, LogFunction.Delete, "Failed AIVideoCoach Delete Attempt {AIVideoCoachId}", id);
            return NotFound();
        }

        _AIVideoCoachRepository.DeleteAIVideoCoach(id);
        _logger.Log(LogLevel.Information, this, LogFunction.Delete, "AIVideoCoach Deleted {AIVideoCoachId}", id);
        return Ok();
    
    }
}