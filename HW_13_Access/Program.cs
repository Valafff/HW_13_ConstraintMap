var builder = WebApplication.CreateBuilder();
builder.Services.AddRouting(options =>
                options.ConstraintMap.Add("ban", typeof(BanVerification)));
var app = builder.Build();

app.Map("/users/{user:ban}", (string user) => $"Not banned user: {user}");
app.Map("/", () => "Start Page");

app.Run();

public class BanVerification : IRouteConstraint
{
    List<string> BannedList = new List<string> { "test1", "test2", "test3" };
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
        RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (!BannedList.Contains(values[routeKey]?.ToString()))
        {
            return true;
        }
        else
        {
            httpContext.Response.WriteAsync("Access denied");
            return false;
        }
         
    }
}

