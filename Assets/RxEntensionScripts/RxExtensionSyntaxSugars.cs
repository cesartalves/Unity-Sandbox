using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public static class RxExtensionSyntaxSugars
{
    public static IObservable<Unit> EveryFirstUpdateAsObservable(this GameObject gameObject)
    {
        var firstUpdate = FirstUpdateAsObservable(gameObject);

        return firstUpdate.Merge(
                    firstUpdate
                    .RepeatSafe()
                    .SkipUntil(gameObject.OnDisableAsObservable())
                    .First()
                    .RepeatUntilDestroy(gameObject)
                    .CatchIgnore()
                 );
    }

    public static IObservable<Unit> FirstUpdateAsObservable(this GameObject gameObject)
    {
        return gameObject.UpdateAsObservable().Take(1);
    }

    
}

