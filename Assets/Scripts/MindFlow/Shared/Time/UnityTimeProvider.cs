namespace MindFlow.Shared.Time
{
    /// <summary>
    /// Unity-backed time provider for runtime systems.
    /// </summary>
    public sealed class UnityTimeProvider : ITimeProvider
    {
        public float RealtimeSinceStartup => UnityEngine.Time.realtimeSinceStartup;
    }
}
