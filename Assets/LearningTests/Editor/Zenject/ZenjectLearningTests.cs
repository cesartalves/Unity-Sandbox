using Zenject;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

[TestFixture]
public class ZenjectLearningTests : ZenjectUnitTestFixture
{
    public class SignalTest : Signal<SignalTest, string> { }


    public class Greeter
    {
        public void SayHello(string userName) => Debug.Log("Hello " + userName + "!");
        
    }

    public class GreeterFactory
    {
        public Greeter Get() =>  new Greeter();
        
    }

    public class TriggersSignal
    {
        public TriggersSignal(SignalTest signal)
        {
            signal.Fire("@cesartalves");
        }
    }

    public class ListensToSignal
    {
        [Inject]
        public ListensToSignal(SignalTest signal, Greeter greeter)
        {
            signal.Listen(greeter.SayHello);
        }
    }

    public class InjectedOnMethod 
    {
        private Greeter greeter;

        [Inject]
        public void Initialize(Greeter greeter)
        {
            this.greeter = greeter;
        }

        
        public void Print() => greeter.SayHello("@cesartalves");
        
    }

    [Test]
    public void BindingWithId()
    {
        Container.Bind<int>().WithId("5").FromInstance(5);     

        GenericComposite<int> composite = new GenericComposite<int>(Container.ResolveId<int>("5"));

        composite.AddChild(Container.ResolveId<int>("5"));

        foreach (var element in composite) Debug.Log(element.element);

        LogAssert.Expect(LogType.Log, "5");
        LogAssert.Expect(LogType.Log, "5");

    }

    [Test]
    public void Signal()
    {
        Container.Bind<SignalManager>().AsSingle();
        Container.DeclareSignal<SignalTest>().RequireHandler();

        Container.Bind<GreeterFactory>().AsSingle();
        Container.Bind<Greeter>().FromMethod(x => x.Container.Resolve<GreeterFactory>().Get());

        Container.Bind<ListensToSignal>().AsSingle().NonLazy();
        //Non Lazy doesn't work on EditorTests, apparently. Otherwise, this'd log twice?

        var listener = Container.Resolve<ListensToSignal>();
        new TriggersSignal(Container.Resolve<SignalTest>());

        LogAssert.Expect(LogType.Log, "Hello @cesartalves!");
        
    }


    [Test]
    public void FromMethod()
    {
        Container.Bind<GreeterFactory>().AsSingle();
        Container.Bind<Greeter>().FromMethod(x => x.Container.Resolve<GreeterFactory>().Get());
    }

    [Test]
    public void FieldInjection()
    {
        Container.Bind<Greeter>().FromInstance(new Greeter()).AsSingle();
        Container.Bind<InjectingInField>().AsTransient();

        Container.Resolve<InjectingInField>().Greet();

        LogAssert.Expect(LogType.Log, "Hello @cesartalves!");
    }

    [Test]
    public void WithoutAnything()
    {
        Container.Bind(typeof(InjectingInField));
        Container.Resolve<InjectingInField>().Greet();

        LogAssert.Expect(LogType.Log, "Hello @cesartalves!");
    }


    public class InjectingInField
    {
        [Inject]
        protected Greeter greeter;
        
        public void Greet()
        {
            greeter.SayHello("@cesartalves");
        }
    }
    
}