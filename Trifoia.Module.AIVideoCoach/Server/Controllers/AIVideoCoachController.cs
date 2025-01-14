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

    
}