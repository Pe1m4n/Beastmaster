using System;
using UniRx;
using UnityEngine;

namespace Common.UniRxExtensions
{
    public static class UniRxExtensions
    {
        public static IObservable<float> TimerTween(float timeSeconds, bool unscaled = false)
        {
            return Observable.Create<float>(o =>
            {
                var currentTime = 0f;

                return Observable.EveryUpdate().Subscribe(_ =>
                {
                    var deltaTime = unscaled ? Time.unscaledDeltaTime : Time.deltaTime;
                    currentTime += deltaTime;
                    o.OnNext(Mathf.Clamp01(currentTime / timeSeconds));

                    if (currentTime >= timeSeconds)
                        o.OnCompleted();
                });
            });
        }
    }
}