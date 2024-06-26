﻿using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Extensions.Localization;
using VerticalSlice.Api.Shared.Resources;

namespace VerticalSlice.Api.Shared.Notifications;

public sealed class NotificationManager
{
    private readonly IStringLocalizer _localizer;

    public NotificationManager(IStringLocalizerFactory factory)
    {
        var type = typeof(SharedResource);
        var assemblyName = new AssemblyName(type.Assembly.FullName
                                            ?? throw new InvalidOperationException("Assembly full name is null"));
        _localizer = factory.Create(type.Name, assemblyName.Name
                                               ?? throw new InvalidOperationException("Assembly name is null"));
    }

    private readonly HashSet<Notification> _notifications = [];

    public IImmutableSet<Notification> Notifications => _notifications.ToImmutableHashSet();
    public bool HasNotifications => _notifications.Count != 0;

    public void AddNotification(Notification notification) =>
        _notifications.Add(notification);

    public void AddNotification(string key, params object[] valueParams)
    {
        var localized = _localizer[key, valueParams];
        var notification = new Notification(key, localized);
        AddNotification(notification);
    }
}
