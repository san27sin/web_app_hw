using FluentValidation;
using web_app_hw.Models.Dto;

namespace web_app_hw.Models
{
    //создаем класс и наследуемся от абстрактного класса
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {

        public AuthenticationRequestValidator()
        {
            RuleFor(x => x.Login)//выставляем характеристики для валидации, чтобы отсеить до подключения к БД
                .NotNull()
                .Length(7, 255)
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull()
                .Length(5, 30);
        }
    }
}
