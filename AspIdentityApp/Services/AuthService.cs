using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AspIdentityApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthService(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<LoginResponse> LoginAsync(LoginModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {                    
                    new Claim(ClaimTypes.Email, user.Email),                   
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                  
                }

                var token = GetToken(authClaims);

                Log.Information($"Logging {request.Email} was succeeded");
                return new LoginResponse()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }

            Log.Error($"User with e-mail: {request.Email} not found or password is wrong");
            return null;
        }

        public async Task<Response> RegisterAsync(RegisterModel request)
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists is not null)
            {
                Log.Error($"Register failed. User with e-mail: {request.Email} already exists");
                return new Response { Status = "Error", Message = "User already exists!" };
            }
                
            var user = new IdentityUser()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                Log.Error($"Register {request.Email} was failed");
                return new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." };
            }
                
            Log.Information($"Register {request.Email} was succeeded");
            await _userManager.AddToRoleAsync(user, UserRoles.User);
            return new Response { Status = "Success", Message = "User created successfully!" };
        }

        private  JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:JwtExpireDays"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            Log.Information("Create JWT token");
            return token;
        }
    }
}
