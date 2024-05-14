
namespace Eshop.Service.src.DTO
{
    public class TokenDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public TokenDTO(
            string accessToken,
            string refreshToken
        )
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }

    public class RefreshTokenDTO
    {
        public string RefreshToken { get; set; }
    }
}
