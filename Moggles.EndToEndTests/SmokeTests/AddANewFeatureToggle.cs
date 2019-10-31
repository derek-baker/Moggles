﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moggles.EndToEndTests.TestFramework;
using MogglesEndToEndTests.TestFramework;
using NSTestFrameworkDotNetCoreUI.Helpers;

namespace MogglesEndToEndTests.SmokeTests
{
    [TestClass]
    public class AddANewFeatureToggle : BaseTest
    {       
        [TestMethod]
        [TestCategory("AddFeatureToggle")]
        [TestCategory("SmokeTests")]

        public void AddANewFeatureToggle_TheFeatureToggleIsAdded()
        {
            //act
            Browser.Goto(Constants.BaseUrl);
            Pages.FeatureTogglesPage.SelectASpecificApplication(Constants.SmokeTestsApplication);
            Pages.FeatureTogglesPage.AddFeatureToggle(Constants.FeatureToggleName);

            //assert
            Pages.FeatureTogglesPage.NewAddedFeatureToggleIsVisible(Constants.FeatureToggleName).Should().BeTrue();
            Pages.FeatureTogglesPage.CreationDateIsCorrectlyDisplayed(Constants.FeatureToggleName).Should().BeTrue();    
        }

        [TestCleanup]
        public override void After()
        {
            Pages.FeatureTogglesPage.DeleteFeatureToggle(Constants.FeatureToggleName);
            base.After();
        }
    }
}