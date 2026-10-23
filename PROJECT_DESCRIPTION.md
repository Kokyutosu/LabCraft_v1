# LabCraft_v1 — описание проекта и структуры файлов

## 1. Общий обзор

Этот проект представляет собой .NET-приложение с несколькими способами запуска и отображения UI:

- MAUI-приложение (`LabCraft_v1`)
- Blazor Web-приложение (`LabCraft_v1.Web`)
- Blazor WebAssembly-клиент (`LabCraft_v1.Web.Client`)
- общий компонентный слой (`LabCraft_v1.Shared`)
- хост для .NET Aspire (`LabCraft_v1.AppHost`)
- базовые настройки сервисов и телеметрии (`LabCraft_v1.ServiceDefaults`)

Проект построен как шаблон для кроссплатформенного приложений с общим Blazor UI и адаптацией поведения под платформу или тип устройства.

## 2. Технологический стек

- .NET 10
- ASP.NET Core
- Blazor Server / Blazor WebAssembly
- .NET MAUI
- .NET Aspire
- OpenTelemetry
- Bootstrap CSS

## 3. Структура решения

```text
LabCraft_v1/
├── aspire.config.json
├── LabCraft_v1.slnx
├── LabCraft_v1.AppHost/
│   ├── AppHost.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── LabCraft_v1.AppHost.csproj
│   └── Properties/
│       └── launchSettings.json
├── LabCraft_v1.ServiceDefaults/
│   ├── Extensions.cs
│   └── LabCraft_v1.ServiceDefaults.csproj
└── LabCraft_v1/
	├── LabCraft_v1.Shared/
	│   ├── _Imports.razor
	│   ├── Routes.razor
	│   ├── LabCraft_v1.Shared.csproj
	│   ├── Layout/
	│   │   ├── MainLayout.razor
	│   │   ├── MainLayout.razor.css
	│   │   ├── NavMenu.razor
	│   │   └── NavMenu.razor.css
	│   ├── Pages/
	│   │   ├── Counter.razor
	│   │   ├── Home.razor
	│   │   ├── NotFound.razor
	│   │   └── Weather.razor
	│   ├── Services/
	│   │   └── IFormFactor.cs
	│   └── wwwroot/
	│       ├── app.css
	│       ├── favicon.png
	│       └── lib/
	│           └── bootstrap/
	├── LabCraft_v1.Web/
	│   ├── Program.cs
	│   ├── LabCraft_v1.Web.csproj
	│   ├── appsettings.json
	│   ├── appsettings.Development.json
	│   ├── Properties/
	│   │   └── launchSettings.json
	│   ├── Components/
	│   │   ├── _Imports.razor
	│   │   └── App.razor
	│   └── Services/
	│       └── FormFactor.cs
	├── LabCraft_v1.Web.Client/
	│   ├── Program.cs
	│   ├── _Imports.razor
	│   ├── LabCraft_v1.Web.Client.csproj
	│   ├── Services/
	│   │   └── FormFactor.cs
	│   └── wwwroot/
	│       ├── appsettings.json
	│       └── appsettings.Development.json
	└── LabCraft_v1/
		├── App.xaml
		├── App.xaml.cs
		├── MainPage.xaml
		├── MainPage.xaml.cs
		├── MauiProgram.cs
		├── LabCraft_v1.csproj
		├── Services/
		│   └── FormFactor.cs
		├── wwwroot/
		│   ├── app.css
		│   └── index.html
		├── Components/
		│   └── _Imports.razor
		├── Platforms/
		│   ├── Android/
		│   ├── iOS/
		│   ├── MacCatalyst/
		│   └── Windows/
		├── Properties/
		│   └── launchSettings.json
		├── Resources/
		│   ├── AppIcon/
		│   ├── Fonts/
		│   ├── Images/
		│   ├── Raw/
		│   └── Splash/
		└── ...
```

> В корне проекта также есть сгенерированные папки `bin` и `obj`, но они обычно не считаются исходным кодом и не описываются как часть архитектуры приложения.

