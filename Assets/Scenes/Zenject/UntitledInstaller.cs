using Zenject;
using UnityEngine;
using System.Collections;

public class UntitledInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.Bind<string>().FromFactory<Logger>();
        Container.Bind<Greeter>().AsSingle().NonLazy();
        
  
    }
}

public class Logger : IFactory<string>, IFactory
{
    public string Create()
    {
        return "Hello World!";
    }
}

public class Greeter { 
    string message;

    public Greeter(string message)
    {
        this.message = message;
    }

    public void Greet()
    {
        Debug.Log(message);
    }

   
   
}