// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using Xunit;

namespace System.Runtime.Serialization.Formatters.Tests
{
    public class BinaryFormatterTests
    {
        [Fact]
        public void SerializationInfoAddGet()
        {
            var value = new Serializable();
            var si = new SerializationInfo(typeof(Serializable), FormatterConverter.Default);
            var sc = new StreamingContext();
            value.GetObjectData(si, sc);

            Assert.Equal(typeof(Serializable), si.ObjectType);
            Assert.Equal(typeof(Serializable).FullName, si.FullTypeName);
            Assert.Equal(typeof(Serializable).GetTypeInfo().Assembly.FullName, si.AssemblyName);

            Assert.Equal(15, si.MemberCount);

            Assert.Equal(true, si.GetBoolean("bool"));
            Assert.Equal("hello", si.GetString("string"));
            Assert.Equal('a', si.GetChar("char"));

            Assert.Equal(byte.MaxValue, si.GetByte("byte"));

            Assert.Equal(decimal.MaxValue, si.GetDecimal("decimal"));
            Assert.Equal(double.MaxValue, si.GetDouble("double"));
            Assert.Equal(short.MaxValue, si.GetInt16("short"));
            Assert.Equal(int.MaxValue, si.GetInt32("int"));
            Assert.Equal(long.MaxValue, si.GetInt64("long"));
            Assert.Equal(sbyte.MaxValue, si.GetSByte("sbyte"));
            Assert.Equal(float.MaxValue, si.GetSingle("float"));
            Assert.Equal(ushort.MaxValue, si.GetUInt16("ushort"));
            Assert.Equal(uint.MaxValue, si.GetUInt32("uint"));
            Assert.Equal(ulong.MaxValue, si.GetUInt64("ulong"));
            Assert.Equal(DateTime.MaxValue, si.GetDateTime("datetime"));
        }

        [Fact]
        public void SerializationInfoEnumerate()
        {
            var value = new Serializable();
            var si = new SerializationInfo(typeof(Serializable), FormatterConverter.Default);
            var sc = new StreamingContext();
            value.GetObjectData(si, sc);

            int items = 0;
            foreach (SerializationEntry entry in si)
            {
                items++;
                switch (entry.Name)
                {
                    case "int":
                        Assert.Equal(int.MaxValue, (int)entry.Value);
                        Assert.Equal(typeof(int), entry.ObjectType);
                        break;
                    case "string":
                        Assert.Equal("hello", (string)entry.Value);
                        Assert.Equal(typeof(string), entry.ObjectType);
                        break;
                    case "bool":
                        Assert.Equal(true, (bool)entry.Value);
                        Assert.Equal(typeof(bool), entry.ObjectType);
                        break;
                }
            }

            Assert.Equal(si.MemberCount, items);
        }

        [Fact]
        public void NegativeAddValueTwice()
        {
            var si = new SerializationInfo(typeof(Serializable), FormatterConverter.Default);
            Assert.Throws<SerializationException>(() =>
            {
                si.AddValue("bool", true);
                si.AddValue("bool", true);
            });

            try
            {
                si.AddValue("bool", false);
            }
            catch (Exception e)
            {
                Assert.Equal("Cannot add the same member twice to a SerializationInfo object.", e.Message);
            }
        }

        [Fact]
        public void NegativeValueNotFound()
        {
            var si = new SerializationInfo(typeof(Serializable), FormatterConverter.Default);
            Assert.Throws<SerializationException>(() =>
            {
                si.AddValue("a", 1);
                si.GetInt32("b");
            });

            si = new SerializationInfo(typeof(Serializable), FormatterConverter.Default);
            try
            {
                si.AddValue("a", 1);
                si.GetInt32("b");
            }
            catch (Exception e)
            {
                Assert.Equal("Member 'b' was not found.", e.Message);
            }
        }
    }

    [Serializable]
    internal class Serializable : ISerializable
    {
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("string", "hello");
            info.AddValue("bool", true);
            info.AddValue("char", 'a');

            info.AddValue("byte", byte.MaxValue);
            info.AddValue("decimal", decimal.MaxValue);
            info.AddValue("double", double.MaxValue);
            info.AddValue("short", short.MaxValue);
            info.AddValue("int", int.MaxValue);
            info.AddValue("long", long.MaxValue);
            info.AddValue("sbyte", sbyte.MaxValue);
            info.AddValue("float", float.MaxValue);
            info.AddValue("ushort", ushort.MaxValue);
            info.AddValue("uint", uint.MaxValue);
            info.AddValue("ulong", ulong.MaxValue);
            info.AddValue("datetime", DateTime.MaxValue);
        }
    }

    internal class FormatterConverter : IFormatterConverter
    {
        public static readonly FormatterConverter Default = new FormatterConverter();

        public object Convert(object value, TypeCode typeCode)
        {
            throw new NotImplementedException();
        }

        public object Convert(object value, Type type)
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(object value)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(object value)
        {
            throw new NotImplementedException();
        }

        public char ToChar(object value)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(object value)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(object value)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(object value)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(object value)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(object value)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(object value)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(object value)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(object value)
        {
            throw new NotImplementedException();
        }

        public string ToString(object value)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(object value)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(object value)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(object value)
        {
            throw new NotImplementedException();
        }
    }
}
