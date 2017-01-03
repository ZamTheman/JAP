using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Utils;

namespace PlattformerTests
{
    [TestFixture]
    public class FileReaderTests
    {
        MockRepository mockRepository = new MockRepository(MockBehavior.Strict);

        [Test]
        public void FileReaderConstructor_CalledTwice_ReturnsSingleton()
        {
            // Arrange
            var reader = FileReader.Instance;
            var reader2 = FileReader.Instance;
            
            // Assert
            Assert.That(reader, Is.EqualTo(reader2));
        }
    }
}
