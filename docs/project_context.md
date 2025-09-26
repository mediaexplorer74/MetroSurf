# Project Overview
Восстанавливаем и стабилизируем сборку WinRT-совместимого решения MetroSurf (несколько проектов: MetroSurf, MetroLog, NotificationsExtensions и пр.), устраняя артефакты декомпиляции, битые пути и конфликты пакетов.

# Goals
- Устранить ошибки компиляции (CS2001, CS0168, CS7036 и др.).
- Нормализовать .csproj (пути, удаление несуществующих файлов, корректные PackageReference).
- Довести сборку Debug/x64 до успешного состояния.

# Current Progress
- Исправлены MetroLog.csproj (пути к Strings.cs, удалены атрибуты [DebuggerNonUserCode] в исходниках).
- Исправлен NotificationsExtensions.csproj (удалены ссылки на отсутствующие файлы, включая IToastAudio.cs).
- Удалены/исправлены пустые атрибуты [MethodImpl] в TileContentFactory.cs, заменены на [MethodImpl(MethodImplOptions.InternalCall)] для устранения CS7036.
- Сборка решения проходила до конфликтов пакетов NU1107.

# Pending Tasks
- Произвести обновление/фиксацию версий пакета System.Runtime.Serialization.Primitives (рекомендовано 4.3.0) в MetroSurf.csproj и MetroLab.Common.csproj.
- Пересобрать решение Debug/x64 и проверить отсутствие новых ошибок/предупреждений.