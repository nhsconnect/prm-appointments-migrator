using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.Slot;
using Newtonsoft.Json;

namespace GPConnectAdaptor.Slots
{
    public class SlotClient : Slots.ISlotClient
    {
        private readonly ISlotHttpClientWrapper _clientWrapper;
        private readonly ISlotModelMapper _mapper;

        public SlotClient(ISlotHttpClientWrapper clientWrapper, ISlotModelMapper mapper)
        {
            _clientWrapper = clientWrapper;
            _mapper = mapper;
        } 
        public async Task<List<SlotModel>> GetSlots(DateTime start, DateTime end,
            SourceTarget sourceTarget = SourceTarget.Target)
        {
            var response = await _clientWrapper.GetSlotsHttp(start, end, sourceTarget);
            var mappedSlots = _mapper.Map(response);

            return mappedSlots;
        }
    }
}