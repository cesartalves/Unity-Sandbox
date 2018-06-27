using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System.Linq;
using System;
using UnityEngine.UI;

public class RxExtensionLearningTests
{

    [TestCase(new int[] { 1, 2, 3 })]
    public void Scan(int[] array)
    {

        array.ToObservable().Scan((x, y) => (x + y)).Subscribe(_ => Debug.Log(_)); // 6
        LogAssert.Expect(LogType.Log, "1");
        LogAssert.Expect(LogType.Log, "3");
        LogAssert.Expect(LogType.Log, "6");
    }

    [TestCase(1, 2, 3, 4)]
    public void SelectManyAKAFlatMap(params int[] array)
    {
        array.ToObservable().SelectMany(x => new int[] { x, x + 5 }).Subscribe(_ => Debug.Log(_));

        LogAssert.Expect(LogType.Log, "1");
        LogAssert.Expect(LogType.Log, "6");
        LogAssert.Expect(LogType.Log, "2");
        LogAssert.Expect(LogType.Log, "7");
        LogAssert.Expect(LogType.Log, "3");
        LogAssert.Expect(LogType.Log, "8");
        LogAssert.Expect(LogType.Log, "4");
        LogAssert.Expect(LogType.Log, "9");
    }

    [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 5, 6, 7 })]
    public void Merge(int[] array1, int[] array2)
    {
        array1.ToObservable().Merge(array2.ToObservable()).Subscribe(_ => Debug.Log(_)); // 1 2 3 4 5 6 7

        LogAssert.Expect(LogType.Log, "1");
        LogAssert.Expect(LogType.Log, "2");
        LogAssert.Expect(LogType.Log, "3");
        LogAssert.Expect(LogType.Log, "4");
        LogAssert.Expect(LogType.Log, "5");
        LogAssert.Expect(LogType.Log, "6");
        LogAssert.Expect(LogType.Log, "7");
    }

    [TestCase(1, 2, 3, 4)]
    public void PairWise(params int[] args)
    {
        args.ToObservable().Pairwise().Subscribe(_ => Debug.Log(_.Current));
        LogAssert.Expect(LogType.Log, "2");
        LogAssert.Expect(LogType.Log, "3");
        LogAssert.Expect(LogType.Log, "4");
    }

    [TestCase(100, 200, 300, 400)]
    public void AggregateAKAReduce(params int[] args)
    {
        args.ToObservable().Aggregate((x, y) => x + y).Subscribe(x => Debug.Log(x));
        LogAssert.Expect(LogType.Log, "1000");
    }

    [Test]
    public void CombineLatest()
    {
        new int[] { 1, 2 }.ToObservable().CombineLatest(new int[] { 1, 4 }.ToObservable(), (x, y) => x + y).Subscribe(_ => Debug.Log(_));
        LogAssert.Expect(LogType.Log, "2");
        LogAssert.Expect(LogType.Log, "3");
        LogAssert.Expect(LogType.Log, "6");
    }

    [Test]
    public void SelectAKAMap()
    {
        new int[] { 1, 2 }.ToObservable().Select(_ => _ * 5).Subscribe(_ => Debug.Log(_));
        LogAssert.Expect(LogType.Log, "5");
        LogAssert.Expect(LogType.Log, "10");

    }

    [Test]
    public void SelectAKAMapWithIndex()
    {
        new int[] { 1, 2 }.
            ToObservable().Select((value, index) => String.Format("{0}:{1}", index, value)).Subscribe(_ => Debug.Log(_));
        LogAssert.Expect(LogType.Log, "0:1");
        LogAssert.Expect(LogType.Log, "1:2");

    }

    [TestCase(1, 2, 3, 4, 5, ExpectedResult = 5)]
    [TestCase(5, ExpectedResult = 5)]
    public int Wait(params int[] args) => args.ToObservable().Wait();


    [Test]
    public void Sample()
    {
        new int[] { 1, 2 }.ToObservable().Sample(new int[] { 1 }.ToObservable()).Subscribe(_ => Debug.Log(_));
        LogAssert.Expect(LogType.Log, "1");
    }

    [Test]
    public void Concat()
    {
        new int[] { 1, 2, 3 }.ToObservable().Concat(new int[] { 1, 2, 3 }.ToObservable()).Subscribe(_ => Debug.Log(_));
    }

    [TestCase(1, 2, 3)]
    public void Zip_2_2(params int[] args)
    {
        args.
            ToObservable()
            .ZipLatest(
                new int[] { 2, 2 }
                    .ToObservable()

                ,
                (x, y) => x + y).Subscribe(value => Debug.Log(value));

        LogAssert.Expect(LogType.Log, "3");
        LogAssert.Expect(LogType.Log, "4");
    }

    [Test]
    public void Timestamp()
    {
        new int[] { 1, 2, 3, 4 }.ToObservable().Timestamp().Subscribe(_ => Debug.Log(_.Timestamp));
    }

    [Test]
    public void WithLatestFrom()
    {
        new int[] { 1, 2, 3, 4 }.ToObservable().
            WithLatestFrom(new string[] { "a", "b" }.ToObservable(), (inT, sTring) => inT.ToString() + sTring).Subscribe(_ => Debug.Log(_));

        LogAssert.Expect(LogType.Log, "2a");
        LogAssert.Expect(LogType.Log, "3b");
        LogAssert.Expect(LogType.Log, "4b");
    }

    [UnityTest]
    public IEnumerator BatchFrame1()
    {
        Subject<string> subject = new Subject<string>();

        var observable = subject.AsObservable();

        observable.
            BatchFrame(2, FrameCountType.Update)
            .Subscribe(_ => Debug.Log(_.Aggregate((latest, next) => latest + next)));

        //1234
        //567

        yield return null;

        subject.OnNext("1");

        yield return null;

        subject.OnNext("2");

        yield return null;

        subject.OnNext("3");

        yield return null;

        subject.OnNext("4");

        yield return null;

        subject.OnNext("5");

        yield return null;

        subject.OnNext("6");

        yield return null;

        subject.OnNext("7");
    }

    [UnityTest]
    public IEnumerator BatchFrame2()
    {

        Subject<string> subject = new Subject<string>();

        var observable = subject.AsObservable();

        observable.
            BatchFrame()
            .Subscribe(_ => Debug.Log(_.Aggregate((latest, next) => latest + next)));

        //12
        //3
        //4
        //5
        //6
        //7

        yield return null;

        subject.OnNext("1");

        yield return null;

        subject.OnNext("2");

        yield return null;

        subject.OnNext("3");

        yield return null;

        subject.OnNext("4");

        yield return null;

        subject.OnNext("5");

        yield return null;

        subject.OnNext("6");

        yield return null;

        subject.OnNext("7");

    }

    [UnityTest]
    public IEnumerator Buffer()
    {
        Subject<string> subject = new Subject<string>();

        var observable = subject.AsObservable();

        observable.Buffer(3, 2).Subscribe(_ => Debug.Log(_.Aggregate((one, two) => one + two)));

        //123
        //345
        //567

        yield return null;

        subject.OnNext("1");

        yield return null;

        subject.OnNext("2");

        yield return null;

        subject.OnNext("3");

        yield return null;

        subject.OnNext("4");

        yield return null;

        subject.OnNext("5");

        yield return null;

        subject.OnNext("6");

        yield return null;

        subject.OnNext("7");
    }

    [UnityTest]
    public IEnumerator Throttle()
    {
        Subject<string> subject = new Subject<string>();

        var observable = subject.AsObservable().DelayFrame(1);

        observable.ThrottleFrame(2, FrameCountType.FixedUpdate).Subscribe(_ => Debug.Log(_));
        //1, then 7

        subject.OnNext("1");

        yield return null; yield return null;
        yield return null; yield return null;

        subject.OnNext("2");

        yield return null;

        subject.OnNext("7");

    }

    [UnityTest]
    public IEnumerator Amb()
    {
        Subject<string> subject = new Subject<string>();
        Subject<string> subject2 = new Subject<string>();

        var observable = subject.AsObservable();

        observable.Amb(subject2.AsObservable()).Subscribe(_ => Debug.Log(_));
        //3, then 4, then 5

        subject2.OnNext("3");
        subject.OnNext("1");
        subject.OnNext("2");
        subject2.OnNext("4");
        subject.OnNext("7");
        subject2.OnNext("5");

        yield return null;

    }

    public class PenisAdder : IObserver<string, string>
    {
        public string OnCompleted()
        {
            throw new NotImplementedException();
        }

        public string OnError(Exception exception)
        {
            throw new NotImplementedException();
        }

        public string OnNext(string value)
        {
            return value + " with a penis";
        }
    }
 

    [Test]
    public void MaterializeAKAVisitor()
    {
        Subject<string> subject = new Subject<string>();

        IObserver<string, string> observer = new PenisAdder();

        var observable = subject.AsObservable();

        observable.Materialize().Subscribe(_ => {

            _.Accept<string>((str => str), ex => "", () => "");
            Debug.Log(_.Accept(observer)); });

        subject.OnNext("1");
        subject.OnNext("2");
        subject.OnNext("3");

        //Guess what will be printed.

    }

    [Test]
    public void Publish()
    {
        Subject<string> subject = new Subject<string>();
        var observable = subject.AsObservable();

        observable.Publish().Connect();

        subject.OnNext("1");
        subject.OnNext("2");
        subject.OnNext("3");
    }

    [UnityTest]
    public IEnumerator FrameInverval()
    {

        Subject<string> subject = new Subject<string>();
        var observable = subject.AsObservable();

        observable.FrameInterval().Subscribe(_ => Debug.Log(_.Interval));
        //0,0
        yield return null;
        yield return null;
        yield return null;

        subject.OnNext("1");

        yield return null;
        yield return null;

        subject.OnNext("1");


    }

    [UnityTest]
    public IEnumerator Timeout()
    {

        Subject<string> subject = new Subject<string>();
        var observable = subject.AsObservable();

        observable.TimeoutFrame(2).Catch<string, TimeoutException>(exception => Observable.Start(() => "2")).Subscribe(_ => Debug.Log(_));

        subject.OnNext("1");

        yield return null;
        yield return null;
        yield return null;
        yield return null;

        subject.OnNext("1");

    }

    [Test]
    public void WhereToSkip()
    {
        Subject<string> subject = new Subject<string>();
        var observable = subject.AsObservable();

        var print1skip1 = observable.Where((s, i) => new int[] { 0, 2, 3 }.Contains(i)).Do(_ => Debug.Log(_));

        print1skip1.Subscribe();

        subject.OnNext("1");
        subject.OnNext("2");
        subject.OnNext("3");
        subject.OnNext("4");

    }

    [Test]
    public void ContinueWith()
    {
        new int[] { 1, 2, 3 }.ToObservable().ContinueWith(new int[] { 2, 2, 3 }.ToObservable()).Subscribe(_ => Debug.Log(_));
        //2, 2, 3
    }

    [Test]
    public void ContinueWith2()
    {
        new int[] { 1 }.ToObservable()
            .ContinueWith(Observable.Empty(1))
            .ContinueWith(new string[] { "3", "2" }.ToObservable())
            .ContinueWith(new int[] { 0 }.ToObservable())
            
            .Subscribe(_ => Debug.Log(_));
    }

    [Test]
    public void Interactable()
    {
        new int[] { 1, 2, 3 }.
            ToObservable()
            .Select(i => i > 2).
            SubscribeToInteractable(
                new GameObject().AddComponent<Button>());
    }

    [Test]
    public void ReactiveCommand()
    {

        var button = new GameObject().AddComponent<Button>();

        var command = new int[] { 1, 2, 3 }.ToObservable().Select(ç => ç != 1).ToReactiveCommand<int>();
        command.Subscribe(_ => Debug.Log("Click"));

        command.CanExecute.BindToButtonOnClick(button, _ => Debug.Log("Click"));


    }
    [Test]
    public void Distinct()
    {
        Func<int, int> restOf5 = (x) => x % 5;

        Observable.Range(1, 10)
            .Select(restOf5)
            .Distinct().Subscribe(_ => Debug.Log(_));
    }

    [Test]
    public void BathFrameParesDepois()
    {
        var obsver = Observable.Range(1, 10).Select(p => p.ToString());

        obsver.Where((x, i) => i % 2 == 0).DelayFrame(400).DoOnCompleted(() => Debug.Log("pares acabaram"))
            .Merge(obsver.Where((x, i) => i % 2 == 1).DoOnCompleted(() => Debug.Log("inpares acabaram")))
            .BatchFrame(29, FrameCountType.Update)
            .Subscribe(_ => Debug.Log(_.Aggregate((f, s) => f + s)));
    }
   

    [Test]
    public void DistincMerge()
    {
        var observer = new int[] { 1, 2, 3, 4, 5, 6 }.ToObservable();

        var observerX2 = observer.Select(_ => _ * 2);

        observer.Merge(observerX2.DelayFrame(20)).Distinct().Select(_ => _.ToString()).Aggregate((x, i) => x + " " + i).Subscribe(l => Debug.Log(l));
        //1 2 3 4 5 6 8 10 12
    }

    [UnityTest]
    public IEnumerator ThrottleFirst()
    {
        Subject<int> firer = new Subject<int>();
        firer.ToReactiveProperty().ThrottleFirstFrame(2).Subscribe(_ => Debug.Log(_));
     
        firer.OnNext(1);
        yield return null;
        firer.OnNext(2);
        yield return null;
        firer.OnNext(3);
        yield return null;
        yield return null;
        firer.OnNext(4);
       

        LogAssert.Expect(LogType.Log, "1");
        LogAssert.Expect(LogType.Log, "4");

        LogAssert.NoUnexpectedReceived();

    }

    [UnityTest]
    public IEnumerator DelaySubscription()
    {
        Subject<int> firer = new Subject<int>();
        firer.DelaySubscription(DateTimeOffset.FromUnixTimeSeconds(1)).Subscribe(_ => Debug.Log(_));
        firer.OnNext(1);

        for(int i = 0; i < 1000; i++)
            yield return null;

        //prints nothing. What the hell?

    }

    [UnityTest]
    public IEnumerator Finally()
    {
        Subject<int> firer = new Subject<int>();

        firer.AsObservable().Catch((Exception e) => Observable.Empty<int>()).Finally(() => Debug.Log("Ue"));
        
        yield return null;
        firer.OnError(new Exception(""));
        firer.OnCompleted();
        //does nothing?
    }

    [Test]
    public void Replay()
    {
        new int[] { 1, 2, 3 }.ToObservable().Replay().Subscribe(_ => Debug.Log(_));
        //what does this do?
    }

    
}