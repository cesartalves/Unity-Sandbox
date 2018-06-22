using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InjectedClick : MonoBehaviour {

    [Inject]
    protected Greeter greeter;

	public void Click()
    {
        greeter.Greet();
    }
}
