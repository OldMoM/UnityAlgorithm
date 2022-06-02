using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class FastSubject<T>:IObservable<T>,IObserver<T>
{
    private List<IObserver<T>> observers = new List<IObserver<T>>();
    public void OnCompleted()
    {
     
        throw new NotImplementedException();
    }
    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(T value)
    {
        if (observers != null)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(value);
            }
        }
    }
    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        return new Unsubscriber<T>(observers, observer);
    }
    public IDisposable Subscribe(Action<T> onNext)
    {
        return Subscribe(new Observer<T>(onNext));
    }
}
