import jsonpath from 'jsonpath';
import React, { useEffect, useReducer } from 'react';
import { getSlots } from './service/slots';
import Cards from './cards';

export default () => {
    const filterSort = (items, query) => {
        return jsonpath.query(items, `$.entry[?(@.resource.resourceType == '${query}')].resource`)
            .sort((a, b) => Number(a.id) - Number(b.id));
    };

    const reduceSchedules = (acc, next) => {
        const filtered = filterSort(next, 'Schedule');
        return filtered.
            map(({ id, actor, comment }) => {
                return [
                    [id],
                    [actor[0].reference],
                    [actor[1].reference],
                    [comment, 'comment']
                ];
            });
    };

    const reduceSlots = (acc, next) => {
        const filtered = filterSort(next, 'Slot');
        return filtered.
            map(({ id, schedule, start, end }) => {
                return [
                    [id],
                    [schedule.reference],
                    [start, 'start'],
                    [end, 'end']
                ];
            });
    };

    const [schedules, setSchedules] = useReducer(reduceSchedules, []);
    const [slots, setSlots] = useReducer(reduceSlots, []);

    const getSlotService = async () => {
        const slots = await getSlots();
        setSchedules(slots);
        setSlots(slots);
    };

    useEffect(() => {
        getSlotService();
    }, []);

    return (
        <div>
            <h3>Schedules</h3>
            <Cards items={schedules} />
            <h3>Slots</h3>
            <Cards items={slots} />
        </div>
    );
};
