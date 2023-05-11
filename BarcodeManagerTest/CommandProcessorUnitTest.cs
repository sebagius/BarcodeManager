using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManagerTest
{
    public class CommandProcessorUnitTest
    {
        private TerminalWindow _window;
        private CommandProcessor _processor;


        [SetUp]
        public void Setup()
        {
            Program.TEST_ENVIRONMENT = true;
            _window = new TerminalWindow();
            _processor = new CommandProcessor(_window);
            _processor.Prepare();
        }

        [Test]
        public void TestCommandFound()
        {
            Assert.IsTrue(_processor.ProcessCommand("switch stock"));
        }

        [Test]
        public void TestCommandNotFound()
        {
            Assert.IsFalse(_processor.ProcessCommand("swhhsh stock"));
        }

        [Test]
        public void TestBasicAutoComplete()
        {
            Assert.That(_processor.AutoComplete("sw")![0]!, Is.EqualTo("itch <stock/registry>"));
            Assert.That(_processor.AutoComplete("switc"), Is.EqualTo("h <stock/registry>"));
            Assert.That(_processor.AutoComplete("switch <st"), Is.EqualTo("ock/registry>"));
            Assert.That(_processor.AutoComplete("s"), Is.EqualTo(""));
            Assert.That(_processor.AutoComplete("blah"), Is.EqualTo(""));
        }
    }
}
