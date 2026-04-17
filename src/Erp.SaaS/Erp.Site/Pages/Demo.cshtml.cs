using Erp.Application.Leads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Erp.Site.Pages;

public sealed class DemoModel : PageModel
{
    private readonly ILeadCaptureService _leadCaptureService;

    public DemoModel(ILeadCaptureService leadCaptureService)
    {
        _leadCaptureService = leadCaptureService;
    }

    [BindProperty]
    public CreateLeadRequest Lead { get; set; } = new();

    public bool Submitted { get; private set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Lead.ContactName) ||
            string.IsNullOrWhiteSpace(Lead.CompanyName) ||
            string.IsNullOrWhiteSpace(Lead.Email) ||
            Lead.RequestedUsers <= 0)
        {
            ErrorMessage = "Nombre, empresa, email y número de usuarios son obligatorios.";
            return Page();
        }

        await _leadCaptureService.CaptureAsync(Lead);
        Submitted = true;
        Lead = new CreateLeadRequest();
        ModelState.Clear();
        return Page();
    }
}
