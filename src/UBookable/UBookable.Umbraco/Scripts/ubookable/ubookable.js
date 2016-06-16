﻿var uBookable = (function ($) {
    function _saveBookingAndBooker(nodeid, startDate, endDate, bookerName, autoApprove) {
        var approve = (autoApprove === undefined) ? false : autoApprove;
        console.log('save booking and booker: ')
        var testBooking = Booking(nodeid, '', startDate, endDate, approve)
        var testBooker = Booker(bookerName)
        var data = {}
        data['booker'] = testBooker
        data['booking'] = testBooking
        return $.ajax({
            url: '/umbraco/api/booking/addbookingandbooker',
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8'
        })
    }

    function _saveBooking(nodeid, bookerid, startDate, endDate) {
        var testBooking = Booking(nodeid, bookerid, startDate, endDate)
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

    function _saveBooker(name, memberid) {
        var memId = (memberid !== undefined) ? memberid : ''
        var testBooker = Booker(name, memId)
        console.log(testBooker)
        return $.ajax({
            url: '/umbraco/api/booking/addbooker',
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(testBooker),
            contentType: 'application/json; charset=utf-8'
        })
    }

    return {
        SaveBooking: _saveBooking,
        SaveBooker: _saveBooker,
        SaveBookingAndBooker: _saveBookingAndBooker,
        GetTimeSlot: _getTimeSlots
    }
})(jQuery)

var Booking = function (nodeId, bookerID, startDate, endDate, approved, cancelled) {
    return {
        'nodeId': nodeId,
        'bookerID': bookerID,
        'startDate': startDate,
        'endDate': endDate,
        'approved': approved,
        'cancelled': cancelled
    }
}
var Booker = function (Name, memberid) {
    return {
        'name': Name,
        'memberId': memberid
    }
}
