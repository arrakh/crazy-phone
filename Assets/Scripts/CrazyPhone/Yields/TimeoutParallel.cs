using UnityEngine;

namespace CrazyPhone.Yields
{
    public static class TimeoutParallelExtension
    {
        public static TimeoutParallel TimeoutParallel(this CustomYieldInstruction wrappedInstruction, float timeout) =>
            new (wrappedInstruction, timeout);
    }
    
    public class TimeoutParallel : CustomYieldInstruction
    {
        private CustomYieldInstruction wrappedInstruction;
        private float timeout;
        private float currentTimer;
        private bool hasTimedOut = false;
        public bool HasTimedOut => hasTimedOut;

        public TimeoutParallel(CustomYieldInstruction wrappedInstruction, float timeout)
        {
            this.wrappedInstruction = wrappedInstruction;
            this.timeout = timeout;
        }

        public override bool keepWaiting => CheckShouldKeepWaiting();


        private bool CheckShouldKeepWaiting()
        {
            if (!wrappedInstruction.keepWaiting)
            {
                hasTimedOut = false;
                return false;
            }
            
            currentTimer += Time.deltaTime;
            hasTimedOut = currentTimer >= timeout;
            return !HasTimedOut;
        }
    }
}