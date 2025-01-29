namespace petchat.Interfaces
{
    public interface IPasswordService
    {
        public void CreatePasswordHash(string password, out byte[] hash, out byte[] salt);
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
