using System.Collections;
using System.Collections.Generic;
using System;

public class Observer<T> : IObserver<T>
{
    public Action<T> onNext;
    public Action<Exception> onError;
    public Action onComplete;
    public void OnCompleted()
    {
        if (this.onComplete != null)
        {
            onComplete();
        }
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(T value)
    {
        if (this.onNext != null)
        {
            onNext(value);
        }
    }
    public Observer(Action<T> onNext)
    {
        this.onNext = onNext;
    }
    public Observer(Action<T> onNext, Action onComplete)
    {
        this.onNext = onNext;
        this.onComplete = onComplete;
    }
}
