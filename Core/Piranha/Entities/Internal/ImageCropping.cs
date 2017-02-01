/*
 * Copyright (c) 2011-2015 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using System;

namespace Piranha.Models
{
    /// <summary>
	/// Enum defining the different types of vertical scaling options.
	/// </summary>
	[Flags]
    public enum VerticalCropping
    {
        Top,
        Center,
        Bottom
    }

    /// <summary>
    /// Enum defining the different types of horizontal scaling options.
    /// </summary>
    [Flags]
    public enum HorizontalCropping
    {
        Left,
        Center,
        Right
    }
}