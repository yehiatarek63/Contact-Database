using EdgeDB;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace ContactDatabase;

[EdgeDBType]
public class Contact
{
    public Guid Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public TitleState? Title { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public DateTimeOffset DateOfBirth { get; set; }
    [Required]
    public bool? MarriageStatus { get; set; }
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public RoleState? Roles { get; set; }
    public byte[]? Salt { get; set; }
    public static byte[] GenerateSalt()
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        return salt;
    }
    public static string HashPassword(string password, byte[] salt)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                       password: password,
                       salt: salt,
                       prf: KeyDerivationPrf.HMACSHA512,
                       iterationCount: 10000,
                       numBytesRequested: 256 / 8));
        return hashed;
    }

}
public enum TitleState
{
    Mr,
    Mrs,
    Miss,
    Dr,
    Prof
}

public enum RoleState
{
    User,
    Admin
}


