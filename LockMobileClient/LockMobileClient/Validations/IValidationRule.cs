namespace LockMobileClient.Validations
{
    public interface IValidationRule<T>
    {
        bool Validate(T value);
    }
}
