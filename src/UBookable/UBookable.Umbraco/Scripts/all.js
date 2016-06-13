var DEBUG = true;
if (!DEBUG) {
    if (!window.console) window.console = {};
    var methods = ["log", "debug", "warn", "info"];
    for (var i = 0; i < methods.length; i++) {
        console[methods[i]] = function () { };
    }
}


  (function(yourcode) {
      yourcode(window.jQuery, window, document);

  }(function($, window, document) {

      $(function() {
          $('.full-clndr').clndr({
              render: function (data) {
                  return Mustache.render($('#calendar-tmpl').html(), data);
              },
              clickEvents: {
                  click: function (target) {
                      console.log(target);
                      var bookable = $(target.element).closest(".bookable");
                      var id = bookable.attr("data-id");
                      var startTime = $.parseJSON(bookable.attr("data-start-time"));
                      var endTime = $.parseJSON(bookable.attr("data-end-time"));
                      var period = bookable.attr("data-period");
                      var duration = bookable.attr("data-duration");

                      var startDateTimeUTC = target.date._d.setHours(startTime.hours, startTime.mins);
                      var endDateTimeUTC = target.date._d.setHours(endTime.hours, endTime.mins);



                      renderTimeslots(id, startDateTimeUTC, endDateTimeUTC, period, duration, target.date.format("dddd, MMMM Do YYYY"));

                  }
              }
          });


          $('.bookable').on('click', '.make-booking', function () {
              var me = $(this);
              var id = me.attr("data-id");
              var start = new Date(me.attr("data-start-time"));
              var end = new Date(me.attr("data-end-time"));
              var name = me.prev(".name").val();
              var period = me.attr("data-period");
              var duration = me.attr("data-duration");
              console.log("make booking: " + id + " " + start + " " + end + " " + name);

              if (name != "" && name != undefined) {
                  uBookable.SaveBookingAndBooker(id, start, end, name).done(function (booking) {
                      var bookable = me.closest(".bookable");
                      var slot = me.closest(".slot");
                      var period = bookable.attr("data-period");
                      var duration = bookable.attr("data-duration");
                      var dayDate = moment(start).format("dddd, MMMM Do YYYY");

                      var daystart = slot.attr("data-day-start");
                      var dayend = slot.attr("data-day-end");

                      renderTimeslots(id, daystart, dayend, period, duration, dayDate)
                  })
              }

          })

      });

      function renderTimeslots(id, startDateTimeUTC, endDateTimeUTC, period, duration, dayDate)
      {
          console.log(startDateTimeUTC);
          console.log(endDateTimeUTC);
          uBookable.GetTimeSlot(id, startDateTimeUTC, endDateTimeUTC, period, duration).done(function (timeslots) {
              for (var x = 0, length = timeslots.length; x < length; x++) {
                  timeslots[x].DateStart = timeslots[x].StartTime.FixSerialisedDate();
                  timeslots[x].DateEnd = timeslots[x].EndTime.FixSerialisedDate();
                  timeslots[x].StartTime = moment(timeslots[x].StartTime.FixSerialisedDate()).format('HH:SS');
                  timeslots[x].EndTime = moment(timeslots[x].EndTime.FixSerialisedDate()).format('HH:SS');

              }
              timeslots["Title"] = "Available slots for " + dayDate;
              timeslots["NodeId"] = id;

              timeslots["DayStart"] = startDateTimeUTC;
              timeslots["DayEnd"] = endDateTimeUTC;

              $("#day-bookings").html(Mustache.render($('#day-bookings-tmpl').html(), timeslots));
          })
      }


  }
));


  Date.prototype.withoutTime = function () {
      var d = new Date(this);
      d.setHours(0, 0, 0, 0, 0);
      return d
  }

  String.prototype.FixSerialisedDate = function () {
      return new Date(parseInt(this.replace("/Date(", "").replace(")/", ""), 10));
  }