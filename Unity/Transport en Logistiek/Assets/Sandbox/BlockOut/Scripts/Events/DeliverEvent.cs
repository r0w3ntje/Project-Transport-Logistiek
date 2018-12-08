public static class DeliverEvent
{
    public delegate void Deliver();
    public static event Deliver OnDeliver;

    public static void CallEvent()
    {
        if (OnDeliver != null)
            OnDeliver();
    }
}