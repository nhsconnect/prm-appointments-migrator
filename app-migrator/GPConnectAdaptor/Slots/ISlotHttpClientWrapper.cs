using System;
using System.Threading.Tasks;
using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public interface ISlotHttpClientWrapper
    {
        Task<string> GetAsync(DateTime start, DateTime end);
    }
}