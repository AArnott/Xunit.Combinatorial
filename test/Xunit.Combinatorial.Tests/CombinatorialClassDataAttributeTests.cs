// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Collections;
using Xunit;

public class CombinatorialClassDataAttributeTests(ITestOutputHelper logger)
{
    private static readonly object?[] ExpectedItems =
    {
        new ValueSourceItem(1, "Foo"), new ValueSourceItem(2, "Bar"), new ValueSourceItem(3, "Baz"),
    };

    [Fact]
    public void Ctor_IncompatibleType_Throws()
    {
        Action ctor = () => new CombinatorialClassDataAttribute(typeof(object));
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(ctor);
        Assert.Equal($"The values source {typeof(object)} must be assignable to {typeof(IEnumerable)}), {typeof(TheoryData<>)} or {typeof(TheoryDataBase<,>)}.", exception.Message);
    }

    [Fact]
    public void Ctor_TheoryData_MissingClassDataArguments_Throws()
    {
        Action ctor = () => new CombinatorialClassDataAttribute(typeof(MyTheoryDataValuesSourceWithParameters));
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(ctor);
        logger.WriteLine(exception.Message);
        Assert.Equal(
            $"Failed to create an instance of {typeof(MyTheoryDataValuesSourceWithParameters)}. " +
            "Please make sure the type has a public constructor and the arguments match.",
            exception.Message);
    }

    [Fact]
    public void Ctor_TheoryData_WithArgument_ReturnsCorrectData()
    {
        var argumentValue = 10;
        var attribute =
            new CombinatorialClassDataAttribute(typeof(MyTheoryDataValuesSourceWithParameters), argumentValue);
        var values = attribute.GetValues(null!);
        IEnumerable expected = Enumerable.Range(0, argumentValue);
        Assert.Equal(values, expected);
    }

    [Fact]
    public void Ctor_TheoryData_SetsProperty()
    {
        var attribute = new CombinatorialClassDataAttribute(typeof(MyTheoryDataValuesSource));
        Assert.Equal(ExpectedItems, attribute.GetValues(null!));
    }

    [Fact]
    public void Ctor_IEnumerable_SetsProperty()
    {
        var attribute = new CombinatorialClassDataAttribute(typeof(MyEnumerableDataValuesSource));
        Assert.Equal(ExpectedItems, attribute.GetValues(null!));
    }

#if NETSTANDARD2_0_OR_GREATER
    [Fact]
    public void Generic_Ctor_TheoryData_SetsProperty()
    {
        var attribute = new CombinatorialClassDataAttribute<MyTheoryDataValuesSource>();
        Assert.Equal(expectedItems, attribute.GetValues(null!));
    }

    [Fact]
    public void Generic_Ctor_IEnumerable_SetsProperty()
    {
        var attribute = new CombinatorialClassDataAttribute<MyEnumerableDataValuesSource>();
        Assert.Equal(expectedItems, attribute.GetValues(null!));
    }
#endif

    private class MyTheoryDataValuesSource : TheoryData<ValueSourceItem>
    {
        public MyTheoryDataValuesSource()
        {
            foreach (ValueSourceItem? item in ExpectedItems.Cast<ValueSourceItem>())
            {
                this.Add(item);
            }
        }
    }

    private class MyTheoryDataValuesSourceWithParameters : TheoryData<int>
    {
        public MyTheoryDataValuesSourceWithParameters(int value)
        {
            for (int i = 0; i < value; i++)
            {
                this.Add(i);
            }
        }
    }

    private class MyEnumerableDataValuesSource : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            return ExpectedItems
                .Cast<ValueSourceItem?>()
                .Select(item => new object?[] { item })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    private class ValueSourceItem
    {
        public ValueSourceItem(int number, string text)
        {
            this.Number = number;
            this.Text = text;
        }

        public int Number { get; }

        public string Text { get; }

        public override bool Equals(object? obj)
        {
            // Implemented to simplify comparison in tests
            return obj is ValueSourceItem item &&
                   this.Number == item.Number &&
                   this.Text == item.Text;
        }

        public override int GetHashCode()
        {
            return this.Number.GetHashCode() ^ this.Text.GetHashCode();
        }
    }
}
