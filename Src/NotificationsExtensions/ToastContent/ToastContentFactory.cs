using System;

#nullable disable
namespace NotificationsExtensions.ToastContent
{
    internal static class ToastContentFactory
    {
        internal static IToastImageAndText01 CreateToastImageAndText01() => new ToastImageAndText01();
        internal static IToastImageAndText02 CreateToastImageAndText02() => new ToastImageAndText02();
        internal static IToastImageAndText03 CreateToastImageAndText03() => new ToastImageAndText03();
        internal static IToastImageAndText04 CreateToastImageAndText04() => new ToastImageAndText04();

        internal static IToastText01 CreateToastText01() => new ToastText01();
        internal static IToastText02 CreateToastText02() => new ToastText02();
        internal static IToastText03 CreateToastText03() => new ToastText03();
        internal static IToastText04 CreateToastText04() => new ToastText04();

        internal static string GetName() => "ToastContentFactory";
    }
}
