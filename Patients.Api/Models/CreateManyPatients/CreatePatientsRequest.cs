namespace Patients.Api.Models.CreateManyPatients;

public class CreatePatientsRequest
{
    public List<CreatePatientsItem> Items { get; set; }
}

public class CreatePatientsItem
{
    public Guid Guid { get; set; }
    public string Family { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string? Use { get; set; }
    public List<string>? Given { get; set; }
    public bool Active { get; set; }
    public string? Gender { get; set; }
}