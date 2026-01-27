using System;
using System.Collections.Generic;

public static class EventBus<T>
{
    private static readonly List<Action<T>> Listeners = new();

    public static void Subscribe(Action<T> listener) => Listeners.Add(listener);
    public static void Unsubscribe(Action<T> listener) => Listeners.Remove(listener);

    public static void Publish(T eventData)
    {
        for (var i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners[i]?.Invoke(eventData);
        }
    }
}