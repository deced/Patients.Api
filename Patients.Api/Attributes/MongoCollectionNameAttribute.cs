namespace Patients.Api.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class MongoCollectionNameAttribute : Attribute
{
    public MongoCollectionNameAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }

    public string CollectionName { get; }
}