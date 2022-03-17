using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Sample.Domain.Contacts
{
    public class Contact
    {
        private const int MAX_NAME_LENGTH = 300;
        private const string PHONE_NUMBER_EXPRESSION = @"^((((0{2}?)|(\+){1})46)|0)7[\d]{8}";
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public string? Email { get; private set; }
        public string? PhoneNumber { get; private set; }

        public Contact(Guid id, string name, string email, string phoneNumber)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            if (phoneNumber == PhoneNumber) return;

            if (Regex.IsMatch(phoneNumber, PHONE_NUMBER_EXPRESSION))
            {
                PhoneNumber = phoneNumber;
            }
            else
            {
                throw new InvalidOperationException($"New phonenumber is invalid");
            }
        }

        public void ChangeEmailAddress(string email)
        {
            if (email == Email) return;
            if (MailAddress.TryCreate(email, out MailAddress? address))
            {
                Email = email;
            }
            else
            {
                throw new InvalidOperationException($"New email address is invalid");
            }
        }

        public void ChangeName(string name)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));

            if (name.Length > MAX_NAME_LENGTH)
            {
                throw new InvalidOperationException($"Name cannot be longer than {MAX_NAME_LENGTH}");
            }

            Name = name;
        }

        public static Contact New(string name, string? email, string? phoneNumber)
        {
            return new Contact(Guid.NewGuid(), name, email, phoneNumber);
        }
    }
}
