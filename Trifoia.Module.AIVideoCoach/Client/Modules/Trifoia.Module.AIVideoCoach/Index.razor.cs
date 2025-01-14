using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Shared;
using Oqtane.Services;

using Trifoia.Module.AIVideoCoach.Services;

namespace Trifoia.Module.AIVideoCoach;

public partial class Index : ModuleBase
{
    List<Models.AIVideoCoach> _AIVideoCoachs;
		
    [Inject] public AIVideoCoachService AIVideoCoachService { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IStringLocalizer<Index> Localizer { get; set; }
    [Inject] public ISettingService SettingService { get; set; }
	
    public override List<Resource> Resources => new List<Resource>()
    {
        new Resource { ResourceType = ResourceType.Stylesheet,  Url = "https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap" },
        new Resource { ResourceType = ResourceType.Stylesheet,  Url = "_content/MudBlazor/MudBlazor.min.css" },
        new Resource { ResourceType = ResourceType.Stylesheet,  Url = ModulePath() + "Module.css" },
        new Resource { ResourceType = ResourceType.Script,      Url = "_content/MudBlazor/MudBlazor.min.js", Location = ResourceLocation.Body, Level = ResourceLevel.Site },
        new Resource { ResourceType = ResourceType.Script,      Url = ModulePath() + "Module.js" },
    };	
    private bool IsLoaded;
    private SettingsViewModel _settingsVM; 

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var moduleSettings = await SettingService.GetModuleSettingsAsync(ModuleState.ModuleId);
            _settingsVM = new SettingsViewModel(SettingService, moduleSettings);
            (_AIVideoCoachs, var code) = await AIVideoCoachService.GetAIVideoCoachsAsync();
            if (!IsSuccessStatusCode(code)) {
                throw new HttpRequestException($"Error loading AIVideoCoachs. Code: {code}");
            }

            IsLoaded = true;
        }
        catch (Exception ex)
        {
            await logger.LogError(ex, "Error Loading AIVideoCoach {Error}", ex.Message);
            AddModuleMessage(Localizer["Message.LoadError"], MessageType.Error);
        }
    }

    private async Task Delete(Models.AIVideoCoach AIVideoCoach)
    {
        try
        {
            var code = await AIVideoCoachService.DeleteAIVideoCoachAsync(AIVideoCoach.AIVideoCoachId);
            if (!IsSuccessStatusCode(code)) {
                throw new HttpRequestException($"Error Deleting AIVideoCoachs. id:{AIVideoCoach.AIVideoCoachId}, Code: {code}");
            }
            await logger.LogInformation("AIVideoCoach Deleted {AIVideoCoach}", AIVideoCoach);

            (_AIVideoCoachs, code ) = await AIVideoCoachService.GetAIVideoCoachsAsync();

            StateHasChanged();
        }
        catch (Exception ex)
        {
            await logger.LogError(ex, "Error Deleting AIVideoCoach {AIVideoCoach} {Error}", AIVideoCoach, ex.Message);
            AddModuleMessage(Localizer["Message.DeleteError"], MessageType.Error);
        }
    }

     static bool IsSuccessStatusCode(HttpStatusCode statusCode) { 
        return (int)statusCode >= 200 && (int)statusCode <= 299; 
    }
}

