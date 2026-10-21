using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace PolicyPilot.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(Kernel kernel) : ControllerBase
{
    private const string TestPrompt =
        "Hello! Confirm that you are ready to serve as the AI engine for PolicyPilot insurance queries.";

    [HttpGet("chat")]
    public async Task<IActionResult> Chat(CancellationToken cancellationToken)
    {
        try
        {
            var chatCompletion = kernel.GetRequiredService<IChatCompletionService>();

            var response = await chatCompletion.GetChatMessageContentAsync(
                TestPrompt,
                cancellationToken: cancellationToken);

            return Ok(new
            {
                prompt = TestPrompt,
                reply = response.Content
            });
        }
        catch(Exception ex)
        {
            return null;
        }
        
    }
}
