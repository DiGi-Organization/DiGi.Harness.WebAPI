#### [DiGi\.Harness\.WebAPI](DiGi.Harness.WebAPI.Overview.md 'DiGi\.Harness\.WebAPI\.Overview')

## DiGi\.Harness\.WebAPI Namespace
### Classes

<a name='DiGi.Harness.WebAPI.Modify'></a>

## Modify Class

```csharp
public static class Modify
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Modify
### Methods

<a name='DiGi.Harness.WebAPI.Modify.Add(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,DiGi.Harness.WebAPI.Classes.ChatSession)'></a>

## Modify\.Add\(this ChatSessionStore, ChatSession\) Method

Adds a session to the store; when its owner then holds more than [MaxSessionsPerUser](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSessionStore.MaxSessionsPerUser 'DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore\.MaxSessionsPerUser') sessions, removes that owner's least recently used ones\.

```csharp
public static bool Add(this DiGi.Harness.WebAPI.Classes.ChatSessionStore? chatSessionStore, DiGi.Harness.WebAPI.Classes.ChatSession? chatSession);
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Modify.Add(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,DiGi.Harness.WebAPI.Classes.ChatSession).chatSessionStore'></a>

`chatSessionStore` [ChatSessionStore](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSessionStore 'DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore')

The store\.

<a name='DiGi.Harness.WebAPI.Modify.Add(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,DiGi.Harness.WebAPI.Classes.ChatSession).chatSession'></a>

`chatSession` [ChatSession](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSession 'DiGi\.Harness\.WebAPI\.Classes\.ChatSession')

The session to add\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True when the session was added\.

<a name='DiGi.Harness.WebAPI.Modify.InitializeAsync(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection)'></a>

## Modify\.InitializeAsync\(this IServiceCollection\) Method

