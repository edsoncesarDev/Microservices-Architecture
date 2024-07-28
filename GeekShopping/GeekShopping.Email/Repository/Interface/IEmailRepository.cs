using GeekShopping.Email.Dto;

namespace GeekShopping.Email.Repository.Interface;

public interface IEmailRepository
{
    Task LogEmail(UpdatePaymentResultDto message);
}
