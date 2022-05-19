// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata;

/// <summary>
///     Represents property facet overrides for a particular table-like store object.
/// </summary>
/// <remarks>
///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and examples.
/// </remarks>
public class RuntimeRelationalEntityTypeOverrides : AnnotatableBase, IRelationalEntityTypeOverrides
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RuntimeRelationalEntityTypeOverrides" /> class.
    /// </summary>
    /// <param name="entityType">The entity type for which the overrides are applied.</param>
    /// <param name="storeObject">The store object for which the configuration is applied.</param>
    public RuntimeRelationalEntityTypeOverrides(
        RuntimeEntityType entityType,
        in StoreObjectIdentifier storeObject)
    {
        EntityType = entityType;
        StoreObject = storeObject;
    }

    /// <summary>
    ///     Gets the etity type for which the overrides should be applied.
    /// </summary>
    public virtual RuntimeEntityType EntityType { get; }

    /// <summary>
    ///     Gets store object for which the configuration is applied.
    /// </summary>
    public virtual StoreObjectIdentifier StoreObject { get; }

    /// <inheritdoc />
    IEntityType IRelationalEntityTypeOverrides.EntityType
    {
        [DebuggerStepThrough]
        get => EntityType;
    }
}
