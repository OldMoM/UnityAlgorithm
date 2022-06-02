using System.Collections;
using System.Collections.Generic;
using System;

public class Unsubscriber<T>:IDisposable
{
    private List<IObserver<T>> observers;
    private IObserver<T> observer;

    public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
    {
        this.observers = observers;
        this.observer = observer;
    }

    public void Dispose()
    {
        if (observers != null && observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }
}
