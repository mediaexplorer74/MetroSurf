# MetroSurf v xx.xx - main branch
![](/Images/logo.png)

MetroSurf WP8 edition v 1.0.1-alpha decomp. JetBrains DotPeek used.

## Screenshot
![](/Images/sshot01.png)

# Project Overview
Repair & stabilize MetroSurf build

# Current Progress
- Исправлены MetroLog.csproj (пути к Strings.cs, удалены атрибуты [DebuggerNonUserCode] в исходниках).
- Исправлен NotificationsExtensions.csproj (удалены ссылки на отсутствующие файлы, включая IToastAudio.cs).
- Удалены/исправлены пустые атрибуты [MethodImpl] в TileContentFactory.cs, заменены на [MethodImpl(MethodImplOptions.InternalCall)] для устранения CS7036.
- Сборка решения проходила до конфликтов пакетов NU1107.
- Solution prepared as UWP one but 100500 errors still here/there.
- Newest Visual Studio 2026 compatibility only (2022 -- idk... try to rename MetroSurf.slnx to MetroSurf.sln)
- WP8 - > UWP porting started with AI help (Copilot / ChatGPT5-mini in VS 2026 Insiders IDE)
- Min. Win. SDK used: 15063


# Goals
- Устранить "физические "ошибки компиляции - ok
- Устранить "логические "ошибки - failed (black screen & app crash after window resize)


## References / Links
- https://4pda.to/forum/index.php?showtopic=1111683 MetroSurf Версия: 1.01 (Rus.)
- https://www.reddit.com/r/windowsphone/comments/1nmxnj3/introducing_metrosurf_modern_web_browsing_on Introducing MetroSurf: Modern Web Browsing on Windows Phone

## .
- As is. No support. RnD only!

## ..

[m][e] 26 Sep 2025

![](/Images/footer.png)