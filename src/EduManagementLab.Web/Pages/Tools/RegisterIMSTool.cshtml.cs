using EduManagementLab.Core.Entities;
using EduManagementLab.Core.Services;
using EduManagementLab.IdentityServer;
using IdentityModel;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EduManagementLab.Web.Pages.Tools
{
    public class RegisterIMSToolModel : PageModel
    {
        private readonly IMSToolService _IIMSToolService;
        public RegisterIMSToolModel(IMSToolService IIMSToolService)
        {
            _IIMSToolService = IIMSToolService;
        }
        [BindProperty]
        public ToolModel tool { get; set; } = new ToolModel();
        private readonly string ToolPublicKey = Config.ToolPublicKey;
        public class ToolModel
        {
            public int IdentityServerClientId { get; set; }
            [Required]
            [Display(Name = "Client ID")]
            public string ClientId { get; set; }
            [Required]
            [Display(Name = "Public Key", Description = "Public key to validate messages signed by the tool.")]
            public string PublicKey { get; set; }
            [Display(Name = "Custom Properties", Description = "Custom properties to include in all launches of this tool deployment.")]
            public string? CustomProperties { get; set; }

            [Display(Name = "Deep Linking Launch URL", Description = "The URL to launch the tool's deep linking experience.")]
            public string DeepLinkingLaunchUrl { get; set; }

            [Display(Name = "Deployment ID", Description = "Unique id assigned to this tool deployment.")]
            public string DeploymentId { get; set; }

            [Required]
            [Display(Name = "Launch URL", Description = "The URL to launch the tool.")]
            public string LaunchUrl { get; set; }

            [Required]
            [Display(Name = "Login URL", Description = "The URL to initiate OpenID Connect authorization.")]
            public string LoginUrl { get; set; }

            [Required]
            [Display(Name = "Display Name")]
            public string Name { get; set; }
            public IdentityServer4 IdentityServer { get; set; } = new IdentityServer4();
        }
        public class IdentityServer4
        {
            /// <summary>
            /// Identity Server issuer uri
            /// </summary>
            [Display(Name = "Issuer")]
            public string? Issuer { get; set; }

            /// <summary>
            /// Identity Server authorize endpoint url
            /// </summary>
            [Display(Name = "Authorize URL")]
            public string? AuthorizeUrl { get; set; }

            /// <summary>
            /// Identity Server JWK Set endpoint url
            /// </summary>
            [Display(Name = "JWK Set URL")]
            public string? JwkSetUrl { get; set; }

            /// <summary>
            /// Identity Server access token endpoint uri
            /// </summary>
            [Display(Name = "Access Token URL")]
            public string? TokenUrl { get; set; }
        }

        public void OnGet()
        {
            loadStaticToolInfo();
        }
        public void loadStaticToolInfo()
        {
            tool.ClientId = "IMSTool";
            //tool.Name = "EduLabTool";
            //tool.LaunchUrl = "https://localhost:44308/Tool/324a8c8e865788b5";
            //tool.DeepLinkingLaunchUrl = "https://localhost:44308/Tool/324a8c8e865788b5";
            //tool.LoginUrl = "https://localhost:44308/OidcLogin";
            tool.DeploymentId = "Key1";
            tool.PublicKey = ToolPublicKey;

            tool.IdentityServer.Issuer = "https://localhost:5001";
            tool.IdentityServer.AuthorizeUrl = "https://localhost:5001/connect/authorize";
            tool.IdentityServer.JwkSetUrl = "https://localhost:5001/.well-known/openid-configuration/jwks";
            tool.IdentityServer.TokenUrl = "https://localhost:5001/connect/token";
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {                
                if (Config.Clients.Any(c => c.ClientId == tool.ClientId && c.ClientSecrets.Any(c => c.Value == tool.PublicKey)))
                {
                    var newTool = new IMSTool
                    {
                        CustomProperties = tool.CustomProperties,
                        DeepLinkingLaunchUrl = tool.DeepLinkingLaunchUrl,
                        DeploymentId = tool.DeploymentId,
                        IdentityServerClientId = tool.ClientId,
                        Name = tool.Name,
                        LaunchUrl = tool.LaunchUrl,
                        LoginUrl = tool.LoginUrl,
                    };
                    _IIMSToolService.CreateTool(newTool);
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Client not found");
                    return Page();
                }
            }
            loadStaticToolInfo();
            return Page();
        }
    }
}
