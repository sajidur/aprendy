using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Apprendi.Application.Features.SignUp.Commands.SignUpAsStudent;
using Microsoft.Graph;
using Microsoft.Graph.Models.ODataErrors;

namespace Apprendi.Application.Features.SignUp.Commands.RegisterUserAsStudent
{
    public class SignUpAsStudentCommandHandler : RequestHandler<SignUpAsStudentCommand, SignUpAsStudentCommandResponse>
    {
        private readonly ISignupMapper _signupMapper;
        private readonly IApprendiDbContext _context;
        private readonly GraphServiceClient _graphServiceClient;

        public SignUpAsStudentCommandHandler(IResponseFactory<SignUpAsStudentCommandResponse> responseFactory,
                                             ISignupMapper signupMapper,
                                             IApprendiDbContext context,
                                             GraphServiceClient graphServiceClient) : base(responseFactory)
        {
            _signupMapper = signupMapper;
            _context = context;            
            _graphServiceClient = graphServiceClient;
        }

        private async Task<Microsoft.Graph.Models.User> CreateGraphUser(SignUpAsStudentCommand request)
        {
            var graphUser = _signupMapper.ToGraphUser(request);
            return await _graphServiceClient.Users.PostAsync(graphUser);
        }

        public override async Task<SignUpAsStudentCommandResponse> Handle(SignUpAsStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var id = "70395462-a3d1-4c89-bef8-1e1a98628107@aprendyapp.onmicrosoft.com";
                var issuerAssignedId = "nada778899@yopmail.com";
                var result = await _graphServiceClient
                    .Users
                    .GetAsync(requestConfiguration =>
                    {
                        var issuer = "aprendyapp.onmicrosoft.com";

                        requestConfiguration.QueryParameters.Filter =
                            $"identities/any(i:i/issuer eq '{issuer}' and i/issuerAssignedId eq '{issuerAssignedId}')";
                    });
                var principleName = result.Value.FirstOrDefault().UserPrincipalName;



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
