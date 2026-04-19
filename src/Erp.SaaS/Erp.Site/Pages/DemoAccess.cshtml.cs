using Erp.Application.DemoAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Erp.Site.Pages;

public sealed class DemoAccessModel : PageModel
{
    private readonly IDemoAccessService _demoAccessService;

    public DemoAccessModel(IDemoAccessService demoAccessService)
    {
        _demoAccessService = demoAccessService;
    }

    [BindProperty]
    public CreateDemoAccessRequest Form { get; set; } = new();

    public bool Submitted { get; private set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            await _demoAccessService.CaptureRequestAsync(Form);
            Submitted = true;
            Form = new CreateDemoAccessRequest();
            ModelState.Clear();
            return Page();
        }
        catch (InvalidOperationException exception)
        {
            ErrorMessage = exception.Message;
            return Page();
        }
    }
}
