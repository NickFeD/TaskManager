namespace TaskManager.Core.Contracts.Services;

public interface IEncryptService
{
    byte[] GenerateSalt();
    byte[] HashPassword(string password, byte[] salt);
}