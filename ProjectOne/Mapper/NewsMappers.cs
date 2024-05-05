
using ProjectFuse.Models;
using ProjectFuse.ViewModels.News;
using Riok.Mapperly.Abstractions;

namespace ProjectFuse.Mapper;

[Mapper]
public static partial class NewsMapper
{
    public static partial NewsSettingsViewModel MapToNewsSettingsViewModel(NewsApiSettingsModel settingsModel);
    
    public static partial NewsApiSettingsModel MapToNewsApiSettingsModel(NewsSettingsViewModel settingsViewModel);
}