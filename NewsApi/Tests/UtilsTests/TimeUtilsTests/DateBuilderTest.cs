using System;
using NewsApi.Utils.TimeUtils;
using Xunit;

namespace Tests.UtilsTests.TimeUtilsTests
{
    public class DateBuilderTest
    {

        [Fact]
        public void CreateDate_ValidDate_SameDate()
        {
            // Arrange 
            string year = "1987";
            string month = "2";
            string day = "10";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.NotNull(date);
            Assert.Equal(new DateTime(1987, 2, 10), date.Value);
        }

        [Fact]
        public void CreateDate_ValidLeapDay_SameDate()
        {
            // Arrange 
            string year = "2000";
            string month = "2";
            string day = "29";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.NotNull(date);
            Assert.Equal(new DateTime(2000, 2, 29), date.Value);
        }

        [Fact]
        public void CreateDate_NonNumericYear_Null()
        {
            // Arrange 
            string year = "ABCD";
            string month = "2";
            string day = "10";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.Null(date);
        }

        [Fact]
        public void CreateDate_NonNumericMonth_Null()
        {
            // Arrange 
            string year = "1987";
            string month = "ABCD";
            string day = "10";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.Null(date);
        }

        [Fact]
        public void CreateDate_NonNumericDay_Null()
        {
            // Arrange 
            string year = "1987";
            string month = "2";
            string day = "ABCD";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.Null(date);
        }

        [Fact]
        public void CreateDate_EmptyYear_Null()
        {
            // Arrange 
            string year = "";
            string month = "2";
            string day = "10";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.Null(date);
        }

        [Fact]
        public void CreateDate_EmptyMonth_Null()
        {
            // Arrange 
            string year = "1987";
            string month = "";
            string day = "10";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.Null(date);
        }

        [Fact]
        public void CreateDate_EmptyDay_Null()
        {
            // Arrange 
            string year = "1987";
            string month = "2";
            string day = "";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.Null(date);
        }

        [Fact]
        public void CreateDate_DateTooLow_Null()
        {
            // Arrange 
            string year = "1987";
            string month = "2";
            string day = "0";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.Null(date);
        }

        [Fact]
        public void CreateDate_DateTooHigh_Null()
        {
            // Arrange 
            string year = "1987";
            string month = "2";
            string day = "33";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.Null(date);
        }

        [Fact]
        public void CreateDate_MonthTooLow_Null()
        {
            // Arrange 
            string year = "1987";
            string month = "0";
            string day = "10";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.Null(date);
        }

        [Fact]
        public void CreateDate_MonthTooHigh_Null()
        {
            // Arrange 
            string year = "1987";
            string month = "13";
            string day = "10";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.Null(date);
        }

        [Fact]
        public void CreateDate_NonExistingLeapDay_Null()
        {
            // Arrange 
            string year = "1999";
            string month = "2";
            string day = "29";

            // Act
            var date = DateBuilder.CreateDate(year, month, day);

            // Assert
            Assert.Null(date);
        }
    }
}