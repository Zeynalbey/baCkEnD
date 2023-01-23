using Backend_Final.Contracts.Email;

namespace Backend_Final.Services.Abstracts
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}
