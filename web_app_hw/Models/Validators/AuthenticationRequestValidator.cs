using FluentValidation;
using web_app_hw.Models.Request;

namespace web_app_hw.Models.Validators
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
