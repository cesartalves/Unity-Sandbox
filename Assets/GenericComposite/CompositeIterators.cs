using System.Collections;
using System.Collections.Generic;

public class CompositeIterator<T> : IEnumerator<GenericComposite<T>>
{
    //FIFO: BREADTH-FIRST
    private List<GenericComposite<T>> stack = new List<GenericComposite<T>>();
    protected GenericComposite<T> current;

    protected CompositeIterator() { }

    public CompositeIterator(GenericComposite<T> root)
    {
        stack.Add(root);
    }

    public GenericComposite<T> Current
    {
        get
        {
            return current;
        }
    }

    public virtual bool MoveNext()
    {
        if (stack.Count == 0) return false;

        current = stack[0];
        stack.RemoveAt(0);

        if (current.children.Count > 0)
            stack.AddRange(current.children);

        return true;

    }

    object IEnumerator.Current
    {
        get
        {
            return current;
        }
    }

    public void Dispose() { stack = null; }

    public void Reset() { }

}
public class PostOrderCompositeIterator<T> : CompositeIterator<T>
{
    protected List<GenericComposite<T>> stack = new List<GenericComposite<T>>();
    protected GenericComposite<T> root;

    protected PostOrderCompositeIterator() { }
    public PostOrderCompositeIterator(GenericComposite<T> root)
    {
        this.root = root;
        SetUpStack(root);
    }


    public override bool MoveNext()
    {
        if (stack.Count == 0) return false;

        current = stack[0];
        stack.RemoveAt(0);

        return true;

    }

    protected virtual void SetUpStack(GenericComposite<T> root)
    {
        if (root.children.Count > 0)
        {
            for (int i = 0; i < root.children.Count; i++)
            {
                SetUpStack(root.children[i]);
            }

            stack.Add(root);
        }
        else stack.Add(root);
    }

}
public class InOrderCompositeIterator<T> : PostOrderCompositeIterator<T>
{
    public InOrderCompositeIterator(GenericComposite<T> root) : base(root) { }

    protected override void SetUpStack(GenericComposite<T> root)
    {
        if (root.children.Count == 0)
        {
            stack.Add(root);
            return;

        }
        for (int i = 0; i < root.children.Count; i++)
        {
            SetUpStack(root.children[i]);
            if (!stack.Contains(root))
                stack.Add(root);

        }

    }
}
public class PreOrderCompositeIterator<T> : PostOrderCompositeIterator<T>
{
    protected PreOrderCompositeIterator() { }
    public PreOrderCompositeIterator(GenericComposite<T> root) : base(root) { }

    protected override void SetUpStack(GenericComposite<T> root)
    {
        if (root.children.Count == 0)
        {
            stack.Add(root);
            return;

        }
        for (int i = 0; i < root.children.Count; i++)
        {
            if (!stack.Contains(root))
                stack.Add(root);

            SetUpStack(root.children[i]);

        }

    }
}
