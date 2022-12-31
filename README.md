# PayFURL .NET SDK

Library for integrating with PayFURL payments in your app. It includes the following APIs:

1. Charge API
2. Customer API
3. PaymentMethod API
4. Transfer API
5. Vault API
6. Token API

## 📄 Requirements

Use of the PayFURL .NET SDK requires:

* .NET Standard 2.0/2.1 or .NET Core 2.1 or .NET 4.5/4.8

## 🧰 Installation

To use the PayFURL .NET SDK in your project please do the following step:

```shell
dotnet add package PayfurlSdk --version <version>
```

or 

```shell
NuGet\Install-Package PayfurlSdk -Version <version>
```

## 👷 Usage

An example of creating a PayFURL client with **sandbox** environment.

```csharp
using payfurl.sdk;
using payfurl.sdk.Models;

class Example 
{
    // Initialize
    Config.Setup("PAYFURL_ACCESS_TOKEN", Environment.SANDBOX);
    
    var chargeData = new NewChargeToken
    {
        Amount = 20,
        Token = "5db53c06443c8f28c0cba6e5"
    };

    var svc = new payfurl.sdk.Charge();
    var result = svc.CreateWithToken(chargeData);
}
```

## 🔨 Tests

Clone the repo locally and `cd` into the directory.

```sh
git clone https://github.com/payfurl/dotnetsdk
```

Before running tests, go into `FunctionalTests` folder and open a file with tests that you want to run. Change the token in setup section for the corresponding API and set `Environment` to `Environment.SANDBOX`. `Charge.cs` example:

```csharp
public Charge()
{
    Config.Setup("<SPECIFY_TOKEN_HERE", Environment.SANDBOX);
}
```
