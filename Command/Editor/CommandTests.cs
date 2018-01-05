namespace Utilities.Command
{
    using System.Collections;
    using System.Collections.Generic;

    using NUnit.Framework;

#if UNITY_EDITOR
    public static class CommandTests
    {
        private static readonly bool[] _commandBools = new bool[100];

        [Test]
        public static void TestSerial1()
        {
            Reset();
            var command = new SerialCommand();
            command.Add(() => SetToTrue(0));
            command.MoveNext();
            Compare(new[] {true});
        }

        [Test]
        public static void TestSerial2()
        {
            Reset();

            var command = new SerialCommand();
            command.Add(() => SetToTrue(0));
            command.Add(() => SetToTrue(1));

            command.MoveNext();
            Compare(new[] {true, false});
            command.MoveNext();
            Compare(new[] {true, true});
        }

        [Test]
        public static void TestSerial3()
        {
            Reset();

            var serialCommand = new SerialCommand();
            serialCommand.Add(() => SetToTrue(0));
            serialCommand.Add(() => SetToTrue(1));
            serialCommand.Add(() => SetToTrue(2));

            serialCommand.MoveNext();
            Compare(new[] {true, false, false});

            serialCommand.MoveNext();
            Compare(new[] {true, true, false});

            serialCommand.MoveNext();
            Compare(new[] {true, true, true});
        }

        [Test]
        public static void TestNestedSerial1()
        {
            Reset();

            var serialCommand1 = new SerialCommand();
            serialCommand1.Add(() => SetToTrue(0));

            var serialCommand2 = new SerialCommand();
            serialCommand2.Add(() => SetToTrue(1));

            var fullCommand = new SerialCommand();
            fullCommand.Add(serialCommand1);
            fullCommand.Add(serialCommand2);

            fullCommand.MoveNext();
            Compare(new[] {true, false});

            fullCommand.MoveNext();
            Compare(new[] {true, true});
        }

        [Test]
        public static void TestNestedSerial2()
        {
            Reset();

            var serialCommand1 = new SerialCommand();
            serialCommand1.Add(() => SetToTrue(0));
            serialCommand1.Add(() => SetToTrue(1));

            var serialCommand2 = new SerialCommand();
            serialCommand2.Add(() => SetToTrue(2));
            serialCommand2.Add(() => SetToTrue(3));

            var fullCommand = new SerialCommand();

            fullCommand.Add(serialCommand1);
            fullCommand.Add(serialCommand2);

            fullCommand.MoveNext();
            Compare(new[] {true, false, false, false});
            fullCommand.MoveNext();

            Compare(new[] {true, true, false, false});

            fullCommand.MoveNext();
            Compare(new[] {true, true, true, false});
            fullCommand.MoveNext();
            Compare(new[] {true, true, true, true});
        }

        [Test]
        public static void TestNestedSerial3()
        {
            Reset();

            var serialCommand1 = new SerialCommand();
            serialCommand1.Add(() => SetToTrue(0));
            serialCommand1.Add(() => SetToTrue(1));
            serialCommand1.Add(() => SetToTrue(2));

            var serialCommand2 = new SerialCommand();
            serialCommand2.Add(() => SetToTrue(3));
            serialCommand2.Add(() => SetToTrue(4));
            serialCommand2.Add(() => SetToTrue(5));

            var fullCommand = new SerialCommand();
            fullCommand.Add(serialCommand1);
            fullCommand.Add(serialCommand2);

            fullCommand.MoveNext();
            Compare(new[] {true, false, false, false, false, false});

            fullCommand.MoveNext();
            Compare(new[] {true, true, false, false, false, false});

            fullCommand.MoveNext();
            Compare(new[] {true, true, true, false, false, false});

            fullCommand.MoveNext();
            Compare(new[] {true, true, true, true, false, false});

            fullCommand.MoveNext();
            fullCommand.MoveNext();
            fullCommand.MoveNext();
            fullCommand.MoveNext();
            fullCommand.MoveNext();
            fullCommand.MoveNext();
            fullCommand.MoveNext();
            Compare(new[] {true, true, true, true, true, true});
        }

        [Test]
        public static void TestSerialEnumerator1()
        {
            Reset();

            var serialCommand = new SerialCommand();
            serialCommand.Add(SetToTrueEnumerator(0));
            serialCommand.Add(() => SetToFalse(0));
            serialCommand.Run();

            Compare(new[] {false});
        }

        [Test]
        public static void TestSerialEnumerator3()
        {
            Reset();

            var serialCommand = new SerialCommand();
            serialCommand.Add(() => SetToFalse(0));
            serialCommand.Add(SetToTrueEnumerator(0));
            serialCommand.Add(() => SetToFalse(0));
            serialCommand.Run();

            Compare(new[] {false});
        }

        [Test]
        public static void TestParallel2()
        {
            Reset();

            var parallelCommand = new ParallelCommand();
            parallelCommand.Add(() => SetToTrue(0));
            parallelCommand.Add(() => SetToTrue(1));

            parallelCommand.MoveNext();
            Compare(new[] {true, true});
        }

        [Test]
        public static void TestParallelEnumerator2()
        {
            Reset();

            var parallelCommand = new ParallelCommand();
            parallelCommand.Add(() => SetToFalse(0));
            parallelCommand.Add(SetToTrueEnumerator(0));
            parallelCommand.Run();
            Compare(new[] {true});
        }

        [Test]
        public static void TestParallelEnumerator3()
        {
            Reset();

            var parallelCommand = new ParallelCommand();
            parallelCommand.Add(() => SetToFalse(0));
            parallelCommand.Add(SetToTrueEnumerator(0));
            parallelCommand.Add(() => SetToFalse(0));
            parallelCommand.Run();

            Compare(new[] {true});
        }

        [Test]
        public static void TestParallelEnumerator8()
        {
            Reset();

            var parallelCommand = new ParallelCommand();
            parallelCommand.Add(SetToTrueEnumerator(0));
            parallelCommand.Add(SetToTrueEnumerator(1));

            var serialCommand = new SerialCommand();
            serialCommand.Add(() => SetToFalse(0));
            serialCommand.Add(() => SetToFalse(1));

            var fullCommand = new SerialCommand();
            fullCommand.Add(parallelCommand);
            fullCommand.Add(serialCommand);

            fullCommand.Run();
            Compare(new[] {false, false});
        }

        [Test]
        public static void TestComposite1()
        {
            Reset();

            var serialCommand = new SerialCommand();
            serialCommand.Add(() => SetToTrue(0));

            var parallelCommand = new ParallelCommand();
            parallelCommand.Add(() => SetToFalse(0));
            parallelCommand.Add(() => SetToTrue(1));

            serialCommand.Add(parallelCommand);
            serialCommand.Run();

            Compare(new[] {false, true});
        }

        private static void SetToTrue(int boolIndex)
        {
            _commandBools[boolIndex] = true;
        }

        private static void SetToFalse(int boolIndex)
        {
            _commandBools[boolIndex] = false;
        }

        private static IEnumerator SetToTrueEnumerator(int boolIndex)
        {
            yield return null;
            yield return null;
            yield return null;
            yield return null;

            SetToTrue(boolIndex);
            yield return null;
            yield return null;
        }

        private static void Reset()
        {
            for (var i = 0; i < _commandBools.Length; i++)
            {
                _commandBools[i] = false;
            }
        }

        private static void Compare(IReadOnlyList<bool> bools)
        {
            for (var i = 0; i < bools.Count; i++)
            {
                Assert.AreEqual(_commandBools[i], bools[i]);
            }
        }
    }
#endif
}
