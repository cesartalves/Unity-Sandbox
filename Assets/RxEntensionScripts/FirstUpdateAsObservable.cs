using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;

public class FirstUpdateAsObservable : MonoBehaviour {

	void Start () => 
        gameObject.EveryFirstUpdateAsObservable()
            .Subscribe(
                ç => Debug.Log("This is like a 'MultipleStart' that Unity is missing.")
             );

    public void ToggleActive() =>    
        gameObject.SetActive(!gameObject.activeSelf);
    
}
