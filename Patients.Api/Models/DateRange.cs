using System.Text.RegularExpressions;

namespace Patients.Api.Helpers;

public class DateRange
{
    private int _year;
    private int? _month;
    private int? _day;
    private int? _hours;
    private int? _minutes;

    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    public DateRange(string date)
    {
        ParseDateParts(date);
        SetDateRange();
    }

    private void ParseDateParts(string date)
    {
        var numbers = Regex.Matches(date, "\\d+")
                .Select(x => int.Parse(x.ValueSpan)).ToList();

        _year = numbers[0];
        _month = numbers.Count > 1 ? _month = numbers[1] : null;
        _day = numbers.Count > 2 ? _day = numbers[2] : null;
        _hours = numbers.Count > 3 ? _hours = numbers[3] : null;
        _minutes = numbers.Count > 4 ? _minutes = numbers[4] : null;
    }

    private void SetDateRange()
    {
        if (_month == null)
        {
            Start = new DateTime(_year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            End = new DateTime(_year, 12, 31, 23, 59, 59, DateTimeKind.Utc);
            return;
        }

        if (_day == null)
        {
            var daysInMonth = DateTime.DaysInMonth(_year, _month.Value);

            Start = new DateTime(_year, _month.Value, 1, 0, 0, 0, DateTimeKind.Utc);
            End = new DateTime(_year, _month.Value, daysInMonth, 23, 59, 59, DateTimeKind.Utc);
            return;
        }

        if (_hours == null)
        {
            Start = new DateTime(_year, _month.Value, _day.Value, 0, 0, 0, DateTimeKind.Utc);
            End = new DateTime(_year, _month.Value, _day.Value, 23, 59, 59, DateTimeKind.Utc);
            return;
        }

        if (_minutes == null)
        {
            Start = new DateTime(_year, _month.Value, _day.Value, _hours.Value, 0, 0, DateTimeKind.Utc);
            End = new DateTime(_year, _month.Value, _day.Value, _hours.Value, 59, 59, DateTimeKind.Utc);
            return;
        }

        Start = new DateTime(_year, _month.Value, _day.Value, _hours.Value, _minutes.Value, 0, DateTimeKind.Utc);
        End = new DateTime(_year, _month.Value, _day.Value, _hours.Value, _minutes.Value, 59, DateTimeKind.Utc);
    }
}