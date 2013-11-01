﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelData.cs" company="Copyright © 2013 Tekla Corporation. Tekla is a Trimble Company">
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

namespace ExtensionViewModel.ViewModel
{
    using System.Collections;
    using System.Collections.Generic;

    using ExtensionTypes;

    /// <summary>
    /// The extension data model.
    /// </summary>
    public partial class ExtensionDataModel
    {
        /// <summary>
        ///     The all resource data.
        /// </summary>
        private readonly Dictionary<string, Resource> allResourceData;

        /// <summary>
        ///     The all source coverage data.
        /// </summary>
        private readonly Dictionary<string, SourceCoverage> allSourceCoverageData;

        /// <summary>
        ///     The all source data.
        /// </summary>
        private readonly Dictionary<string, Source> allSourceData;

        /// <summary>
        ///     The allissues date.
        /// </summary>
        private readonly Dictionary<string, List<Issue>> cachedIssuesList;

        /// <summary>
        ///     The cached issues list obs.
        /// </summary>
        private readonly List<string> cachedIssuesListObs;

        /// <summary>
        ///     The comments.
        /// </summary>
        private List<Comment> comments = new List<Comment>();

        /// <summary>
        ///     The issues.
        /// </summary>
        private List<Issue> issues = new List<Issue>();

        /// <summary>
        ///     The issues in editor.
        /// </summary>
        private List<Issue> issuesInEditor = new List<Issue>();

        /// <summary>
        ///     The selected issue.
        /// </summary>
        private Issue selectedIssue;

        /// <summary>
        ///     The selected issues in view.
        /// </summary>
        private IList updateSelectedIssuesInView = new List<Issue>();

        /// <summary>
        ///     The users.
        /// </summary>
        private List<User> usersList = new List<User>();

        /// <summary>
        /// The clear caches.
        /// </summary>
        public void ClearCaches()
        {
            this.allResourceData.Clear();
            this.allSourceCoverageData.Clear();
            this.allSourceData.Clear();
            this.cachedIssuesList.Clear();
            this.cachedIssuesListObs.Clear();
            this.OnPropertyChanged("CachedIssuesListObs");
            this.OnPropertyChanged("CachedIssuesListObs");
            this.IssuesInEditor.Clear();
            this.Issues.Clear();
            this.Comments.Clear();
            this.updateSelectedIssuesInView.Clear();
        }
    }
}