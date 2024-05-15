using System;

public abstract class Event{}
public class EventBus<T> where T : Event
{
    public static event Action<T> OnEvent;
    public static void Invoke(T pEvent){
        OnEvent?.Invoke(pEvent);
    }
}