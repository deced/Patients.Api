using Patients.Api.Data.Entities;

namespace Patients.Api.Models.Shared;

public class PatientModel
{
    public NameModel Name { get; set; }
    public string Gender { get; set; }
    public string BirthDate { get; set; }
    public bool Active { get; set; }

    public PatientModel(Patient patient)
    {
        Gender = patient.Gender.ToString().ToLower();
        BirthDate = patient.BirthDate.ToString("s");
        Active = patient.Active;

        Name = new NameModel(patient);
    }
}

public class NameModel
{
    public Guid Id { get; set; }
    public string? Use { get; set; }
    public string Family { get; set; }
    public List<string> Given { get; set; }


    public NameModel(Patient patient)
    {
        Id = patient.Id;
        Use = patient.Use;
        Family = patient.Family;
        Given = patient.Given;
    }
}