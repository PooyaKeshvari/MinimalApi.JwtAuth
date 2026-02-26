namespace MinimalApi.JwtAuth.Composition;

public static class PipelineExtensions
{
    public static WebApplication UseMinimalJwtAuth(this WebApplication app)
    {
        app.UseExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHealthChecks("/health");
        app.MapGet("/", () => Results.Ok(new { service = "MinimalApi.JwtAuth", status = "Running" }));

        return app;
    }
}
