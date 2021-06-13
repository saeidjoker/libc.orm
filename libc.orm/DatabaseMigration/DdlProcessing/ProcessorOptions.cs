﻿#region License

//
// Copyright (c) 2018, Fluent Migrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

using System;

namespace libc.orm.DatabaseMigration.DdlProcessing
{
    /// <summary>
    ///     Options for an <see cref="IProcessor" />
    /// </summary>
    public sealed class ProcessorOptions
    {
        /// <summary>
        ///     Gets or sets the connection string (will not be used when <see cref="PreviewOnly" /> is active)
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether a preview-only mode is active
        /// </summary>
        public bool PreviewOnly { get; set; }

        /// <summary>
        ///     Gets or sets the default command timeout
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        ///     Gets or sets the provider switches
        /// </summary>
        public string ProviderSwitches { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the comments should be stripped
        /// </summary>
        public bool StripComments { get; set; }

        /// <inheritdoc />
        private int? TimeoutSeconds => Timeout == null ? null : (int?) Timeout.Value.TotalSeconds;
    }
}