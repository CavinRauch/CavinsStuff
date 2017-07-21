using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CavinsStuff.Extentions;

namespace CavinsStuffUnitTests.ExtentionTests
{
	[TestClass]
	public class StringExtentionTests
	{
		#region Right Tests

		[TestMethod]
		public void Right_CorrectResult()
		{
			// Set test objects	
			var testObject = "ThisIsTheTest";
			var expectedResult = "Test";
			var parameterValue = 4;

			// Call method
			var result = testObject.Right(parameterValue);

			// Test Result
			Assert.IsTrue(result.Equals(expectedResult));
		}

		#endregion

		#region IsGuid Tests

		[TestMethod]
		public void IsNotGuid_NoException()
		{
			// Test Varables	
			var testObject = "Not a Guid";

			// Call Method
			var success = testObject.IsGuid();

			// Assert
			Assert.IsFalse(success);
		}

		[TestMethod]
		public void IsGuid_NoException()
		{
			// Test Varables
			var testObject = Guid.NewGuid().ToString();

			// Call Method
			var success = testObject.IsGuid();

			// Assert Test
			Assert.IsTrue(success);
		}

		#endregion

		#region ToGuid Tests

		[TestMethod]
		public void ToGuid_SuccessConvert()
		{
			// Test Varables
			var testObject = "64772925-2e66-4cce-b833-ce534384594e";

			// Call Method
			var newGuid = testObject.ToGuid();

			// Assert
			Assert.IsInstanceOfType(newGuid, typeof(Guid));
			Assert.IsTrue(newGuid != Guid.Empty);
		}

		[TestMethod]
		public void ToGuid_UnsuccessfulConvert()
		{
			try
			{
				// Test Varables
				var testObject = "6-2e66-4cce-b833-ce534384594e";

				// Call Method
				var newGuid = testObject.ToGuid();

				// Assert			
				Assert.Fail();
			}
			catch (InvalidCastException)
			{
				//Passed test
			}
			catch (Exception)
			{
				Assert.Fail();
			}
		}

		#endregion

		#region IsNullOrEmpty Tests

		[TestMethod]
		public void IsNullOrEmpty_NullSuccess()
		{
			// Test Varables
			string testObject = null;

			// Assert
			Assert.IsTrue(testObject.IsNullOrEmpty());
		}

		[TestMethod]
		public void IsNullOrEmpty_EmptySuccess()
		{
			// Test Varables
			var testObject = string.Empty;

			// Assert
			Assert.IsTrue(testObject.IsNullOrEmpty());
		}

		[TestMethod]
		public void IsNullOrEmpty_NotNullOrEmpty()
		{
			// Test Varables
			string testObject = "adsfasdf";

			// Assert
			Assert.IsFalse(testObject.IsNullOrEmpty());
		}

		#endregion
	}
}
