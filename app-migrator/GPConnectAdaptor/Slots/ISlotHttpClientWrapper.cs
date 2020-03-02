using System;
using System.Threading.Tasks;

namespace GPConnectAdaptor.Slots
{
    public interface ISlotHttpClientWrapper
    {
        Task<string> GetSlotsHttp(DateTime start, DateTime end, SourceTarget sourceTarget = SourceTarget.Target);
    }
}