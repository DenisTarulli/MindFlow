namespace Mindlow.Shared.Time
{
    /// <summary>
    /// Provides time values to keep metrics deterministic in tests.
    /// </summary>
    public interface ITimeProvider
    {
        float RealtimeSinceStartup { get; }
    }
}
