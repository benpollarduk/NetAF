using NetAF.Assets.Characters;
using NetAF.Interpretation;
using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Locations;
using NetAF.Assets;
using NetAF.Logic.Modes;

namespace NetAF.Tests.Interpretation
{
    [TestClass]
    public class InterpreterProvider_Tests
    {
        [TestMethod]
        public void GivenNoRegisteredInterpreter_WhenFind_ThenReturnNull()
        {
            var provider = new InterpreterProvider();

            var result = provider.Find(typeof(SceneMode));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenRegisteredInterpreter_WhenFind_ThenReturnRegisteredInterpreter()
        {
            var provider = new InterpreterProvider();
            var interpreter = new SceneCommandInterpreter();
            provider.Register(typeof(SceneMode), interpreter);

            var result = provider.Find(typeof(SceneMode));

            Assert.AreEqual(interpreter, result);
        }

        [TestMethod]
        public void GivenRegisteredInterpreterThenClear_WhenFind_ThenReturnNull()
        {
            var provider = new InterpreterProvider();
            provider.Register(typeof(SceneMode), new SceneCommandInterpreter());
            provider.Clear();

            var result = provider.Find(typeof(SceneMode));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenRegisteredInterpreterThenRemove_WhenFind_ThenReturnNull()
        {
            var provider = new InterpreterProvider();
            provider.Register(typeof(SceneMode), new SceneCommandInterpreter());
            provider.Remove(typeof(SceneMode));

            var result = provider.Find(typeof(SceneMode));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenNoRegisteredInterpreterThenRemove_WhenFind_ThenReturnNull()
        {
            var provider = new InterpreterProvider();
            provider.Remove(typeof(SceneMode));

            var result = provider.Find(typeof(SceneMode));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenRegisteredInterpreterThenRegisterASecond_WhenFind_ThenReturnSecond()
        {
            var provider = new InterpreterProvider();
            var interpreter1 = new SceneCommandInterpreter();
            var interpreter2 = new ConversationCommandInterpreter();
            provider.Register(typeof(SceneMode), interpreter1);
            provider.Register(typeof(SceneMode), interpreter2);

            var result = provider.Find(typeof(SceneMode));

            Assert.AreEqual(interpreter2, result);
        }
    }
}
