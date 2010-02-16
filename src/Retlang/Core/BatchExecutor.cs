using System;

namespace Retlang.Core
{
    /// <summary>
    /// Default Action executor.
    /// </summary>
    public class BatchExecutor : IBatchExecutor
    {
        private bool _running = true;

        /// <summary>
        /// <see cref="IBatchExecutor.ExecuteAll(Action[])"/>
        /// </summary>
        public void ExecuteAll(Action[] toExecute)
        {
            foreach (var action in toExecute)
            {
                if (_running)
                {
                    action();
                }
            }
        }
        
        /// <summary>
        /// When disabled, actions will be ignored by executor. The executor is typically disabled at shutdown
        /// to prevent any pending actions from being executed. 
        /// </summary>
        public bool IsEnabled
        {
            get { return _running; }
            set { _running = value; }
        }
    }
}