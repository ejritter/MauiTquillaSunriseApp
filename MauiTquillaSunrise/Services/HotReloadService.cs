#if DEBUG
[assembly:System.Reflection.Metadata.MetadataUpdateHandlerAttribute(typeof(MauiTquillaSunrise.Services.HotReloadService))]
namespace MauiTquillaSunrise.Services;
public static class HotReloadService
{
    public static event Action<Type[]?>? UpdateApplicationEvent;

    internal static void ClearCache(Type[]? types) { }

    internal static void UpdateApplication(Type[]? types)
    {
        UpdateApplicationEvent?.Invoke(types);
    }
}
#endif