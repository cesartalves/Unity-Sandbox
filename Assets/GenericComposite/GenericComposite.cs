using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericComposite<T> : IEnumerable<GenericComposite<T>>
{
    public T element;
    public List<GenericComposite<T>> children;
    public GenericComposite<T> parent;

    protected CompositeIterator<T> _iterator;

    public GenericComposite(T element, GenericComposite<T> parent = null)
    {
        this.parent = parent;
        this.element = element;
        children = new List<GenericComposite<T>>();

    }

    public GenericComposite<T> AddChild(T element)
    {
        var n = new GenericComposite<T>(element, this);
        children.Add(n);

        return n;
    }

    public GenericComposite<T> AddSibling(T element)
    {
        if (this.parent == null)
            throw new System.Exception("Trying to add sibling to root of tree isnt supported");

        var n = new GenericComposite<T>(element, this.parent);
        this.parent.children.Add(n);

        return this;
    }

    [Zenject.Inject]
    public CompositeIterator<T> SetIterator
    {
        set
        {
            _iterator = value as CompositeIterator<T>;
        }
    }

    public IEnumerator<GenericComposite<T>> GetEnumerator()
    {
        if (_iterator != null) return _iterator;
        return new CompositeIterator<T>(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


