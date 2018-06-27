using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public class AClass : A2ndInterface
{

}

public interface AInterface
{

}

public interface A2ndInterface : AInterface
{

}

public class AInheritedClass : AClass
{

}

public class TestReflection {

    [Test]
    public void NewTestScriptSimplePasses() {

        typeof(AInheritedClass).GetInterfaces().ToList().ForEach(_ => Debug.Log(_.ToString()));
        typeof(AInheritedClass).GetBase().ToList().ForEach(_ => Debug.Log(_.ToString()));
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }

   
}

internal static class Extension
{
    public static IEnumerable<Type> GetBase(this Type t)
    {
        var baseType = t.BaseType;

        while (baseType != null)
        {
            yield return baseType;
            baseType = baseType.BaseType;
        }
    }
}
