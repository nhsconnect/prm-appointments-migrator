export const gpconnectTransformer = (payload) => {
    const { entry: appointments } = payload;
    const lift = appointments
        .map(appointment => appointment.resource)
        .sort((a, b) => Number(a.id) - Number(b.id));

    const flatten = lift.map(({ id, description, start, end, minutesDuration, slot, participant }) => {
        return [
            [participant[0].actor.reference],
            [slot[0].reference],
            [participant[1].actor.reference],
            [participant[2].actor.reference],
            [start, 'start'],
            [end, 'end'],
            [minutesDuration, 'duration'],
            [description, 'description']
        ];
    })
    return flatten;
};
