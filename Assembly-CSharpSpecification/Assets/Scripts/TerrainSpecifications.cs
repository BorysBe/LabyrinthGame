using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specification
{
    [TestClass()]
    public class TerrainSpecifications
    {
        [TestMethod()]
        public void ShouldMoveLeft()
        {
            //Arrange
            var terrain = new Terrain();

            //Act
            //terrain.Move(100.0);

            //Assert

            Assert.Fail();
        }

        [TestMethod()]
        public void ShouldMoveRight()
        {
            //Arrange

            //Act

            //Assert

            Assert.Fail();
        }
    }
}