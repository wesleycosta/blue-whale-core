namespace PixelHotel.Core.Domain.Validations;

public static class ValidatorMessages
{
    public static string NotInformed(string field)
        => $"Field {field} is not informed";

    public static string IsInvalid(string field)
        => $"Field {field} is invalid";

    public static string GreaterThan(string field, int number = 0)
        => $"Field {field} should be greater than {number}";

    public static string NotFoundInDatabase(string field)
        => $"Field {field} was not found in the database";

    public static string FieldCannotBeEmpty(string field)
        => $"The field {field} cannot be empty";

    public static string LessThan(string field, int number = 0)
        => $"Field {field} should be greater than {number}";

    public static string LessThanString(string field)
        => $"Field {field} should be greater than 255";
}

