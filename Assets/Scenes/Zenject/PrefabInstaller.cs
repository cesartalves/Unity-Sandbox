using UnityEngine;
using Zenject;

public class PrefabInstaller : MonoInstaller
{
    public override void InstallBindings(){

        Container.Bind<AScriptToBeBound>().FromNewComponentOnNewGameObject().AsSingle();
    }
}