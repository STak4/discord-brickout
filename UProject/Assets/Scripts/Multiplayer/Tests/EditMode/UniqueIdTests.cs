using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace STak4.brickout.Multiplayer.Tests
{
    public class UniqueIdTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void UniqueIdTestsSimplePasses()
        {
            // Use the Assert class to test conditions
            var a = new UniqueId();
            var b = new UniqueId();
            Debug.Log($"[TEST][UniqueId] {a},{b}");
            Assert.That(a.Id != b.Id);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator UniqueIdTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            var a = new UniqueId();
            yield return null;
            var b = new UniqueId();
            yield return null;
            Debug.Log($"[TEST][UniqueId] {a},{b}");
            Assert.That(a.Id != b.Id);
        }
    }
}