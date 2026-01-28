using System;
using UnityEngine;

// TODO: Remove the allocs here, use a static memory pool?
public static class Awaiters
{
#pragma warning disable CS0436 // Le type est en conflit avec le type importé
    readonly static WaitForUpdate _waitForUpdate = new WaitForUpdate();
#pragma warning restore CS0436 // Le type est en conflit avec le type importé
    readonly static WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    readonly static WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

#pragma warning disable CS0436 // Le type est en conflit avec le type importé
    public static WaitForUpdate NextFrame
#pragma warning restore CS0436 // Le type est en conflit avec le type importé
    {
        get { return _waitForUpdate; }
    }

    public static WaitForFixedUpdate FixedUpdate
    {
        get { return _waitForFixedUpdate; }
    }

    public static WaitForEndOfFrame EndOfFrame
    {
        get { return _waitForEndOfFrame; }
    }

    public static WaitForSeconds Seconds(float seconds)
    {
        return new WaitForSeconds(seconds);
    }

    public static WaitForSecondsRealtime SecondsRealtime(float seconds)
    {
        return new WaitForSecondsRealtime(seconds);
    }

    public static WaitUntil Until(Func<bool> predicate)
    {
        return new WaitUntil(predicate);
    }

    public static WaitWhile While(Func<bool> predicate)
    {
        return new WaitWhile(predicate);
    }
}