Registers the harness services: the [DiGi\.Harness\.Classes\.HarnessConfiguration](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.harnessconfiguration 'DiGi\.Harness\.Classes\.HarnessConfiguration') loaded from `Harness.json`, the in\-memory [ChatSessionStore](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSessionStore 'DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore'), [System\.TimeProvider\.System](https://learn.microsoft.com/en-us/dotnet/api/system.timeprovider.system 'System\.TimeProvider\.System') when none is registered, and the named HTTP client used for model requests \(its timeout from the configuration\)\.

The host also registers its [IHarnessHost](DiGi.Harness.WebAPI.Interfaces.md#DiGi.Harness.WebAPI.Interfaces.IHarnessHost 'DiGi\.Harness\.WebAPI\.Interfaces\.IHarnessHost'); without one the chat controller cannot be activated. Found by reflection by DiGi.WebAPI.WindowsService (a type named `Modify` with a public static `InitializeAsync(IServiceCollection)`).

```csharp
public static System.Threading.Tasks.Task InitializeAsync(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection);
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Modify.InitializeAsync(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection).serviceCollection'></a>

`serviceCollection` [Microsoft\.Extensions\.DependencyInjection\.IServiceCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection 'Microsoft\.Extensions\.DependencyInjection\.IServiceCollection')

The service collection of the host\.

#### Returns
[System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task')  
A task that completes when the services are registered\.

<a name='DiGi.Harness.WebAPI.Modify.Remove(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,string,string)'></a>

## Modify\.Remove\(this ChatSessionStore, string, string\) Method

Removes a session owned by a user\.

```csharp
public static bool Remove(this DiGi.Harness.WebAPI.Classes.ChatSessionStore? chatSessionStore, string? id, string? ownerId);
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Modify.Remove(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,string,string).chatSessionStore'></a>

`chatSessionStore` [ChatSessionStore](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSessionStore 'DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore')

The store\.

<a name='DiGi.Harness.WebAPI.Modify.Remove(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,string,string).id'></a>

`id` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The session id\.

<a name='DiGi.Harness.WebAPI.Modify.Remove(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,string,string).ownerId'></a>

`ownerId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The id of the requesting user; a session of another user is not removed\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
True when the session was removed\.

<a name='DiGi.Harness.WebAPI.Modify.RemoveExpired(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,System.DateTimeOffset)'></a>

## Modify\.RemoveExpired\(this ChatSessionStore, DateTimeOffset\) Method

Removes every session idle for longer than [IdleTimeout](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSessionStore.IdleTimeout 'DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore\.IdleTimeout'), except one whose turn is still running\.

```csharp
public static int RemoveExpired(this DiGi.Harness.WebAPI.Classes.ChatSessionStore? chatSessionStore, System.DateTimeOffset now);
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Modify.RemoveExpired(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,System.DateTimeOffset).chatSessionStore'></a>

`chatSessionStore` [ChatSessionStore](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSessionStore 'DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore')

The store\.

<a name='DiGi.Harness.WebAPI.Modify.RemoveExpired(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,System.DateTimeOffset).now'></a>

`now` [System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')

The current time\.

#### Returns
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')  
The number of sessions removed\.

<a name='DiGi.Harness.WebAPI.Modify.WriteServerSentEventAsync(thisMicrosoft.AspNetCore.Http.HttpResponse,string,System.Threading.CancellationToken)'></a>

## Modify\.WriteServerSentEventAsync\(this HttpResponse, string, CancellationToken\) Method

Writes one server\-sent event to a response and flushes it, so the client receives it at once\.

A [data](DiGi.Harness.WebAPI.md#DiGi.Harness.WebAPI.Modify.WriteServerSentEventAsync(thisMicrosoft.AspNetCore.Http.HttpResponse,string,System.Threading.CancellationToken).data 'DiGi\.Harness\.WebAPI\.Modify\.WriteServerSentEventAsync\(this Microsoft\.AspNetCore\.Http\.HttpResponse, string, System\.Threading\.CancellationToken\)\.data') of null writes a comment line (`: keep-alive`) that keeps an idle stream open through proxies. Data must be a single line (compact JSON).

```csharp
public static System.Threading.Tasks.Task WriteServerSentEventAsync(this Microsoft.AspNetCore.Http.HttpResponse httpResponse, string? data, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Modify.WriteServerSentEventAsync(thisMicrosoft.AspNetCore.Http.HttpResponse,string,System.Threading.CancellationToken).httpResponse'></a>

`httpResponse` [Microsoft\.AspNetCore\.Http\.HttpResponse](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpresponse 'Microsoft\.AspNetCore\.Http\.HttpResponse')

The response, already started with `text/event-stream`\.

<a name='DiGi.Harness.WebAPI.Modify.WriteServerSentEventAsync(thisMicrosoft.AspNetCore.Http.HttpResponse,string,System.Threading.CancellationToken).data'></a>

`data` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The event data, or null for a keep\-alive comment\.

<a name='DiGi.Harness.WebAPI.Modify.WriteServerSentEventAsync(thisMicrosoft.AspNetCore.Http.HttpResponse,string,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The token that cancels the write\.

#### Returns
[System\.Threading\.Tasks\.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task 'System\.Threading\.Tasks\.Task')  
A task that completes when the event is flushed\.

<a name='DiGi.Harness.WebAPI.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

<a name='DiGi.Harness.WebAPI.Query.ChatSession(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,string,string,System.DateTimeOffset)'></a>

## Query\.ChatSession\(this ChatSessionStore, string, string, DateTimeOffset\) Method

Finds a session owned by a user that has not expired\.

A session of another user answers like a missing one, so a client cannot tell foreign session ids from unknown ones.

```csharp
public static DiGi.Harness.WebAPI.Classes.ChatSession? ChatSession(this DiGi.Harness.WebAPI.Classes.ChatSessionStore? chatSessionStore, string? id, string? ownerId, System.DateTimeOffset now);
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Query.ChatSession(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,string,string,System.DateTimeOffset).chatSessionStore'></a>

`chatSessionStore` [ChatSessionStore](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSessionStore 'DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore')

The store\.

<a name='DiGi.Harness.WebAPI.Query.ChatSession(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,string,string,System.DateTimeOffset).id'></a>

`id` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The session id\.

<a name='DiGi.Harness.WebAPI.Query.ChatSession(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,string,string,System.DateTimeOffset).ownerId'></a>

`ownerId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The id of the requesting user\.

<a name='DiGi.Harness.WebAPI.Query.ChatSession(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,string,string,System.DateTimeOffset).now'></a>

`now` [System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')

The current time, compared with [LastActivity](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSession.LastActivity 'DiGi\.Harness\.WebAPI\.Classes\.ChatSession\.LastActivity') plus [IdleTimeout](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSessionStore.IdleTimeout 'DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore\.IdleTimeout')\.

#### Returns
[ChatSession](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSession 'DiGi\.Harness\.WebAPI\.Classes\.ChatSession')  
The session, or null when it is missing, foreign or expired\.

<a name='DiGi.Harness.WebAPI.Query.Transcript(thisDiGi.Harness.Classes.Conversation,DiGi.Harness.Enums.ThinkingVisibility)'></a>

## Query\.Transcript\(this Conversation, ThinkingVisibility\) Method

Builds the copy of a conversation sent to a client that reloads a session\.

Thinking follows the client's visibility: full text for [DiGi\.Harness\.Enums\.ThinkingVisibility\.Full](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.enums.thinkingvisibility.full 'DiGi\.Harness\.Enums\.ThinkingVisibility\.Full'), one summary line for [DiGi\.Harness\.Enums\.ThinkingVisibility\.Summary](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.enums.thinkingvisibility.summary 'DiGi\.Harness\.Enums\.ThinkingVisibility\.Summary') (and [DiGi\.Harness\.Enums\.ThinkingVisibility\.Undefined](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.enums.thinkingvisibility.undefined 'DiGi\.Harness\.Enums\.ThinkingVisibility\.Undefined')), nothing for [DiGi\.Harness\.Enums\.ThinkingVisibility\.None](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.enums.thinkingvisibility.none 'DiGi\.Harness\.Enums\.ThinkingVisibility\.None'). Signatures and redacted thinking data never leave the server. Text, tool calls and tool results are copied.

```csharp
public static DiGi.Harness.Classes.Conversation? Transcript(this DiGi.Harness.Classes.Conversation? conversation, DiGi.Harness.Enums.ThinkingVisibility thinkingVisibility);
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Query.Transcript(thisDiGi.Harness.Classes.Conversation,DiGi.Harness.Enums.ThinkingVisibility).conversation'></a>

`conversation` [DiGi\.Harness\.Classes\.Conversation](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.conversation 'DiGi\.Harness\.Classes\.Conversation')

The conversation\.

<a name='DiGi.Harness.WebAPI.Query.Transcript(thisDiGi.Harness.Classes.Conversation,DiGi.Harness.Enums.ThinkingVisibility).thinkingVisibility'></a>

`thinkingVisibility` [DiGi\.Harness\.Enums\.ThinkingVisibility](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.enums.thinkingvisibility 'DiGi\.Harness\.Enums\.ThinkingVisibility')

The client's thinking visibility\.

#### Returns
[DiGi\.Harness\.Classes\.Conversation](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.conversation 'DiGi\.Harness\.Classes\.Conversation')  
The filtered copy, or null when [conversation](DiGi.Harness.WebAPI.md#DiGi.Harness.WebAPI.Query.Transcript(thisDiGi.Harness.Classes.Conversation,DiGi.Harness.Enums.ThinkingVisibility).conversation 'DiGi\.Harness\.WebAPI\.Query\.Transcript\(this DiGi\.Harness\.Classes\.Conversation, DiGi\.Harness\.Enums\.ThinkingVisibility\)\.conversation') is null\.

<a name='DiGi.Harness.WebAPI.Query.VisibleHarnessEvent(thisDiGi.Harness.Classes.HarnessEvent,DiGi.Harness.Enums.ThinkingVisibility)'></a>

## Query\.VisibleHarnessEvent\(this HarnessEvent, ThinkingVisibility\) Method

Filters one turn event by the client's thinking visibility, before it leaves the server\.

[DiGi\.Harness\.Enums\.ThinkingVisibility\.Full](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.enums.thinkingvisibility.full 'DiGi\.Harness\.Enums\.ThinkingVisibility\.Full') passes thinking deltas and drops the completed-block event (the client already has the text). [DiGi\.Harness\.Enums\.ThinkingVisibility\.Summary](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.enums.thinkingvisibility.summary 'DiGi\.Harness\.Enums\.ThinkingVisibility\.Summary') (also for [DiGi\.Harness\.Enums\.ThinkingVisibility\.Undefined](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.enums.thinkingvisibility.undefined 'DiGi\.Harness\.Enums\.ThinkingVisibility\.Undefined')) drops the deltas and turns each completed block into one [DiGi\.Harness\.Classes\.ThinkingSummaryEvent](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.thinkingsummaryevent 'DiGi\.Harness\.Classes\.ThinkingSummaryEvent') line. [DiGi\.Harness\.Enums\.ThinkingVisibility\.None](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.enums.thinkingvisibility.none 'DiGi\.Harness\.Enums\.ThinkingVisibility\.None') drops all thinking. Every other event passes unchanged.

```csharp
public static DiGi.Harness.Classes.HarnessEvent? VisibleHarnessEvent(this DiGi.Harness.Classes.HarnessEvent? harnessEvent, DiGi.Harness.Enums.ThinkingVisibility thinkingVisibility);
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Query.VisibleHarnessEvent(thisDiGi.Harness.Classes.HarnessEvent,DiGi.Harness.Enums.ThinkingVisibility).harnessEvent'></a>

`harnessEvent` [DiGi\.Harness\.Classes\.HarnessEvent](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.harnessevent 'DiGi\.Harness\.Classes\.HarnessEvent')

The event\.

<a name='DiGi.Harness.WebAPI.Query.VisibleHarnessEvent(thisDiGi.Harness.Classes.HarnessEvent,DiGi.Harness.Enums.ThinkingVisibility).thinkingVisibility'></a>

`thinkingVisibility` [DiGi\.Harness\.Enums\.ThinkingVisibility](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.enums.thinkingvisibility 'DiGi\.Harness\.Enums\.ThinkingVisibility')

The client's thinking visibility\.

#### Returns
[DiGi\.Harness\.Classes\.HarnessEvent](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.harnessevent 'DiGi\.Harness\.Classes\.HarnessEvent')  
The event to send, or null to send nothing\.