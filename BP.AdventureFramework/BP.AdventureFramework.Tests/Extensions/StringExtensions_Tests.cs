﻿using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Extensions
{
    [TestClass]
    public class StringExtensions_Tests
    {
        [TestMethod]
        public void GivenEmptyString_WhenIsVowel_ThenFalse()
        {
            var result = "".IsVowel();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given123_WhenIsVowel_ThenFalse()
        {
            var result = "123".IsVowel();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenAAA_WhenIsVowel_ThenFalse()
        {
            var result = "AAA".IsVowel();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenZ_WhenIsVowel_ThenFalse()
        {
            var result = "Z".IsVowel();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenA_WhenIsVowel_ThenTrue()
        {
            var result = "A".IsVowel();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenE_WhenIsVowel_ThenTrue()
        {
            var result = "E".IsVowel();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenI_WhenIsVowel_ThenTrue()
        {
            var result = "I".IsVowel();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenO_WhenIsVowel_ThenTrue()
        {
            var result = "O".IsVowel();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenU_WhenIsVowel_ThenTrue()
        {
            var result = "U".IsVowel();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenEnsureFinishedSentence_ThenEmptyString()
        {
            var result = "".EnsureFinishedSentence();

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void GivenA_WhenEnsureFinishedSentence_ThenAStop()
        {
            var result = "A".EnsureFinishedSentence();

            Assert.AreEqual("A.", result);
        }

        [TestMethod]
        public void GivenAStop_WhenEnsureFinishedSentence_ThenAStop()
        {
            var result = "A.".EnsureFinishedSentence();

            Assert.AreEqual("A.", result);
        }

        [TestMethod]
        public void GivenAQuestion_WhenEnsureFinishedSentence_ThenAQuestion()
        {
            var result = "A?".EnsureFinishedSentence();

            Assert.AreEqual("A?", result);
        }

        [TestMethod]
        public void GivenAExclamation_WhenEnsureFinishedSentence_ThenAExclamation()
        {
            var result = "A!".EnsureFinishedSentence();

            Assert.AreEqual("A!", result);
        }

        [TestMethod]
        public void GivenAComma_WhenEnsureFinishedSentence_ThenAStop()
        {
            var result = "A,".EnsureFinishedSentence();

            Assert.AreEqual("A.", result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenIsPlural_ThenFalse()
        {
            var result = "".IsPlural();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenA_WhenIsPlural_ThenFalse()
        {
            var result = "A".IsPlural();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenFoxes_WhenIsPlural_ThenTrue()
        {
            var result = "Foxes".IsPlural();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenGetObjectifier_ThenEmptyString()
        {
            var result = "".GetObjectifier();

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void GivenTub_WhenGetObjectifier_ThenA()
        {
            var result = "Tub".GetObjectifier();

            Assert.AreEqual("a", result);
        }

        [TestMethod]
        public void GivenElephant_WhenGetObjectifier_ThenAn()
        {
            var result = "Elephant".GetObjectifier();

            Assert.AreEqual("an", result);
        }

        [TestMethod]
        public void GivenStringIsEqualToIdentifier_WhenEqualsIdentifier_ThenTrue()
        {
            var result = "A".EqualsIdentifier("A".ToIdentifier());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenStringIsNotEqualToIdentifier_WhenEqualsIdentifier_ThenFalse()
        {
            var result = "A".EqualsIdentifier("B".ToIdentifier());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenStringIsEqualToExaminable_WhenEqualsExaminable_ThenTrue()
        {
            var result = "A".EqualsExaminable(new Item("A".ToIdentifier(), Description.Empty, false));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenStringIsNotEqualToDescription_WhenEqualsDescription_ThenFalse()
        {
            var result = "A".EqualsExaminable(new Item("B".ToIdentifier(), Description.Empty, false));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenAString_WhenToIdentifier_ThenNotNull()
        {
            var result = "A".ToIdentifier();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenAString_WhenToDescription_ThenNotNull()
        {
            var result = "A".ToDescription();

            Assert.IsNotNull(result);
        }
    }
}