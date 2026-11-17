#### [DiGi\.Harness\.WebAPI](DiGi.Harness.WebAPI.Overview.md 'DiGi\.Harness\.WebAPI\.Overview')

## DiGi\.Harness\.WebAPI\.Interfaces Namespace
### Interfaces

<a name='DiGi.Harness.WebAPI.Interfaces.IHarnessHost'></a>

## IHarnessHost Interface

What a web host provides to the harness: who the user is, which tools that user gets, and the system prompt\.

Everything project-specific lives behind this interface, in the host (for example `GISHarnessHost` in DiGi.GIS.WebAPI.UI); DiGi.Harness.WebAPI holds none of it. The host registers its implementation as a singleton after calling [InitializeAsync\(this IServiceCollection\)](DiGi.Harness.WebAPI.md#DiGi.Harness.WebAPI.Modify.InitializeAsync(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection) 'DiGi\.Harness\.WebAPI\.Modify\.InitializeAsync\(this Microsoft\.Extensions\.DependencyInjection\.IServiceCollection\)').

```csharp
public interface IHarnessHost : DiGi.Harness.WebAPI.Interfaces.IHarnessWebAPIObject, DiGi.Core.Interfaces.IObject
```

Implements [IHarnessWebAPIObject](DiGi.Harness.WebAPI.Interfaces.md#DiGi.Harness.WebAPI.Interfaces.IHarnessWebAPIObject 'DiGi\.Harness\.WebAPI\.Interfaces\.IHarnessWebAPIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Methods

<a name='DiGi.Harness.WebAPI.Interfaces.IHarnessHost.SystemPromptAsync(System.Threading.CancellationToken)'></a>

## IHarnessHost\.SystemPromptAsync\(CancellationToken\) Method

Builds the system prompt sent with every request of a turn\.

Keep it stable between turns: it is the cached prefix of every Anthropic request.

```csharp
System.Threading.Tasks.Task<string?> SystemPromptAsync(System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Interfaces.IHarnessHost.SystemPromptAsync(System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The token that cancels the build\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
The system prompt, or null for none\.

<a name='DiGi.Harness.WebAPI.Interfaces.IHarnessHost.ToolsAsync(Microsoft.AspNetCore.Http.HttpContext,DiGi.Harness.WebAPI.Classes.HarnessUser,System.Threading.CancellationToken)'></a>

## IHarnessHost\.ToolsAsync\(HttpContext, HarnessUser, CancellationToken\) Method

Creates the tools offered to the model for one turn of a user's request, bound to that user's credentials\.

```csharp
System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyList<DiGi.Harness.Interfaces.ITool>> ToolsAsync(Microsoft.AspNetCore.Http.HttpContext httpContext, DiGi.Harness.WebAPI.Classes.HarnessUser harnessUser, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Interfaces.IHarnessHost.ToolsAsync(Microsoft.AspNetCore.Http.HttpContext,DiGi.Harness.WebAPI.Classes.HarnessUser,System.Threading.CancellationToken).httpContext'></a>

`httpContext` [Microsoft\.AspNetCore\.Http\.HttpContext](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext 'Microsoft\.AspNetCore\.Http\.HttpContext')

The request\.

<a name='DiGi.Harness.WebAPI.Interfaces.IHarnessHost.ToolsAsync(Microsoft.AspNetCore.Http.HttpContext,DiGi.Harness.WebAPI.Classes.HarnessUser,System.Threading.CancellationToken).harnessUser'></a>

`harnessUser` [HarnessUser](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.HarnessUser 'DiGi\.Harness\.WebAPI\.Classes\.HarnessUser')

The signed\-in user\.

<a name='DiGi.Harness.WebAPI.Interfaces.IHarnessHost.ToolsAsync(Microsoft.AspNetCore.Http.HttpContext,DiGi.Harness.WebAPI.Classes.HarnessUser,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The token that cancels the setup\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.IReadOnlyList&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')[DiGi\.Harness\.Interfaces\.ITool](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.interfaces.itool 'DiGi\.Harness\.Interfaces\.ITool')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1 'System\.Collections\.Generic\.IReadOnlyList\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
The tools; empty for a plain chat\.

<a name='DiGi.Harness.WebAPI.Interfaces.IHarnessHost.UserAsync(Microsoft.AspNetCore.Http.HttpContext,System.Threading.CancellationToken)'></a>

## IHarnessHost\.UserAsync\(HttpContext, CancellationToken\) Method

Resolves the signed\-in user of a request\.

```csharp
System.Threading.Tasks.Task<DiGi.Harness.WebAPI.Classes.HarnessUser?> UserAsync(Microsoft.AspNetCore.Http.HttpContext httpContext, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Interfaces.IHarnessHost.UserAsync(Microsoft.AspNetCore.Http.HttpContext,System.Threading.CancellationToken).httpContext'></a>

`httpContext` [Microsoft\.AspNetCore\.Http\.HttpContext](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext 'Microsoft\.AspNetCore\.Http\.HttpContext')

The request\.

<a name='DiGi.Harness.WebAPI.Interfaces.IHarnessHost.UserAsync(Microsoft.AspNetCore.Http.HttpContext,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The token that cancels the lookup\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[HarnessUser](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.HarnessUser 'DiGi\.Harness\.WebAPI\.Classes\.HarnessUser')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
The user, or null when the request is not signed in; the chat endpoints then answer 401\.

<a name='DiGi.Harness.WebAPI.Interfaces.IHarnessWebAPIObject'></a>

## IHarnessWebAPIObject Interface

Marker interface for all objects belonging to the DiGi\.Harness\.WebAPI project\.

```csharp
public interface IHarnessWebAPIObject : DiGi.Core.Interfaces.IObject
```

Derived  
↳ [ChatSession](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSession 'DiGi\.Harness\.WebAPI\.Classes\.ChatSession')  
↳ [ChatSessionStore](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSessionStore 'DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore')  
↳ [HarnessUser](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.HarnessUser 'DiGi\.Harness\.WebAPI\.Classes\.HarnessUser')  
↳ [IHarnessHost](DiGi.Harness.WebAPI.Interfaces.md#DiGi.Harness.WebAPI.Interfaces.IHarnessHost 'DiGi\.Harness\.WebAPI\.Interfaces\.IHarnessHost')

Implements [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')