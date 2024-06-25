using System;

public abstract class Event{}
public class EventBus<T> where T : Event
{
    public static event Action<T> OnEvent;
    public static void Invoke(T pEvent){
        OnEvent?.Invoke(pEvent);
    }
}

public class OnObjectiveComplete : Event{
    public ObjectiveItem item{get; private set;}
    public OnObjectiveComplete(ObjectiveItem pItem){
        item = pItem;
    }
}

public class OnObjectiveActivated : Event{
    public ObjectiveType type{get; private set;}
    public OnObjectiveActivated(ObjectiveType pType){
        type = pType;
    }
}

public class OnTeleporterEntered : Event
{
    public OnTeleporterEntered()
    {

    }
}

public class OnAnimationComplete : Event
{
    public OnAnimationComplete()
    {

    }
}

public class OnPlayerThrow : Event
{
    public OnPlayerThrow()
    {

    }
}