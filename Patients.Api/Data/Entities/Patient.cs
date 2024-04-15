using Patients.Api.Attributes;

namespace Patients.Api.Data.Entities;

[MongoCollectionName("patients")]
public class Patient : EntityBase
{
    public string? Use { get; set; }
    public string Family { get; set; } = string.Empty;
    public List<string> Given { get; set; } = new();
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Active { get; set; }
}

public enum Gender
{
    Unknown = 0,
    Male = 1,
    Female = 2,
    Other = 3,
}