namespace Gym.Interfaces
{
    public interface IParameterService
    {
        Task<Dictionary<string, string>> GetAllActiveParametersAsync();
        Task<string> GetParameterValueAsync(string name);
    }
}
