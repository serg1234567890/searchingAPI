using System;
using submissionstorage.Entities.Searching;
using Xunit;

namespace XUnitTestSubmissionstorage
{
    public class UnitTestEntities
    {
        [Fact]
        public void Test_Submission_type()
        {
            Submission_type type = new Submission_type("text");
            type.Id = 1;
            Assert.Equal(1, type.Id);
            Assert.Equal("text", type.Name);

            Submission s = new Submission("Testing TEXT", type.Id);
            s.Id = 10;
            s.Type = type;

            Assert.Equal(10, s.Id);
            Assert.Equal("Testing TEXT", s.Fieldvalue);
            Assert.Equal(1, s.Submission_typeId);

            Assert.NotNull(s.Type);
            Assert.Equal(1, s.Type.Id);
            Assert.Equal("text", s.Type.Name);
        }
    }
}
