//using Microsoft.AspNetCore.Authentication;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using ProductManagement.Data;
//using System.Security.Claims;
//using System.Text;
//using System.Text.Encodings.Web;

//namespace ProductManagement.Authentication
//{
//    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
//    {
//        private readonly AppDbContext _context;

//        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, AppDbContext context) : base(options, logger, encoder)
//        {
//            _context = context;
//        }

//        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
//        {
//            // هنا بتاكد هل اليوزر باعت الداتا بتاع ال authentication اصلا ولا لا هل الهيدر بتاع ال authorization اتبعت ولا لا
//            if (!Request.Headers.ContainsKey("Authorization"))
//                return await Task.FromResult(AuthenticateResult.NoResult());
//            //باخد الداتا الى مبعوته فى الهيدر واحولها ل string
//            var authHeader = Request.Headers["Authorization"].ToString();
//            // بتاكد من ال scheme هل نفس اللى انا مستنياها ولا لا
//            if (!authHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
//                return await Task.FromResult(AuthenticateResult.Fail("Unknown Scheme"));
//            //باخد جزء اليوزر والباسوورد بس
//            var encodedCredentials = authHeader["Basic ".Length..];

//            //بعمله decoding واحوله ل string عادى
//            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));

//            // بعمل split فى اراى 
//            var userNameAndPassword = decodedCredentials.Split(':');

//            if (await _context.users.AnyAsync(u => u.UserName == userNameAndPassword[0] && u.Password == userNameAndPassword[1]))
//            {
//                var identity = new ClaimsIdentity(new Claim[]
//                {
//                    new Claim(ClaimTypes.Name, userNameAndPassword[0])
//                }, "Basic");

//                var principal = new ClaimsPrincipal(identity); 
//                var ticket = new AuthenticationTicket(principal,"Basic");
//                return await Task.FromResult(AuthenticateResult.Success(ticket));
//            }
//            else
//                return await Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
//        }
//    }
//}
