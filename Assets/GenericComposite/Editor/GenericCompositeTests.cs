using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class GenericCompositeTests {

    private GenericComposite<int> _composite;

    [SetUp]
    public void Create12345Tree()
    {
        GenericComposite<int> composite = new GenericComposite<int>(1);
        composite.
            AddChild(2).
                AddChild(4)
                    .AddSibling(5);

        composite.
            AddChild(3);

        _composite = composite;
    }

    [Test]
    public void TestCompositePostOrderIterator()
    {
        var iterator = new PostOrderCompositeIterator<int>(_composite);

        var expected = new int[] { 4, 5, 2, 3, 1 };
       
        Assert.AreEqual(expected, IterateToArray(iterator));
    }

    [Test]
    public void TestCompositeInOrderIterator()
    {
        var iterator = new InOrderCompositeIterator<int>(_composite);

        var expected = new int[] { 4, 2, 5, 1, 3 };
      
        Assert.AreEqual(expected, IterateToArray(iterator));
    }

    [Test]
    public void TestCompositePreOrderIterator()
    {
        var iterator = new PreOrderCompositeIterator<int>(_composite);

        var expected = new int[] { 1, 2, 4, 5, 3};
        
        Assert.AreEqual(expected, IterateToArray(iterator));
    }

    [Test]
    public void TestCompositeBreadthFirstIterator()
    {
        var iterator = new CompositeIterator<int>(_composite);

        var expected = new int[] { 1, 2, 3, 4, 5};
        Assert.AreEqual(expected, IterateToArray(iterator));
    }

    private T[] IterateToArray<T>(CompositeIterator<T> iterator)
    {
        var actual = new List<T>();
        while (iterator.MoveNext())
        {
            actual.Add(iterator.Current.element);
        }

        return actual.ToArray();
    }

    
}
