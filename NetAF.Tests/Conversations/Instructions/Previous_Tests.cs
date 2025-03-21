﻿using NetAF.Conversations;
using NetAF.Conversations.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Conversations.Instructions
{
    [TestClass]
    public class Previous_Tests
    {
        [TestMethod]
        public void GivenCurrentlyOn0_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2")
            };
            var instruction = new Previous();

            var result = instruction.GetIndexOfNext(paragraphs[0], paragraphs);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenCurrentlyOn1_WhenNext_ThenReturn0()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2"),
                new Paragraph("Test3")
            };
            var instruction = new Previous();

            var result = instruction.GetIndexOfNext(paragraphs[1], paragraphs);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenCurrentlyOn2_WhenNext_ThenReturn1()
        {
            var paragraphs = new[]
            {
                new Paragraph("Test"),
                new Paragraph("Test2"),
                new Paragraph("Test3")
            };
            var instruction = new Previous();

            var result = instruction.GetIndexOfNext(paragraphs[2], paragraphs);

            Assert.AreEqual(1, result);
        }
    }
}
