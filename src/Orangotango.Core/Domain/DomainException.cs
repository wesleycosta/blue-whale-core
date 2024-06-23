using System;

namespace Orangotango.Core.Domain;

public sealed class DomainException(string message) : Exception(message)
{
}
