using System.Net.Http.Json;
using Patients.Seed;

const int patientsCount = 100;

var baseAddress = Environment.GetEnvironmentVariable("PATIENTS_API_URL") ?? "http://localhost:5001/";

var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri(baseAddress);


var request = new CreatePatientsModel{ Items = new List<PatientModel>()
{
    // patient for update
    RandomDataProvider.GetRandomPatient(Guid.Parse("ff84b262-e615-4613-8d93-ea2ca723a39b")),
    // patient for delete
    RandomDataProvider.GetRandomPatient(Guid.Parse("cb0395bd-2abd-4454-bc18-132394713883")),
    // patient for getById
    RandomDataProvider.GetRandomPatient(Guid.Parse("0d293ae8-8a93-40ad-b7e3-4998f1926962"))
} };

for (var i = request.Items.Count; i < patientsCount; i++)
    request.Items.Add(RandomDataProvider.GetRandomPatient());

try
{
    var response = await httpClient.PostAsJsonAsync("patients/create-many", request);

    if (response.IsSuccessStatusCode)
        Console.WriteLine("PATIENTS CREATED SUCCESSFULLY");
    else
        Console.WriteLine("SOMETHING WENT WRONG DURING CREATING PATIENTS");
}
catch (Exception e)
{
    Console.WriteLine("SOMETHING WENT WRONG DURING CREATING PATIENTS");
}




