using System;

public static class Converter
{
    public static TEnum ToEnum<TEnum> (string value)
    {
        return (TEnum) Enum.Parse(typeof(TEnum), value);
    } 

    public static TEnum ToEnum<TEnum> (int value)
    {
        return (TEnum) Enum.ToObject(typeof(TEnum), value);
    } 
}
