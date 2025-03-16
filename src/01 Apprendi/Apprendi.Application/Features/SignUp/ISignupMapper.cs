using Apprendi.Application.Features.SignUp.Commands.SignUpAsStudent;
using Apprendi.Application.Features.SignUp.Commands.SignUpAsTutor;
using Apprendi.Application.Features.SignUp.Commands.UpdateTutorAbout;
using Apprendi.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.Graph.Models;
using GraphUser = Microsoft.Graph.Models.User;
using EntityUser = Apprendi.Domain.Entities.User;

namespace Apprendi.Application.Features.SignUp
{
    public interface ISignupMapper
    {
        void DtoToEntity(UpdateTutorAboutCommand command, Tutor tutor);
        EntityUser ToUserEntity(SignUpAsStudentCommand request);
        EntityUser ToUserEntity(SignUpAsTutorCommand request);
        Student ToStudentEntity(EntityUser user);
        Tutor ToTutorEntity(EntityUser user);
        GraphUser ToGraphUser(SignUpAsStudentCommand request);
        GraphUser ToGraphUser(SignUpAsTutorCommand request);
    }

    public class SignupMapperOptions
    {
        public string Issuer { get; set; }
    }

    public class SignupMapper : ISignupMapper
    {
        private readonly IOptions<SignupMapperOptions> _options;

        public SignupMapper(IOptions<SignupMapperOptions> options)
        {
            _options = options;
        }

        private static SpokenLanguage MapToEntity(Tutor tutor, SpokenLanguageDto language)
        {
            return new SpokenLanguage
            {
                TutorId = tutor.Id,
                LanguageId = language.LanguageId.Value,
                ProficiencyLevelId = language.LevelId.Value
            };
        }

        public void DtoToEntity(UpdateTutorAboutCommand command, Tutor tutor)
        {
            tutor.Id = command.TutorId;
            tutor.User.FirstName = command.FirstName;
            tutor.User.LastName = command.LastName;
            tutor.CountryOfBirthId = command.CountryOfBirthId;
            tutor.TeachingSubjects = command.SubjectsTaughtIds
                .Select(id => new TutorSubject 
                {
                    TutorId = tutor.Id,
                    SubjectId = id
                } )
                .ToList();
            tutor.SpokenLanguages = command.SpokenLanguages.Select(x => MapToEntity(tutor, x)).ToList();
            tutor.IsOver18 = command.IsOver18;
        }

        public GraphUser ToGraphUser(SignUpAsStudentCommand request)
        {
            var issuer = _options.Value.Issuer;
            return new GraphUser
            {
                AccountEnabled = true,
                DisplayName = request.FirstName,
                Identities = new List<ObjectIdentity>
                {
                    new ObjectIdentity
                    {
                        IssuerAssignedId = request.Email,
                        Issuer = issuer,
                        SignInType = "emailAddress"
                    }
                },
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = false,
                    Password = request.Password
                }
            };
        }

        public GraphUser ToGraphUser(SignUpAsTutorCommand request)
        {
            var issuer = _options.Value.Issuer;
            return new GraphUser
            {
                AccountEnabled = true,
                DisplayName = request.Email,
                Identities = new List<ObjectIdentity>
                {
                    new ObjectIdentity
                    {
                        IssuerAssignedId = request.Email,
                        Issuer = issuer,
                        SignInType = "emailAddress"
                    }
                },
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = false,
                    Password = request.Password
                }
            };
        }

        public Student ToStudentEntity(EntityUser user)
        {
            return new Student
            {
                User = user
            };
        }

        public Tutor ToTutorEntity(EntityUser user)
        {
            return new Tutor
            {
                User = user
            };
        }

        public EntityUser ToUserEntity(SignUpAsStudentCommand request)
        {
            var user = new EntityUser
            {
                CurrencyId = request.CurrencyId,
                Email = request.Email,
                FirstName = request.FirstName,
                LanguageId = request.LanguageId,
                TimeZoneId = request.TimeZoneId,
            };

            var studentRole = new UserRole
            {
                RoleId = Role.StudentRoleId,
                User = user,
                IsActive = true
            };

            user.Roles.Add(studentRole);

            return user;
        }

        public EntityUser ToUserEntity(SignUpAsTutorCommand request)
        {
            var user = new EntityUser
            {
                CurrencyId = request.CurrencyId,
                Email = request.Email,
                FirstName = " ",
                LanguageId = request.LanguageId,
                TimeZoneId = request.TimeZoneId,
            };

            var studentRole = new UserRole
            {
                RoleId = Role.TutorRoleId,
                User = user,
                IsActive = true
            };

            user.Roles.Add(studentRole);

            return user;
        }
    }
}

