public static class CollectEvent
{
    public delegate void Collect();
    public static event Collect OnCollect;

    public static void CallEvent()
    {
        if (OnCollect != null)
            OnCollect();
    }
}