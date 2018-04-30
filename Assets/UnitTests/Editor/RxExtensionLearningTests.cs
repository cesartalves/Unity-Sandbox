using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UniRx;


public class RxExtensionLearningTests {

	[TestCase(new int[] {1, 2, 3})]
	public void Scan(int[] array) {

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

    [TestCase(1,2,3,4)]
    public void PairWise(params int[]  args)
    {
        args.ToObservable().Pairwise().Subscribe(_ => Debug.Log(_.Current));
        LogAssert.Expect(LogType.Log, "2");
        LogAssert.Expect(LogType.Log, "3");
        LogAssert.Expect(LogType.Log, "4");
    }

    [TestCase(100,200,300,400)]
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

    [TestCase(1,2,3,4,5, ExpectedResult = 5)]
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

    [TestCase(1,2,3)]
    public void Zip_2_2(params int[] args)
    {
        args.
            ToObservable()
            .ZipLatest(
                new int[] { 2, 2}
                    .ToObservable()
                    
                ,
                (x, y) => x + y).Subscribe(value => Debug.Log(value));

        LogAssert.Expect(LogType.Log, "3");
        LogAssert.Expect(LogType.Log, "4");
    }
}

