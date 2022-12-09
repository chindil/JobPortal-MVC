﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stx.Shared.Common
{
    /// <summary>
    /// Represents the optgroup HTML element and its attributes.
    /// In a select list, multiple groups with the same name are supported.
    /// They are compared with reference equality.
    /// </summary>
    public class SelectListGroup
    {
        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="SelectListGroup"/> is disabled.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Represents the value of the optgroup's label.
        /// </summary>
        public string Name { get; set; }
    }
}
