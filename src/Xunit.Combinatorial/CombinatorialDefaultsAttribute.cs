namespace Xunit
{
    using System;

    /// <summary>
    /// Used to indicate the static method in a test class to call to initialise default combinatorial values for particular types
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class CombinatorialDefaultsAttribute : Attribute
    {
    }
}
