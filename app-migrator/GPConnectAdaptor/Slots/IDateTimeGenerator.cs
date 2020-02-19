using System;

namespace GPConnectAdaptor.Slots
{
    public interface IDateTimeGenerator
    {
        string Generate(DateTime dateTime);
    }
}