using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace FileInfoTests
{
    [TestFixture]
    public class FileInfoTests
    {
        [Test]
        public void RetrieveEmptyList()
        {
            var service = new FileInfoFinder();
            var repository = new Mock<FileInfoRepository>();
            service.Repository = repository.Object;

            repository.Setup(x => x.GetInfoByPattern(It.IsAny<string>()))
                .Returns(new List<FileInfoResult>());

            var result = service.Search("pattern");

            result.Count.Should().Be(0);
        }
    }

    public class FileInfoResult
    {
    }

    public class FileInfoRepository
    {
        public virtual List<FileInfoResult> GetInfoByPattern(string pattern)
        {
            throw new NotImplementedException();
        }
    }

    public class FileInfoFinder
    {
        public FileInfoRepository Repository { get; set; }

        public List<FileInfoResult> Search(string pattern)
        {
            return Repository.GetInfoByPattern(pattern);
        }
    }
}
