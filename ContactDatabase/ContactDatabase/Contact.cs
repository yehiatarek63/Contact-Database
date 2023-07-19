using EdgeDB;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ContactDatabase;

[EdgeDBType]
public class Contact
{
    [EdgeDBProperty("first_name")]
    [Required]
    public string? FirstName { get; set; }
    [EdgeDBProperty("last_name")]
    [Required]
    public string? LastName { get; set; }
    [EdgeDBProperty("email")]
    [Required]
    public string? Email { get; set; }
    [EdgeDBProperty("title")]
    [Required]
    public TitleState? Title { get; set; }
    [EdgeDBProperty("description")]
    [Required]
    public string? Description { get; set; }
    [EdgeDBProperty("date_of_birth")]
    [Required]
    public DateTimeOffset BirthDate { get; set; }
    [EdgeDBProperty("marriage_status")]
    [Required]
    public bool? IsMarried { get; set; }

}
public enum TitleState
{
    Mr,
    Mrs,
    Miss,
    Dr,
    Prof
}
