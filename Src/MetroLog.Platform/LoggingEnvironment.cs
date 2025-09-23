// Decompiled with JetBrains decompiler
// Type: MetroLog.LoggingEnvironment
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using MetroLog.Internal;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;

#nullable disable
namespace MetroLog
{
  public class LoggingEnvironment : LoggingEnvironmentBase
  {
    private static XamlApplicationState _xamlApplicationState;

    public string PackageArchitecture { get; private set; }

    public string PackageFullName { get; private set; }

    public string PackagePublisher { get; private set; }

    public string PackagePublisherId { get; private set; }

    public string PackageResourceId { get; private set; }

    public string PackageVersion { get; private set; }

    public string InstallationId { get; private set; }

    public LoggingEnvironment()
      : base("Windows Phone App 8.1")
    {
      PackageId id = Package.Current.Id;
      this.PackageArchitecture = id.Architecture.ToString();
      this.PackageFullName = id.FullName;
      this.PackagePublisher = id.Publisher;
      this.PackagePublisherId = id.PublisherId;
      this.PackageResourceId = id.ResourceId;
      this.PackageVersion = string.Format("{0}.{1}.{2}.{3}", (object) id.Version.Major, (object) id.Version.Minor, (object) id.Version.Build, (object) id.Version.Revision);
      ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
      if (!localSettings.Containers.ContainsKey("MetroLog"))
        localSettings.CreateContainer("MetroLog", (ApplicationDataCreateDisposition) 0);
      this.InstallationId = (string) ((IDictionary<string, object>) localSettings.Values)[nameof (InstallationId)];
      if (!string.IsNullOrEmpty(this.InstallationId))
        return;
      this.InstallationId = Guid.NewGuid().ToString();
      ((IDictionary<string, object>) localSettings.Values)[nameof (InstallationId)] = (object) this.InstallationId;
    }

    internal static XamlApplicationState XamlApplicationState
    {
      get
      {
        if (LoggingEnvironment._xamlApplicationState == XamlApplicationState.Unknown)
        {
          if (DesignMode.DesignModeEnabled)
          {
            LoggingEnvironment._xamlApplicationState = XamlApplicationState.Unavailable;
          }
          else
          {
            try
            {
              LoggingEnvironment._xamlApplicationState = Application.Current == null ? XamlApplicationState.Unavailable : XamlApplicationState.Available;
            }
            catch
            {
              LoggingEnvironment._xamlApplicationState = XamlApplicationState.Unavailable;
            }
          }
        }
        return LoggingEnvironment._xamlApplicationState;
      }
    }
  }
}
