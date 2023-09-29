namespace CCM.Projects.SisGeapeWeb2.Business.InfraValidation.Interface
{
    public interface IValidationDictionary
    {
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
    }
}
