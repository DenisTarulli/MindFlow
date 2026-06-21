using UnityEngine;

namespace Mindlow.Puzzles
{
    /// <summary>
    /// Base MonoBehaviour for concrete puzzle views/controllers.
    /// </summary>
    public abstract class PuzzleBehaviour : MonoBehaviour, IPuzzle
    {
        private PuzzleSessionService sessionService;

        protected PuzzleSession CurrentSession { get; private set; }

        /// <summary>
        /// Injects the active session and service used to report puzzle progress.
        /// </summary>
        public void Initialize(PuzzleSession session, PuzzleSessionService service)
        {
            CurrentSession = session;
            sessionService = service;
            OnInitialized(session);
        }

        protected virtual void OnInitialized(PuzzleSession session)
        {
        }

        protected void ReportAttempt()
        {
            sessionService.RecordAttempt(CurrentSession.sessionId);
        }

        protected void ReportError()
        {
            sessionService.RecordError(CurrentSession.sessionId);
        }

        protected void ReportRestart()
        {
            sessionService.Restart(CurrentSession.sessionId);
        }

        protected void ReportCompleted()
        {
            sessionService.Complete(CurrentSession.sessionId);
        }

        protected void ReportAbandoned()
        {
            sessionService.Abandon(CurrentSession.sessionId);
        }
    }
}
