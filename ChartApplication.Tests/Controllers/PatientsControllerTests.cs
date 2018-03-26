using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ChartApplication;
using ChartApplication.Controllers;
using ChartApplication.Models;

namespace ChartApplication.Tests
{
    [TestClass]
    public class PatientsControllerTests
    {
        [TestClass]
        public class StaffControllerTests
        {
            [TestMethod]
            public void Test_Index_GivesAllCurrentPatients()
            {
                // Arrange
                var expected = new List<Patient>
            {
                new Patient { PatientId = 11, PatientFirst = "Bob" },
                new Patient { PatientId = 12, PatientFirst = "Brenda" },
                new Patient { PatientId = 3, PatientFirst = "Sally" },
                new Patient { PatientId = 44, PatientFirst = "Jack" },
            };

                var patientBackingStore = expected.AsQueryable();
                var mockDb = new Mock<ChartEntities>();
                var mockPatients = new Mock<DbSet<Patient>>();
                mockPatients.As<IQueryable<Patient>>().Setup(x => x.Provider).Returns(patientBackingStore.Provider);
                mockPatients.As<IQueryable<Patient>>().Setup(x => x.Expression).Returns(patientBackingStore.Expression);
                mockPatients.As<IQueryable<Patient>>().Setup(x => x.ElementType).Returns(patientBackingStore.ElementType);
                mockPatients.As<IQueryable<Patient>>().Setup(x => x.GetEnumerator())
                    .Returns(() => patientBackingStore.GetEnumerator());
                // mockStaff.Setup(x => x.Find(data)).Returns(staffToReturn);
                mockPatients.Setup(x => x.Include(It.IsAny<String>())).Returns(mockPatients.Object);
                mockDb.SetupGet(x => x.Patients).Returns(mockPatients.Object);
                var target = new PatientsController(mockDb.Object);


                // Act
                var viewResult = (ViewResult)target.Index() as ViewResult;
                var actual = (IEnumerable<Patient>)viewResult.Model;
                // Assert
                //Assert.AreEqual(expected.Select(x => actual.Contains(x)), actual.Select(x => expected.Contains(x)));

                Assert.AreEqual(expected.Count, actual.Count());
                Assert.AreEqual(0, expected.Except(actual).Count());
                Assert.AreEqual(0, actual.Except(expected).Count());
            }
        }
    }
}
