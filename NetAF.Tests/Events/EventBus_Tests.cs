using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Events;
using System;

namespace NetAF.Tests.Logging.Events
{
    [TestClass]
    public class EventBus_Tests
    {
        [TestMethod]
        public void GivenNoSubscribers_WhenPublish_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                EventBus.UnsubscribeAll();
                EventBus.Publish(new BaseEvent());
            });
        }

        [TestMethod]
        public void GivenSubscriber_WhenPublish_ThenCallbackInvoked()
        {
            var result = false;
            EventBus.UnsubscribeAll();
            var callback = new Action<BaseEvent>(x => result = true);
            EventBus.Subscribe(callback);

            EventBus.Publish(new BaseEvent());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Givens2Subscribers_WhenPublish_ThenBothCallbacksInvoked()
        {
            var result1 = false;
            var result2 = false;
            EventBus.UnsubscribeAll();
            var callback1 = new Action<BaseEvent>(x => result1 = true);
            var callback2 = new Action<BaseEvent>(x => result2 = true);
            EventBus.Subscribe(callback1);
            EventBus.Subscribe(callback2);

            EventBus.Publish(new BaseEvent());

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void GivenSubscribeAndUnsubscribe_WhenPublish_ThenCallbackNotInvoked()
        {
            var result = false;
            EventBus.UnsubscribeAll();
            var callback = new Action<BaseEvent>(x => result = true);
            EventBus.Subscribe(callback);
            EventBus.Unsubscribe(callback);

            EventBus.Publish(new BaseEvent());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Givens2SubscribersThenUnsubscribeAll_WhenPublish_ThenNeitherCallbacksInvoked()
        {
            var result1 = false;
            var result2 = false;
            var callback1 = new Action<BaseEvent>(x => result1 = true);
            var callback2 = new Action<BaseEvent>(x => result2 = true);
            EventBus.Subscribe(callback1);
            EventBus.Subscribe(callback2);
            EventBus.UnsubscribeAll();

            EventBus.Publish(new BaseEvent());

            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }
    }
}
