using System;

namespace Environment.LaboratoryFirstLevel
{
    public static class LaboratoryWorkerCryStaticEvent
    {
        public static event EventHandler<LaboratoryWorkerCryEventArgs> OnAnyLaboratoryWorkerCry;

        public static void Call(object sender, LaboratoryWorkerCryEventArgs laboratoryWorkerCryEventArgs)
        {
            OnAnyLaboratoryWorkerCry?.Invoke(sender, laboratoryWorkerCryEventArgs);
        }
    }
    
    public class LaboratoryWorkerCryEventArgs : EventArgs
    {
        public readonly bool IsCrying;

        public LaboratoryWorkerCryEventArgs(bool isCrying)
        {
            IsCrying = isCrying;
        }
    }
}