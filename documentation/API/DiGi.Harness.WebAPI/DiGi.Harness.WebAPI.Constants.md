#### [DiGi\.Harness\.WebAPI](DiGi.Harness.WebAPI.Overview.md 'DiGi\.Harness\.WebAPI\.Overview')

## DiGi\.Harness\.WebAPI\.Constants Namespace
### Classes

<a name='DiGi.Harness.WebAPI.Constants.Default'></a>

## Default Class

Defaults of the harness web layer\.

```csharp
public static class Default
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Default
### Fields

<a name='DiGi.Harness.WebAPI.Constants.Default.HttpClientName'></a>

## Default\.HttpClientName Field

The name of the [System\.Net\.Http\.IHttpClientFactory](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.ihttpclientfactory 'System\.Net\.Http\.IHttpClientFactory') client used for model requests\.

```csharp
public const string HttpClientName = "DiGi.Harness";
```

#### Field Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.Harness.WebAPI.Constants.Default.KeepAliveSeconds'></a>

## Default\.KeepAliveSeconds Field

How often a comment line keeps an idle event stream open through proxies, in seconds\.

```csharp
public const int KeepAliveSeconds = 15;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Harness.WebAPI.Constants.Default.MaxMessageCharacters'></a>

## Default\.MaxMessageCharacters Field

The maximum length of one user message, in characters\.

```csharp
public const int MaxMessageCharacters = 8000;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Harness.WebAPI.Constants.Default.MaxSessionsPerUser'></a>

## Default\.MaxSessionsPerUser Field

How many chat sessions one user may hold\.

```csharp
public const int MaxSessionsPerUser = 5;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Harness.WebAPI.Constants.Default.MaxThinkingSummaryCharacters'></a>

## Default\.MaxThinkingSummaryCharacters Field

The maximum length of a thinking summary line, in characters\.

```csharp
public const int MaxThinkingSummaryCharacters = 200;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Harness.WebAPI.Constants.Default.SessionIdleTimeoutMinutes'></a>

## Default\.SessionIdleTimeoutMinutes Field

How long a chat session lives without a request, in minutes\.

```csharp
public const int SessionIdleTimeoutMinutes = 120;
```

#### Field Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')