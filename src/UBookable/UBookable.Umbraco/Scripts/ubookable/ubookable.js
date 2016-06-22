var uBookable = (function ($) {

    //function _saveBookingAndBooker(nodeid, startDate, endDate, bookerName, autoApprove, memberId) {
    //    console.log("save booking");
    //    var approve = (autoApprove === undefined) ? false : autoApprove;
    //    console.log('save booking and booker: ')
    //    var testBooking = Booking(nodeid, '', startDate, endDate, approve)
    //    var testBooker = Booker(bookerName, memberId)
    //    console.log(testBooker);
        
    //    var data = {}
    //    data['booker'] = testBooker
    //    data['booking'] = testBooking
    //    return $.ajax({
    //        url: '/umbraco/api/booking/addbookingandbooker',
    //        type: 'POST',
    //        dataType: 'json',
    //        data: JSON.stringify(data),
    //        contentType: 'application/json; charset=utf-8'
    //    })
    //}

    function _saveBooking(nodeid, bookerid, startDate, endDate, autoApprove) {
        var approve = (autoApprove === undefined) ? false : autoApprove;
        var testBooking = Booking(nodeid, bookerid, startDate, endDate, approve)
        return $.ajax({
            url: '/umbraco/api/booking/addbooking',
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(testBooking),
            contentType: 'application/json; charset=utf-8'
        })
    }

    function _getTimeSlots(id, startdate, enddate, period, duration) {
        var params = {}
        params['start'] = startdate
        params['end'] = enddate
        params['Id'] = id
        params['period'] = period
        params['duration'] = duration

        return $.getJSON('/umbraco/api/booking/getdaybookingslots', params)
    }


    return {
        SaveBooking: _saveBooking,
        GetTimeSlot: _getTimeSlots
    }
})(jQuery)

var Booking = function (nodeId, bookerID, startDate, endDate, approved, cancelled) {
    return {
        'nodeId': nodeId,
        'bookerID': bookerID,
        'startDateISO': startDate.toISOString(),
        'endDateISO': endDate.toISOString(),
        'approved': approved,
        'cancelled': cancelled
    }
}

Date.prototype.toJSON = function () { return moment.utc(this).format(); }