using System.Net.Mail;

namespace GeekShopping.IdentityUserAPI.Helpers;

public static class EmailValidation
{
    public static bool IsEmail(string email)
    {
		try
		{
            var model = new MailAddress(email);
            return false;
        }
		catch (Exception)
		{
			return true;
		}
    }
}
