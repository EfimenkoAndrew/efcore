// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Metadata.Internal;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
public class RelationalEntityTypeOverrides : ConventionAnnotatable, IRelationalEntityTypeOverrides
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public RelationalEntityTypeOverrides(IReadOnlyEntityType entityType, in StoreObjectIdentifier storeObject)
    {
        EntityType = entityType;
        StoreObject = storeObject;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual IReadOnlyEntityType EntityType { get; }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual StoreObjectIdentifier StoreObject { get; }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public override bool IsReadOnly
        => ((Annotatable)EntityType).IsReadOnly;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static IRelationalEntityTypeOverrides? Find(IReadOnlyEntityType entityType, in StoreObjectIdentifier storeObject)
    {
        var tableOverrides = (Dictionary<StoreObjectIdentifier, IRelationalEntityTypeOverrides>?)
            entityType[RelationalAnnotationNames.RelationalOverrides];
        return tableOverrides != null
            && tableOverrides.TryGetValue(storeObject, out var overrides)
                ? overrides
                : null;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static IEnumerable<IRelationalEntityTypeOverrides>? Get(IReadOnlyEntityType entityType)
    {
        var tableOverrides = (Dictionary<StoreObjectIdentifier, IRelationalEntityTypeOverrides>?)
            entityType[RelationalAnnotationNames.RelationalOverrides];
        return tableOverrides?.OrderBy(pair => pair.Key.Name, StringComparer.Ordinal)
            .Select(pair => (IRelationalEntityTypeOverrides)pair.Value);
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static RelationalEntityTypeOverrides GetOrCreate(
        IMutableEntityType entityType,
        in StoreObjectIdentifier storeObject)
    {
        var tableOverrides = (Dictionary<StoreObjectIdentifier, IRelationalEntityTypeOverrides>?)
            entityType[RelationalAnnotationNames.RelationalOverrides];
        if (tableOverrides == null)
        {
            tableOverrides = new Dictionary<StoreObjectIdentifier, IRelationalEntityTypeOverrides>();
            entityType[RelationalAnnotationNames.RelationalOverrides] = tableOverrides;
        }

        if (!tableOverrides.TryGetValue(storeObject, out var overrides))
        {
            overrides = new RelationalEntityTypeOverrides(entityType, storeObject);
            tableOverrides.Add(storeObject, overrides);
        }

        return (RelationalEntityTypeOverrides)overrides;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static RelationalEntityTypeOverrides GetOrCreate(
        IConventionEntityType entityType,
        in StoreObjectIdentifier storeObject)
        => GetOrCreate((IMutableEntityType)entityType, storeObject);

    /// <inheritdoc />
    IEntityType IRelationalEntityTypeOverrides.EntityType
    {
        [DebuggerStepThrough]
        get => (IEntityType)EntityType;
    }
}
