using Microsoft.AspNetCore.Builder;

namespace TaskManager.DeveloperEvaluation.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
