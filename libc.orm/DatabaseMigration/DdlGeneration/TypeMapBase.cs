﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using libc.orm.DatabaseMigration.Abstractions;
namespace libc.orm.DatabaseMigration.DdlGeneration {
    public abstract class TypeMapBase : ITypeMap {
        private const string SizePlaceholder = "$size";
        protected const string PrecisionPlaceholder = "$precision";
        private readonly Dictionary<DbType, SortedList<int, string>> _templates =
            new Dictionary<DbType, SortedList<int, string>>();
        protected TypeMapBase() {
            // ReSharper disable once VirtualMemberCallInConstructor
            SetupTypeMaps();
        }
        /// <inheritdoc />
        public virtual string GetTypeMap(DbType type, int? size, int? precision) {
            if (!_templates.ContainsKey(type))
                throw new NotSupportedException($"Unsupported DbType '{type}'");
            if (size == null)
                return ReplacePlaceholders(_templates[type][-1], 0, precision);
            var sizeValue = size.Value;
            foreach (var entry in _templates[type]) {
                var capacity = entry.Key;
                var template = entry.Value;
                if (sizeValue <= capacity)
                    return ReplacePlaceholders(template, sizeValue, precision);
            }
            throw new NotSupportedException($"Unsupported DbType '{type}'");
        }
        /// <inheritdoc />
        [Obsolete]
        public virtual string GetTypeMap(DbType type, int size, int precision) {
            return GetTypeMap(type, (int?) size, precision);
        }
        protected abstract void SetupTypeMaps();
        protected void SetTypeMap(DbType type, string template) {
            EnsureHasList(type);
            _templates[type][-1] = template;
        }
        protected void SetTypeMap(DbType type, string template, int maxSize) {
            EnsureHasList(type);
            _templates[type][maxSize] = template;
        }
        private void EnsureHasList(DbType type) {
            if (!_templates.ContainsKey(type))
                _templates.Add(type, new SortedList<int, string>());
        }
        private string ReplacePlaceholders(string value, int? size, int? precision) {
            if (size != null) value = value.Replace(SizePlaceholder, size.Value.ToString(CultureInfo.InvariantCulture));
            if (precision != null)
                value = value.Replace(PrecisionPlaceholder, precision.Value.ToString(CultureInfo.InvariantCulture));
            return value;
        }
    }
}