﻿/*
 * SonarQube Scanner for MSBuild
 * Copyright (C) 2016-2017 SonarSource SA
 * mailto:info AT sonarsource DOT com
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program; if not, write to the Free Software Foundation,
 * Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */
 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SonarQube.Common;
using SonarScanner.Shim;
using System.Collections.Generic;

namespace SonarQube.TeamBuild.PostProcessor.Tests
{
    internal class MockSonarScanner : ISonarScanner
    {
        private bool methodCalled;

        #region Test Helpers

        public string ErrorToLog { get; set; }

        public ProjectInfoAnalysisResult ValueToReturn { get; set; }

        public IEnumerable<string> SuppliedCommandLineArgs { get; set; }

        #endregion

        #region ISonarScanner interface

        public ProjectInfoAnalysisResult Execute(AnalysisConfig config, IEnumerable<string> userCmdLineArguments, ILogger logger)
        {
            Assert.IsFalse(this.methodCalled, "Scanner should only be called once");
            this.methodCalled = true;
            this.SuppliedCommandLineArgs = userCmdLineArguments;
            if (ErrorToLog != null)
            {
                logger.LogError(this.ErrorToLog);
            }

            return this.ValueToReturn;
        }

        #endregion

        #region Checks

        public void AssertExecuted()
        {
            Assert.IsTrue(this.methodCalled, "Expecting the sonar-scanner to have been called");
        }

        public void AssertNotExecuted()
        {
            Assert.IsFalse(this.methodCalled, "Not expecting the sonar-scanner to have been called");
        }

        #endregion
    }
}
