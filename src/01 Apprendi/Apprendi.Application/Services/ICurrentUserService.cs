namespace Apprendi.Application.Common.Services
{
    public interface ICurrentUserService
    {
        public string User { get; }
    }

    public class CurrentUserService : ICurrentUserService
    {
        public string User { get; set; } = "DummyUserId";
    }
}
