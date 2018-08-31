namespace Identity.Application.Service
{
    using Identity.Application.Dto.Input;
    using Identity.Application.Dto.Output;
    using System.Threading.Tasks;

    public interface IAuthApplicationService
    {
        Task<JwTokenDto> PerformAuthentication(LoginDto login);
    }
}
