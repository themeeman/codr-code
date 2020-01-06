using Codr.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass()]
public class HashedPasswordTest {
    [TestMethod()]
    public void TestHash() {
        var p1 = new HashedPassword("Password123");
        var p2 = new HashedPassword("Password123");
        Assert.IsTrue(p1.Password != p2.Password);
    }

    [TestMethod()]
    public void TestVerify() {
        var p1 = new HashedPassword("MyPassword");
        Assert.IsTrue(p1.Verify("MyPassword"));
        Assert.IsFalse(p1.Verify("WrongPassword"));
    }
}
