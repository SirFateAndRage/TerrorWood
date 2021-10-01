using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable<T>
{
    void Subscribe(IObserver<T> obs);
    void Unsubscribe(IObserver<T> obs);

    void NotifyToObservers(T action);
}
