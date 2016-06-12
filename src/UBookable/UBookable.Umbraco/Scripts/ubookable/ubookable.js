var uBookable = (function ($) {

    function _saveBooking(nodeid, bookerid, startDate, endDate) {
        console.log("save booking");
        var testBooking = Booking(nodeid, bookerid, startDate, endDate);
        var testBooker = Booker('Lachlann')
         return $.ajax({
            url: '/umbraco/api/booking/addbooking',
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(testBooking),
            contentType: 'application/json; charset=utf-8'
        });
    }

    function _getTimeSlots(id, startdate, enddate, period, duration)
    {
        var params = {};
        params["start"] = startdate;
        params["end"] = enddate;
        params["Id"] = id;
        params["period"] = period;
        params["duration"] = duration;

        return $.getJSON('/umbraco/api/booking/getdaybookingslots', params)

    }

    function _saveBooker(name, memberid) {
        var memId = (memberid !== undefined) ? memberid : '';
        var testBooker = Booker(name, memId);
        console.log(testBooker);
        return $.ajax({
            url: '/umbraco/api/booking/addbooker',
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(testBooker),
            contentType: 'application/json; charset=utf-8'
        });
    }

    return {
        SaveBooking: _saveBooking,
        SaveBooker: _saveBooker,
        GetTimeSlot: _getTimeSlots
    };
})(jQuery);

var Booking = function (nodeId, bookerID, startDate, endDate, approved, cancelled) {
    //Model for a user submitted answer
    return {
        'nodeId': nodeId,
        'bookerID': bookerID,
        'startDate': startDate,
        'endDate': endDate,
        'approved': approved,
        'cancelled': cancelled,
    };
};
var Booker = function (Name, memberid) {
    //Model for a user submitted answer
    
    return {
        'name': Name,
        'memberId': memberid
    };
};