## 4. Описание ключевых проектов

### 4.1 `LabCraft_v1.AppHost`

Это проект запуска приложения через .NET Aspire.

#### `AppHost.cs`
- Создаёт `DistributedApplication`.
- Добавляет в хост два проекта:
  - `labcraft-v1` — MAUI приложение
  - `labcraft-v1-web` — Blazor Web приложение
- Вызов `builder.Build().Run();` запускает окружение Aspire.

#### `LabCraft_v1.AppHost.csproj`
- Содержит `Aspire.AppHost.Sdk`.
- Ссылается на `LabCraft_v1.Web` и `LabCraft_v1` (MAUI).
- Настроен для работы с .NET Aspire и запуска приложений в едином окружении.

#### `aspire.config.json`
- Указывает путь к AppHost.
- Используется интеграцией Aspire для корректного запуска приложения.

### 4.2 `LabCraft_v1.ServiceDefaults`

Это общий проект настроек для сервисов.

#### `Extensions.cs`
- Добавляет стандартные сервисы для ASP.NET Core:
  - Service Discovery
  - health checks
  - OpenTelemetry telemetry
  - resilience HTTP-клиентов
- Включает метрики и трассировку через OpenTelemetry.
- Добавляет endpoint `/health` и `/alive` для диагностики.
- Метод `MapDefaultEndpoints()` — подключает стандартные health endpoints.

#### `LabCraft_v1.ServiceDefaults.csproj`
- Является shared project с .NET Aspire-библиотекой.
- Подключает пакеты OpenTelemetry и HttpResilience.

## 5. Общий UI слой: `LabCraft_v1.Shared`

Это проект, содержащий компоненты Blazor, которые используются одновременно в Web и MAUI.

### `LabCraft_v1.Shared.csproj`
- Сборка `Microsoft.NET.Sdk.Razor`
- Представляет собой библиотеку компонентов и shared UI.
- Используется и на стороне MAUI, и на стороне Web приложений.

### `_Imports.razor`
- Общие using-директивы для всех Razor-компонентов.
- Подключает Blazor-библиотеки и namespace проекта.

### `Routes.razor`
- Основной router приложения.
- Настраивает маршрутизацию через `Router`.
- Указывает `NotFoundPage` как `Pages.NotFound`.
- Устанавливает `MainLayout` как дефолтный layout.

### `Layout/MainLayout.razor`
- Основной шаблон страницы.
- Содержит боковое меню (`NavMenu`) и область контента `@Body`.
- Включает блок ошибок `blazor-error-ui`.

### `Layout/NavMenu.razor`
- Навигационное меню приложения.
- Содержит ссылки на страницы:
  - Home
  - Counter
  - Weather

### `Pages/Home.razor`
- Главная страница.
- Использует интерфейс `IFormFactor` для отображения текущей платформы/форм-фактора.
- Пример Injection: `@inject IFormFactor FormFactor`.

### `Pages/Counter.razor`
- Стандартный пример счётчика.
- Хранит состояние `currentCount`.
- При клике увеличивает счётчик.

### `Pages/Weather.razor`
- Пример страницы с асинхронной загрузкой данных.
- Генерирует фейковые погодные данные через `Task.Delay(500)`.
- Показывает таблицу с температурой и описанием.

### `Pages/NotFound.razor`
- Страница 404.
- Используется для несуществующих маршрутов.

### `Services/IFormFactor.cs`
- Контракт для платформенной абстракции.
- Определяет методы:
  - `GetFormFactor()`
  - `GetPlatform()`

### `wwwroot/app.css`
- Общий CSS-файл для shared UI.
- Содержит стили по умолчанию и оформление Blazor-страниц.

## 6. Web-сервер: `LabCraft_v1.Web`

Это серверный ASP.NET Core+Blazor проект, который запускает приложение в браузере через Blazor WebAssembly и Razor Components.

