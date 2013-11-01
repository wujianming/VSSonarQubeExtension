﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoverageTests.cs" company="Copyright © 2013 Tekla Corporation. Tekla is a Trimble Company">
//     Copyright (C) 2013 [Jorge Costa, Jorge.Costa@tekla.com]
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License
// as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
// of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details. 
// You should have received a copy of the GNU Lesser General Public License along with this program; if not, write to the Free
// Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// --------------------------------------------------------------------------------------------------------------------

namespace ExtensionViewModel.Test
{
    using System;
    using System.Collections.Generic;

    using ExtensionHelpers;

    using ExtensionTypes;

    using ExtensionViewModel.ViewModel;

    using NUnit.Framework;

    using Rhino.Mocks;

    using SonarRestService;

    /// <summary>
    /// The coverage tests.
    /// </summary>
    public class CoverageTests
    {
            /// <summary>
    /// The comment on issue command test.
    /// </summary>
    [TestFixture]
    public class NewWorkFlowTests
    {
        /// <summary>
        /// The mocks.
        /// </summary>
        private MockRepository mocks;

        /// <summary>
        /// The service.
        /// </summary>
        private ISonarRestService service;

        /// <summary>
        /// The vshelper.
        /// </summary>
        private IVsEnvironmentHelper vshelper;

        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.mocks = new MockRepository();
            this.service = this.mocks.Stub<ISonarRestService>();
            this.vshelper = this.mocks.Stub<IVsEnvironmentHelper>();

            using (this.mocks.Record())
            {
                SetupResult.For(this.service.GetServerInfo(Arg<ConnectionConfiguration>.Is.Anything)).Return(3.6);
                SetupResult.For(this.service.AuthenticateUser(Arg<ConnectionConfiguration>.Is.Anything)).Return(true);
                SetupResult.For(this.vshelper.ReadSavedOption("Sonar Options", "General", "SonarHost")).Return("serveraddr");
                SetupResult.For(this.vshelper.ReadSavedOption("Sonar Options", "General", "SonarUserPassword")).Return("password");
                SetupResult.For(this.vshelper.ReadSavedOption("Sonar Options", "General", "SonarUserName")).Return("login");
            }
        }

            /// <summary>
            /// The test loading of window.
            /// </summary>
            [Test]
            public void CoverageInEditorSetAndGetTest()
            {
                var data = new ExtensionDataModel(this.service, this.vshelper, null);
                var sourceCoverage = new SourceCoverage();
                data.CoverageInEditor = sourceCoverage;
                Assert.AreEqual(sourceCoverage, data.CoverageInEditor);
            }

            /// <summary>
            /// The test loading of window.
            /// </summary>
            [Test]
            public void DisableCoverageInEditor()
            {
                var data = new ExtensionDataModel(this.service, this.vshelper, null);
                var sourceCoverage = new SourceCoverage();
                data.CoverageInEditor = sourceCoverage;
                data.EnableCoverageInEditor = false;
                Assert.IsFalse(data.EnableCoverageInEditor);
                Assert.AreNotEqual(sourceCoverage, data.CoverageInEditor);
                Assert.AreEqual(0, data.CoverageInEditor.BranchHits.Count);
                Assert.AreEqual(0, data.CoverageInEditor.LinesHits.Count);
            }

            /// <summary>
            /// The enable coverage in editor.
            /// </summary>
            [Test]
            public void EnableCoverageInEditor()
            {
                var data = new ExtensionDataModel(this.service, this.vshelper, null);
                var sourceCoverage = new SourceCoverage();
                var source = new Source { Lines = new List<Line> { new Line { Id = 1, Val = "#include bal" }, new Line { Id = 2, Val = "#include bals" } } };
                var element = new Resource { Date = new DateTime(2000, 1, 1) };
                this.service.Expect(
                    mp => mp.GetResourcesData(Arg<ConnectionConfiguration>.Is.Anything, Arg<string>.Is.Equal("resource")))
                    .Return(new List<Resource> { element })
                    .Repeat.Twice();
                this.service.Expect(
                    mp => mp.GetSourceForFileResource(Arg<ConnectionConfiguration>.Is.Anything, Arg<string>.Is.Equal("resource")))
                    .Return(source);
                this.service.Expect(
                    mp => mp.GetCoverageInResource(Arg<ConnectionConfiguration>.Is.Anything, Arg<string>.Is.Equal("resource")))
                    .Return(sourceCoverage);
                element.Key = "resource";
                data.ResourceInEditor = element;
                data.EnableCoverageInEditor = true;
                Assert.IsTrue(data.EnableCoverageInEditor);
                Assert.AreEqual(sourceCoverage, data.CoverageInEditor);
                Assert.AreEqual(element, data.ResourceInEditor);
                Assert.AreEqual(0, data.CoverageInEditor.BranchHits.Count);
                Assert.AreEqual(0, data.CoverageInEditor.LinesHits.Count);
            }
        }
    }
}