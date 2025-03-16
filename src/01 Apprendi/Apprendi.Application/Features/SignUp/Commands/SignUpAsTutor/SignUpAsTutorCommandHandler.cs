using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.SignUp.Commands.SignUpAsTutor;
using Microsoft.Graph;
using Microsoft.Graph.Models.ODataErrors;

namespace Apprendi.Application.Features.SignUp.Commands.RegisterUserAsTutor
{
    public class SignUpAsTutorCommandHandler : RequestHandler<SignUpAsTutorCommand, SignUpAsTutorCommandResponse>
    {
        private readonly IApprendiDbContext _context;
        private readonly ISignupMapper _signupMapper;
        private readonly GraphServiceClient _graphServiceClient;

        public SignUpAsTutorCommandHandler(IResponseFactory<SignUpAsTutorCommandResponse> responseFactory,
                                          IApprendiDbContext context,
                                          ISignupMapper signupMapper,
                                          GraphServiceClient graphServiceClient) : base(responseFactory)
        {
            _context = context;
            _signupMapper = signupMapper;
            _graphServiceClient = graphServiceClient;
        }

        private async Task<Microsoft.Graph.Models.User> CreateGraphUser(SignUpAsTutorCommand request)
        {
            var graphUser = _signupMapper.ToGraphUser(request);
            return await _graphServiceClient.Users.PostAsync(graphUser);
        }

        public override async Task<SignUpAsTutorCommandResponse> Handle(SignUpAsTutorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transation = await _context.BeginTransactionAsync(cancellationToken);

                var user = _signupMapper.ToUserEntity(request);
                var tutor = _signupMapper.ToTutorEntity(user);

                await _context.Tutors.AddAsync(tutor, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var graphUser = await CreateGraphUser(request);

                await transation.CommitAsync(cancellationToken);

                return Response.Success();
            }
            catch (ODataError e) when (IsWeakPassword(e))
            {
                var message = "Please choose a stronger password";
                return Response.BadRequest(message);
            }
            catch (ODataError e) when (UserAlreadyExists(e))
            {
                var message = "There is already an account with this email";
                return Response.BadRequest(message);
            }
        }

        private static bool IsWeakPassword(ODataError e)
        {
            return e.Message == "The specified password does not comply with password complexity requirements. Please provide a different password.";
        }

        private static bool UserAlreadyExists(ODataError e)
        {
            return e.Error.Details.Any(x => x.Code == "ObjectConflict");
        }
    }
}
