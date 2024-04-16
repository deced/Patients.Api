using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Patients.Api.Models.FilterPatients;

public class FilterPatientsRequest : IValidatableObject
{
    public string[]? DateFilters { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DateFilters == null)
            return Enumerable.Empty<ValidationResult>();
        
        var availableFilters = new[] { "eq", "ne", "lt", "gt", "ge", "le", "sa", "eb", "ap" };
        var results = new List<ValidationResult>();

        for (int i = 0; i < DateFilters.Length; i++)
        {
            var dateItem = DateFilters[i];

            if (string.IsNullOrEmpty(dateItem))
            {
                results.Add(new("Filter cant be empty", GetParamName(i)));
                continue;
            }
            
            var filter = dateItem[..2];
            if (!availableFilters.Contains(filter))
            {
                results.Add(new($"Filter {filter} is unsupported", GetParamName(i)));
                continue;
            }

            var dateString = dateItem[2..];
            if (!Regex.IsMatch(dateString, @"^\d{4}(-\d{1,2}(-\d{1,2}(T\d{1,2}(:\d{1,2})?)?)?)?$"))
            {
                results.Add(new($"{dateString} is invalid date", GetParamName(i)));
                continue;
            }

            try
            {
                var dateRange = new DateRange(dateString);
            }
            catch (Exception e)
            {
                results.Add(new($"{dateString} is invalid date", GetParamName(i)));
            }
        }

        return results;

        string[] GetParamName(int index) => new[] { $"{nameof(DateFilters)}[{index}]" };
    }
}