### `Program.cs`
- Создаёт `WebApplication`.
- Подключает `AddServiceDefaults()` из проекта `LabCraft_v1.ServiceDefaults`.
- Добавляет Razor-компоненты и `InteractiveWebAssemblyComponents`.
- Регистрирует `IFormFactor` через `FormFactor`.
- Настраивает pipeline:
  - `UseWebAssemblyDebugging()` в Development
  - `UseExceptionHandler()` в Production
  - `UseHsts()`
  - `UseStatusCodePagesWithReExecute()`
  - `UseHttpsRedirection()`
  - `UseAntiforgery()`
  - `MapStaticAssets()`
  - `MapRazorComponents<App>()`
- Подключает `LabCraft_v1.Shared` и `LabCraft_v1.Web.Client` в составе приложения.

### `Components/App.razor`
- HTML-обёртка веб-страницы.
- Подключает Bootstrap и общий CSS.
- Загружает Blazor JS и маршрутизатор `Routes`.
- Использует `@rendermode="InteractiveWebAssembly"`.

### `Components/_Imports.razor`
- Подключает namespaces Blazor и общий UI библиотеки.

### `Services/FormFactor.cs`
- Реализация `IFormFactor` для web-среды.
- Возвращает:
  - `GetFormFactor()` => `"Web"`
  - `GetPlatform()` => `Environment.OSVersion.ToString()`

## 7. WebAssembly-клиент: `LabCraft_v1.Web.Client`

Это клиентская часть, которая запускается в браузере.

### `Program.cs`
- Создаёт `WebAssemblyHostBuilder`.
- Регистрирует `IFormFactor` в DI.
- Запускает приложение через `builder.Build().RunAsync()`.

### `_Imports.razor`
- Подключает стандартные пространства имён Blazor WebAssembly.

### `Services/FormFactor.cs`
- Реализация `IFormFactor` для WebAssembly:
  - `GetFormFactor()` => `"WebAssembly"`
  - `GetPlatform()` => `Environment.OSVersion.ToString()`

### `wwwroot/appsettings.json`
- Настройки приложения для WebAssembly.

### `wwwroot/appsettings.Development.json`
- Переменные и параметры только для Development-среды.

## 8. MAUI-приложение: `LabCraft_v1`

Это мобильный/desktop-клиент на базе .NET MAUI, в котором UI работает через BlazorWebView.

### `MauiProgram.cs`
- Создаёт `MauiApp`.
- Настраивает MAUI-приложение через `.UseMauiApp<App>()`.
- Подключает шрифт `OpenSans-Regular`.
- Регистрирует `IFormFactor` в DI.
- Включает `AddMauiBlazorWebView()`.
- В режиме DEBUG включает developer tools.

### `App.xaml` / `App.xaml.cs`
- Основной класс приложения MAUI.
- Создаёт окно с `MainPage`.
- `CreateWindow()` возвращает `Window(new MainPage())`.

### `MainPage.xaml`
- Основная XAML-страница.
- Внутри находится `BlazorWebView`.
- `RootComponent` указывает на `shared:Routes`.
- HostPage = `wwwroot/index.html`.

### `MainPage.xaml.cs`
- Код-бихайндер для `MainPage`.

### `Services/FormFactor.cs`
- Реализация `IFormFactor` для MAUI:
  - `GetFormFactor()` => `DeviceInfo.Idiom.ToString()`
  - `GetPlatform()` => `DeviceInfo.Platform.ToString() + " - " + DeviceInfo.VersionString`

### `wwwroot/index.html`
- HTML-шаблон для BlazorWebView в MAUI.
- Подключает CSS и JS-файлы.
- Настраивает рендеринг компонентов через `HeadOutlet` и `Routes`.

### `Components/_Imports.razor`
- Namespace imports для MAUI-сценария с Blazor.

### `Platforms/`
- Специфичные настройки для платформ:
  - Android
  - iOS
  - MacCatalyst
  - Windows
