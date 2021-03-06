// <copyright file="StringExtentionsTest.cs" company="Personal">Copyright ©  2017</copyright>
using System;
using CavinsStuff.Extentionsm;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CavinsStuff.Extentionsm.Tests
{
	/// <summary>This class contains parameterized unit tests for StringExtentions</summary>
	[PexClass(typeof(StringExtentions))]
	[PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
	[PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
	[PexAssumeNotNull()]
	[TestClass]
	public partial class StringExtentionsTest
	{
		/// <summary>Test stub for Right(String, Int32)</summary>
		[PexMethod]
		public string RightTest(string intialString, int length)
		{
			string result = StringExtentions.Right(intialString, length);

			return result;
			// TODO: add assertions to method StringExtentionsTest.RightTest(String, Int32)
		}
	}
}
