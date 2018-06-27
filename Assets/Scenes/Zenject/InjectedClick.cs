using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InjectedClick : MonoBehaviour {

    [InjectAttribute]
    protected Greeter greeter;

	public void Click()
    {
        greeter.Greet();
    }
}
