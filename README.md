# Instasharp
Instasharp is an open-source web-scraping API for .NET — used to obtain a wide range of metadata from Instagram profiles.

The scraper works regardless of authentication, therefore, a username or password is **not required.**

## Features
Instasharp can:
- Acquire metadata from a user's profile
- Download a user's profile picture in high-definition
- Work without authentication

## Usage
### Runtime
Currently, Instasharp targets **.NET Core 5.0**, however, implementation for earlier versions, as well as **.NET Framework** is planned.

### Dependencies
- [Json.NET](https://github.com/JamesNK/Newtonsoft.Json)

### Examples
#### Obtaining a specific profile's metadata
Here is a short example of how to obtain metadata from an Instagram user's profile:

```C#
using Instasharp;

// SessionID is an optional parameter that may be required
// to ensure Instagram does not re-direct requests to the
// login page
var sessionId = "0123456789ABC&%!";
var client = new InstagramClient(sessionId);
var username = "hey_its_curtis";

// The URL of the user's profile may also be used here instead
// of the username
var profile = await client.GetProfileMetadataAsync(username);
```

#### Searching for profiles matching a specified username
```C#
using Instasharp;
using Instasharp.Search;

var sessionId = "0123456789ABC&%!";
var client = new InstagramClient(sessionId);
var username = "hey_its_curtis";

var results = await client.SearchForProfilesAsync(username).CollateAsync();
```
