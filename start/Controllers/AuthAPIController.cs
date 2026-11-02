using AtlasBlog.Data;
using AtlasBlog.Models;
using AtlasBlog.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AtlasBlog.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthAPIController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly string _apiKey;

    public AuthAPIController(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _context = context;

        _apiKey = configuration["ApiSettings:ApiKey"] ?? "";
    }

    private bool IsValidApiKey(string apiKey)
    {
        return apiKey == _apiKey;
    }

[HttpPost("registeradmin")]
public async Task<IActionResult> RegisterAdmin(
    RegisterAdminDTO model)
{
    if (!IsValidApiKey(model.ApiKey))
        return Unauthorized("Invalid API Key");

    var user = new ApplicationUser
    {
        UserName = model.Username,
        Email = model.Username
    };

    var result = await _userManager.CreateAsync(
        user,
        model.Password);

    if (!result.Succeeded)
        return BadRequest(result.Errors);

    await _userManager.AddClaimAsync(
        user,
        new Claim("admin", "true"));

    return Ok("Admin Created");
}


[HttpPost("registermoderator")]
public async Task<IActionResult> RegisterModerator(
    RegisterModeratorDTO model)
{
    if (!IsValidApiKey(model.ApiKey))
        return Unauthorized("Invalid API Key");

    var user = new ApplicationUser
    {
        UserName = model.Username,
        Email = model.Username
    };

    var result = await _userManager.CreateAsync(
        user,
        model.Password);

    if (!result.Succeeded)
        return BadRequest(result.Errors);

    await _userManager.AddClaimAsync(
        user,
        new Claim("moderator", "true"));

    return Ok("Moderator Created");
}
[HttpPost("registerauthor")]
public async Task<IActionResult> RegisterAuthor(
    RegisterAuthorDTO model)
{
    if (!IsValidApiKey(model.ApiKey))
        return Unauthorized("Invalid API Key");

    var user = new ApplicationUser
    {
        UserName = model.Username,
        Email = model.Username
    };

    var result = await _userManager.CreateAsync(
        user,
        model.Password);

    if (!result.Succeeded)
        return BadRequest(result.Errors);

    await _userManager.AddClaimAsync(
        user,
        new Claim("author", "true"));

    var blog = new Blog
    {
        Title = model.BlogTitle,
        Subtitle = model.Subtitle,
        Description = model.Description,
        CreatedAt = DateTime.Now,
        LastModifiedAt = DateTime.Now,
        AuthorId = user.Id
    };

    _context.Blogs.Add(blog);

    await _context.SaveChangesAsync();

    return Ok("Author and Blog Created");
}


}