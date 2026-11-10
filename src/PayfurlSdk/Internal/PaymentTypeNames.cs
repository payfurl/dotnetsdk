using System.Collections.Generic;
using System.Linq;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Internal;

/// <summary>Payment links' allowed payment types: the browser SDK's names, and <see cref="PaymentType"/>.</summary>
internal static class PaymentTypeNames
{
    public static List<PaymentType>? ToTypes(List<string>? names) =>
        names?.Select(EnumValues<PaymentType>.Parse).ToList();

    public static List<string>? ToNames(List<PaymentType>? types) =>
        types?.Select(EnumValues<PaymentType>.Name).ToList();
}
