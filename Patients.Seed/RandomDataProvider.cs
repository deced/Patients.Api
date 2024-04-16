namespace Patients.Seed;

public static class RandomDataProvider
{
    private static Random _random = new Random();

    private static Dictionary<Gender, string[]> _firstNames = new()
    {
        {
            Gender.Male, new[]
            {
                "Aleksandr", "Andrey", "Mikhail", "Ivan", "Dmitriy",
                "Sergey", "Maksim", "Aleksey", "Artyom", "Nikolay",
                "Egor", "Ilya", "Kirill", "Vladimir", "Timofey",
                "Pavel", "Daniil", "Grigoriy", "Anton", "Stepan"
            }
        },
        {
            Gender.Female, new[]
            {
                "Anna", "Maria", "Elena", "Natalia", "Svetlana",
                "Olga", "Tatiana", "Yulia", "Irina", "Ekaterina",
                "Marina", "Anastasia", "Daria", "Victoria", "Valentina",
                "Yelena", "Galina", "Larisa", "Tamara", "Lyudmila"
            }
        }
    };

    private static Dictionary<Gender, string[]> _lastNames =new ()
    {
        {
            Gender.Male, new[]
            {
                "Ivanov", "Smirnov", "Kuznetsov", "Popov", "Sokolov",
                "Lebedev", "Kozlov", "Novikov", "Morozov", "Petrov",
                "Volkov", "Solovyov", "Vasilyev", "Zaitsev", "Pavlov",
                "Semyonov", "Golubev", "Vinogradov", "Bogdanov", "Vorobyov"
            }
        },
        {
            Gender.Female, new[]
            {
                "Ivanova", "Smirnova", "Kuznetsova", "Popova", "Sokolova",
                "Lebedeva", "Kozlova", "Novikova", "Morozova", "Petrova",
                "Volkova", "Solovyova", "Vasilyeva", "Zaitseva", "Pavlova",
                "Semyonova", "Golubeva", "Vinogradova", "Bogdanova", "Vorobyova"
            }
        }
    };

    private static Dictionary<Gender, string[]> _middleNames = new()
    {
        {
            Gender.Male, new[]
            {
                "Andreevich", "Mikhailovich", "Ivanovich", "Dmitrievich",
                "Sergeevich", "Maksimovich", "Alekseevich", "Artyomovich", "Nikolaevich",
                "Yegorovich", "Ilyich", "Kirillovich", "Vladimirovich", "Timofeevich",
                "Pavlovich", "Daniilovich", "Grigorievich", "Antonovich", "Stepanovich"
            }
        },
        {
            Gender.Female, new[]
            {
                "Aleksandrovna", "Andreevna", "Mikhailovna", "Ivanovna", "Dmitrievna", 
                "Sergeevna", "Maksimovna", "Alekseevna", "Artyomovna", "Nikolaevna", 
                "Yegorovna", "Ilyinichna", "Kirillovna", "Vladimirovna", "Timofeevna",
                "Pavlovna", "Daniilovna", "Grigorievna", "Antonovna", "Stepanovna"
            }
        }
    };

    private static string[] _useStatuses = { "official", "unofficial", "something" };
    private static bool[] _activeStatuses = { true, false };
    private static Gender[] _genders = Enum.GetValues<Gender>();

    private static T GetRandomValueFromArray<T>(T[] array)
    {
        return array[_random.Next(array.Length)];
    }
    
    private static T GetRandomValueFromDictionary<T>(Dictionary<Gender, T[]> dictionary, Gender gender)
    {
        dictionary.TryGetValue(gender, out var array);
        array ??= dictionary[Gender.Male];
        
        return array[_random.Next(array.Length)];
    }

    private static string GetRandomFirstName(Gender gender)
    {
        return GetRandomValueFromDictionary(_firstNames, gender);
    }
    
    private static string GetRandomLastName(Gender gender)
    {
        return GetRandomValueFromDictionary(_lastNames, gender);
    }
    
    private static string GetRandomMiddleName(Gender gender)
    {
        return GetRandomValueFromDictionary(_middleNames, gender);
    }

    private static Gender GetRandomGender()
    {
        return GetRandomValueFromArray(_genders);
    }

    private static DateTime GetRandomDateTime()
    {
        var now = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
        return DateTime.UnixEpoch.AddSeconds(_random.NextInt64(now));
    }

    public static PatientModel GetRandomPatient(Guid guid = default)
    {
        var gender = GetRandomGender();

        var patient = new PatientModel()
        {
            // it is not really necessary, but it will be used for
            // update, delete, getById demonstration in postman
            Guid = guid,
            Active = GetRandomValueFromArray(_activeStatuses),
            Gender = gender.ToString().ToLower(),
            Family = GetRandomLastName(gender),
            Given = new List<string>()
            {
                GetRandomFirstName(gender),
                GetRandomMiddleName(gender)
            },
            Use = GetRandomValueFromArray(_useStatuses),
            BirthDate = GetRandomDateTime()
        };

        return patient;
    }
    
}

public enum Gender
{
    Male,
    Female,
    Other,
    Unknown
}