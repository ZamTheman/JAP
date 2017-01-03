using System.Collections.Generic;
using NUnit.Framework;
using Managers;
using Models;
using Moq;
using Utils;

namespace PlattformerTests
{
    //[TestFixture]
    //public class MapManagerTests
    //{
    //    private MockRepository mockRepository = new MockRepository(MockBehavior.Strict);
    //    private Map mockMap = new Map(new List<List<char>>
    //    {
    //        new List<char>{'a', 'b', 'c'},
    //        new List<char>{'d', 'e', 'f'},
    //        new List<char>{'g', 'h', 'i'}
    //    },
    //        new Dictionary<char, int[]>()
    //        {
    //            ['a'] = new[] { 0, 0},
    //            ['b'] = new[] { 1, 0 },
    //            ['c'] = new[] { 2, 0 }
    //        },
    //        new Texture2D());

    //    [Test]
    //    public void ReadANewMap_NoInput_GetNewMapWasCalled()
    //    {
    //        // Arrange
    //        var mapManager = new MapManager();
    //        var mockReader = mockRepository.Create<IReader>();
    //        mockReader.Setup(m => m.GetNewMap()).Returns(mockMap);

    //        // Act
    //        mapManager.ReadNewMap(mockReader.Object);

    //        // Assert
    //        mockReader.Verify(m => m.GetNewMap());
    //        mockReader.VerifyAll();
    //    }
    //}
}
