namespace PixelHotel.Core.Domain.Validations;

public static class ValidatorMessages
{
    public static string NotInformed(string field)
        => $"{field} is not informed";

    public static string IsInvalid(string field)
        => $"{field} is invalid";

    public static string GreaterThan(string field, int number = 0)
        => $"{field} should be greater than {number}";

    public static string NotFound(string field)
        => $"{field} was not found";

    public static string FieldCannotBeEmpty(string field)
        => $"{field} cannot be empty";

    public static string LessThan(string field, int number = 0)
        => $"{field} should be greater than {number}";

    public static string LessThanString(string field)
        => $"{field} should be greater than 255";
}
