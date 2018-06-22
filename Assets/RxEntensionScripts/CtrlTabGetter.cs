using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using System;

public class CtrlTabGetter : MonoBehaviour {


    void Start(){

        var shortcut = new KeyCode[] { KeyCode.LeftControl, KeyCode.A };

        var tabPressed = gameObject.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.A))
            .Select(_ => KeyCode.A);

        var ctrlPressed = gameObject.UpdateAsObservable()
            .Where(_ => Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            .Select(_ => KeyCode.LeftControl);

        IObservable<KeyCode> observableKeyPresses =
            shortcut.ToObservable()
                .SelectMany(
                    keycode => gameObject.UpdateAsObservable()
                                .Where(_ => Input.GetKey(keycode) || Input.GetKeyDown(keycode))
                                .Select(_ => keycode)
                           );

        observableKeyPresses
            .Scan(new List<Timestamped<KeyCode>>() { }, (x, y) => { x.Add(Timestamped.Create(y, DateTimeOffset.Now)); return x; })
            .Where(_ => _.Count >= shortcut.Length && _.Select(ç => ç.Value).ToList().GetRange(_.Count - shortcut.Length, shortcut.Length).SequenceEqual(shortcut))
            .Where(_ => _[_.Count - shortcut.Length].Timestamp.AddMilliseconds(100) >= DateTimeOffset.Now)
            .Subscribe(_ => Debug.Log("Shortcut pressed"));


        //ctrlPressed
        //  .Merge(tabPressed)
        //  .Scan(new List<KeyCode>() { }, (x, y) => { x.Add(y); return x; })
        //  .Where(_ => _.Count >= shortcut.Length && _.GetRange(_.Count - shortcut.Length, shortcut.Length).SequenceEqual(shortcut))
        //  .Subscribe(_ => Debug.Log("Ctrl a pressed"));

        //ctrlPressed
        //    .Merge(tabPressed)
        //    .Pairwise()
        //    .Where(_ => _.Previous == KeyCode.LeftControl && _.Current == KeyCode.A)
        //    .TimeInterval()
        //    .Subscribe(_ => Debug.Log("Ctrl a pressed"));
    }  

}
