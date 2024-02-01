using System.Reflection.PortableExecutable;
using Microsoft.Extensions.Primitives;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET" && context.Request.Path == "/")
    {
        //Convert querystring string format into dictionary
        var reader = context.Request.QueryString.ToString();
        Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(reader);

        //check if querystring contains 3 keys
        if (context.Request.Query.ContainsKey("firstnumber") &&
        context.Request.Query.ContainsKey("secondnumber") &&
        context.Request.Query.ContainsKey("operation"))
        {
            // convert to int
            long firstNumber = int.Parse(context.Request.Query["firstnumber"]);
            long secondNumber = int.Parse(context.Request.Query["secondnumber"]);
            string operation = context.Request.Query["operation"];

            //return responce from server according to operation request
            switch (operation)
            {
                case "add":
                    long addResult = firstNumber + secondNumber;
                    await context.Response.WriteAsync(addResult.ToString());
                    break;
                case "subtract":
                    long subtractResult = firstNumber - secondNumber;
                    await context.Response.WriteAsync(subtractResult.ToString());
                    break;
                case "multiply":
                    long multiplyResult = firstNumber * secondNumber;
                    await context.Response.WriteAsync(multiplyResult.ToString());
                    break;
                case "divide":
                    long divideResult = firstNumber + secondNumber;
                    await context.Response.WriteAsync(divideResult.ToString());
                    break;

                default:
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid operation");
                    break;
            }
        }
        else if (!queryDict.ContainsKey("firstnumber") &&
        !queryDict.ContainsKey("secondnumber") &&
        !queryDict.ContainsKey("operation"))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid input for 'firstnumber'");
            await context.Response.WriteAsync("Invalid input for 'secondnumber'");
            await context.Response.WriteAsync("Invalid input for 'operation'");
        }
        else
        {
            context.Response.StatusCode = 400;
            if (!queryDict.ContainsKey("firstnumber"))
            {
                await context.Response.WriteAsync("Invalid input for 'firstnumber'");
            }
            if (!queryDict.ContainsKey("secondnumber"))
            {
                await context.Response.WriteAsync("Invalid input for 'secondnumber'");
            }
            if (!queryDict.ContainsKey("operation"))
            {
                await context.Response.WriteAsync("Invalid input for 'operation'");
            }
        }
    }
});

app.Run();


