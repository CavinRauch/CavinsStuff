using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// <copyright file="StringExtentionsTest.RightTest.g.cs" company="Personal">Copyright ©  2017</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace CavinsStuff.Extentionsm.Tests
{
	public partial class StringExtentionsTest
	{

[TestMethod]
[PexGeneratedBy(typeof(StringExtentionsTest))]
public void RightTest803()
{
    string s;
    s = this.RightTest((string)null, 0);
    Assert.AreEqual<string>("", s);
}

[TestMethod]
[PexGeneratedBy(typeof(StringExtentionsTest))]
public void RightTest475()
{
    string s;
    s = this.RightTest("\0", 4);
    Assert.AreEqual<string>("\0", s);
}

[TestMethod]
[PexGeneratedBy(typeof(StringExtentionsTest))]
public void RightTest185()
{
    string s;
    s = this.RightTest("\0\0", 1);
    Assert.AreEqual<string>("\0", s);
}
	}
}
