using GreenNote.Auth.Models;

namespace GreenNote.Auth.Services.IServices
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
