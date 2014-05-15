using System.Linq;
using FileInfoBussiness;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace FileInfoTests
{
    [TestFixture]
    public class FileInfoTests
    {
        [Test]
        public void RetrieveEmptyContent()
        {
            var service = new FileInfoFinder();
            var repository = new Mock<FileInfoRepository>();
            service.Repository = repository.Object;

            repository.Setup(x => x.GetContent(It.IsAny<string>()))
                .Returns(string.Empty);

            var result = service.Analize("pattern");

            result.Keys.Count.Should().Be(0);
        }

        [Test]
        public void RetrieveOneKey()
        {
            var service = new FileInfoFinder();
            var repository = new Mock<FileInfoRepository>();
            service.Repository = repository.Object;

            repository.Setup(x => x.GetContent(It.IsAny<string>()))
                .Returns("Quijote");

            var result = service.Analize("pattern");

            result.Keys.Count.Should().Be(1);
            result.Keys.First().Key.Should().Be("quijote");
            result.Keys.First().Value.Should().Be(1);
        }
    }
}
