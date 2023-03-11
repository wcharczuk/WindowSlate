using System.Windows.Forms;

namespace WindowSlate.Test
{
    [TestClass]
    public class HotKey_Test
    {
        [TestMethod]
        public void Test_HotKey_fromString_good()
        {
            var hk = new HotKey("win+alt+F");
            Assert.AreEqual(Keys.F, hk.Key);
            Assert.AreEqual(KeyModifiers.Windows | KeyModifiers.Alt, hk.KeyModifiers);
        }

        [TestMethod]
        public void Test_HotKey_fromString_good_casing()
        {
            var hk = new HotKey("WIN+ALT+F");
            Assert.AreEqual(Keys.F, hk.Key);
            Assert.AreEqual(KeyModifiers.Windows | KeyModifiers.Alt, hk.KeyModifiers);
        }

        [TestMethod]
        public void Test_HotKey_ToString()
        {
            var hk = new HotKey("win+alt+F");
            var hks = hk.ToString();
            Assert.AreEqual("win+alt+F", hks);
        }
    }
}
