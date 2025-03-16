using Apprendi.Application.Features.SignUp;
using Apprendi.Application.Features.Users.Commands.AddUser;
using Apprendi.Domain.Entities;

namespace Apprendi.Application.Features.Users
{
    public interface IUserMapper
    {
        UserDto ToDto(User user);
        List<UserDto> ToDtos(ICollection<User> users);
        User ToEntity(AddUserCommand request);
    }

    public static class UserMapperProjector
    {
        private static IQueryable<SpokenLanguageDto> ProjectToDto(this IEnumerable<SpokenLanguage> languages)
        {
            return languages.AsQueryable().Select(language => new SpokenLanguageDto
            {
                LanguageId = language.LanguageId,
                LevelId = language.ProficiencyLevelId
            });
        }

        public static IQueryable<UserDto> ProjectToDto(this IQueryable<User> users)
        {
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleIds = user.Roles.Select(x => x.RoleId).ToList(),
                CurrencyId = user.CurrencyId,
                TimeZoneId = user.TimeZoneId
            });
        }

        public static IQueryable<TutorInformationDto> ProjectToTutorInformationDto(this IQueryable<Tutor> users)
        {
            return users.Select(tutor => new TutorInformationDto
            {
                TutorId = tutor.Id,
                TutorSignUpStage = tutor.SignUpStage,
            });
        }

        public static IQueryable<TutorAboutStageDetailsDto> ProjectToAboutDetailsDto(this IQueryable<Tutor> tutors)
        {
            return tutors.Select(tutor => new TutorAboutStageDetailsDto
            {
                FirstName = tutor.User.FirstName,
                LastName = tutor.User.LastName,
                CountryOfBirthId = tutor.CountryOfBirthId,
                IsOver18 = tutor.IsOver18,
                SpokenLanguages = tutor.SpokenLanguages.ProjectToDto().ToList(),
                SubjectsTaughtIds = tutor.TeachingSubjects.Select(subject => subject.SubjectId).ToList()
            });
        }
    }

    public class UserMapper : IUserMapper
    {   
        public UserDto ToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public List<UserDto> ToDtos(ICollection<User> users)
        {
            return users.Select(ToDto).ToList();
        }

        public User ToEntity(AddUserCommand request)
        {
            return new User
            {
                FirstName = request.FirstName,
                Email = request.Email
            };
        }
    }
}