- Содержат entry points, manifest, app declarations, и platform-specific bootstrap code.

### `Resources/`
- Иконки, шрифты, изображения, splash screen и raw assets.

## 9. Конструкции и паттерны проекта

### 9.1 Общая архитектура

Проект построен по модели:

- shared UI library — `LabCraft_v1.Shared`
- host-specific implementations — `Web`, `Web.Client`, `MAUI`
- common abstraction — `IFormFactor`

Это позволяет одним и тем же Blazor-компонентам работать на нескольких платформах, а логика платформы инкапсулирована в отдельных реализации.

### 9.2 Dependency Injection

Везде используется DI (`builder.Services.AddSingleton...`):

- Web: `IFormFactor -> FormFactor`
- Web.Client: `IFormFactor -> FormFactor`
- MAUI: `IFormFactor -> FormFactor`

### 9.3 Routing и навигация

- `Routes.razor` отвечает за маршрутизацию Blazor.
- `NavMenu.razor` добавляет меню ссылок.
- `NotFound.razor` обрабатывает отсутствующие маршруты.

### 9.4 Aspire / service defaults

- AppHost запускает несколько проектов в одном окружении.
- `ServiceDefaults` добавляет health checks, OpenTelemetry и HTTP resilience.
- Это типичный шаблон для микросервисной и распределённой архитектуры.

### 9.5 Кроссплатформенность

Поскольку проект содержит:

- MAUI для мобильных/desktop-платформ
- Blazor Web + WebAssembly для браузера

он демонстрирует возможность разработки общей UI-логики с разной точкой входа и разными платформенными реализациями.

## 10. Что сейчас находится в проекте по содержимому

Это не полноценное приложение с бизнес-логикой, а каркас/шаблон, созданный из типовых .NET-шаблонов. Сейчас в нём присутствуют:

- приветственная страница
- страница счётчика
- страница с примером погодных данных
- страница 404
- общий layout и навигация
- адаптация поведения для разных платформ через `IFormFactor`

## 11. Итог

Project `LabCraft_v1` — это демонстрационный/каркасный проект, который объединяет в себе:

- MAUI
- Blazor Web
- Blazor WebAssembly
- общий shared UI
- архитектуру на основе .NET Aspire
- базовую телеметрию и health checks

Он хорошо подходит как основу для дальнейшего расширения: добавления экранов, бизнес-логики, API, авторизации, базы данных, сервисов и компонентов UI.

## 12. Краткий список ключевых файлов

- `LabCraft_v1.slnx` — файл решения
- `aspire.config.json` — конфиг Aspire
- `LabCraft_v1.AppHost/AppHost.cs` — точка запуска среды
- `LabCraft_v1.ServiceDefaults/Extensions.cs` — стандартные сервисы и telemetry
- `LabCraft_v1/LabCraft_v1.Shared/Routes.razor` — маршрутизация shared UI
- `LabCraft_v1/LabCraft_v1.Shared/Layout/MainLayout.razor` — основной layout
- `LabCraft_v1/LabCraft_v1.Shared/Pages/Home.razor` — домашняя страница
- `LabCraft_v1/LabCraft_v1.Web/Program.cs` — запуск web-приложения
- `LabCraft_v1/LabCraft_v1.Web.Client/Program.cs` — запуск WASM-клиента
- `LabCraft_v1/LabCraft_v1/MauiProgram.cs` — запуск MAUI-проекта
- `LabCraft_v1/LabCraft_v1/MainPage.xaml` — MAUI host для BlazorWebView
- `LabCraft_v1/LabCraft_v1/Services/FormFactor.cs` — MAUI-реализация `IFormFactor`
- `LabCraft_v1/LabCraft_v1.Shared/Services/IFormFactor.cs` — общий контракт

Если хотите, я могу следующим сообщением сделать ещё 2 варианта этого документа:

1. более краткий и структурный (для быстрого чтения)
2. более подробный технический (с описанием всех классов и файлов по строкам)
