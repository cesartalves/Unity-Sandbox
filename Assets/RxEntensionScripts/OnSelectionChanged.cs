using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UniRx;

public class OnSelectionChanged : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<DropDownList>().OnSelectionChanged.AsObservable().Subscribe(ç => Debug.Log(ç));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
