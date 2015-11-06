    $(document).ready(function () {
        $('#calendar').fullCalendar
            ({

                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,basicWeek,basicDay'
                },
                selectable: true,
                eventLimit: true,
                allDayDefault: false,
                timezone: 'local'
            })
    });