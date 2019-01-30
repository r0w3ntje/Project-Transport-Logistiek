namespace TransportLogistiek
{
    public static class MachineStateChangeEvent
    {
        public delegate void MachineStateEvent();
        public static event MachineStateEvent OnMachineStateChange = delegate { };

        public static void CallEvent()
        {
            OnMachineStateChange();
        }
    }
}