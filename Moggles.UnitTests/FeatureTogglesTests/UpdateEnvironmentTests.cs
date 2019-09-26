﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moggles.Controllers;
using Moggles.Domain;
using Moggles.Models;

namespace Moggles.UnitTests.FeatureTogglesTests
{
    [TestClass]
    public class UpdateEnvironmentTests
    {
        private IRepository<Application> _appRepository;

        [TestInitialize]
        public void BeforeTest()
        {
            _appRepository = new InMemoryApplicationRepository();
        }

        [TestMethod]
        public async Task EnvironmentIsBeingModified()
        {
            //arrange
            var app = Application.Create("TestApp", "DEV", false);
            await _appRepository.AddAsync(app);

            var controller = new FeatureTogglesController(_appRepository);

            var updatedEnvironmentName = "QA";

            var updatedEnvironment = new UpdateEnvironmentModel
            {
                ApplicationId = app.Id,
                InitialEnvName = "DEV",
                NewEnvName = updatedEnvironmentName
            };

            //act
            var result = await controller.UpdateEnvironment(updatedEnvironment);

            //assert
            result.Should().BeOfType<OkResult>();
            (await _appRepository.FindByIdAsync(app.Id)).DeploymentEnvironments.First().EnvName.Should().Be(updatedEnvironmentName);
        }

        [TestMethod]
        public async Task WhenNewInvironmentName_MatchesAnExistingEnvrionment_TheChangeIsRejected()
        {
            //arrange
            var app = Application.Create("TestApp", "DEV", false);
            await _appRepository.AddAsync(app);

            var controller = new FeatureTogglesController(_appRepository);

            var updatedEnvironment = new UpdateEnvironmentModel
            {
                ApplicationId = app.Id,
                InitialEnvName = "DEV",
                NewEnvName = "dev"
            };

            //act
            var result = await controller.UpdateEnvironment(updatedEnvironment);

            //assert
            result.Should().BeOfType<BadRequestObjectResult>();
            (await _appRepository.FindByIdAsync(app.Id)).DeploymentEnvironments.First().EnvName.Should().Be("DEV");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Environment does not exist!")]
        public async Task WhenEnvironmentIsModifiedWithInvalidID_ThrowsInvalidOperationException()
        {
            //arrange
            var app = Application.Create("TestApp", "DEV", false);
            await _appRepository.AddAsync(app);

            var controller = new FeatureTogglesController(_appRepository);

            var updatedEnvironmentName = "QA";

            var updatedEnvironment = new UpdateEnvironmentModel
            {
                ApplicationId = app.Id,
                InitialEnvName = "BLA",
                NewEnvName = updatedEnvironmentName
            };

            //act
            await controller.UpdateEnvironment(updatedEnvironment);

            //assert
            //throws InvalidOperationException
        }
    }
}