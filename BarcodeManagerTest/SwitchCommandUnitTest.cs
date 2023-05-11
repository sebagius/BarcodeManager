
namespace BarcodeManagerTest
{
    public class Tests
    {
        private TerminalWindow _window;
        private SwitchCommand _command;


        [SetUp]
        public void Setup()
        {
            Program.TEST_ENVIRONMENT = true;
            _window = new TerminalWindow();
            _command = new SwitchCommand();
        }

        [Test]
        public void TestSwitchStock()
        {
            Assert.IsTrue(_command.Process(new string[] {"switch", "stock"}, _window));
        }

        [Test]
        public void TestSwitchRegistry()
        {
            Assert.IsTrue(_command.Process(new string[] { "switch", "registry" }, _window));
            Assert.IsTrue(_command.Process(new string[] { "switch", "reg" }, _window));
        }

        [Test]
        public void TestSwitchInvalid()
        {
            Assert.IsFalse(_command.Process(new string[] { "switch", "hello" }, _window));
        }
    }
}