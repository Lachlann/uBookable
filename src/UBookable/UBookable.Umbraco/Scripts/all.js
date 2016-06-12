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

                      uBookable.GetTimeSlot(id, startDateTimeUTC, endDateTimeUTC, period, duration).done(function (timeslots) {
                          for (var x = 0, length = timeslots.length; x < length; x++)
                          {
                              timeslots[x].StartTime = moment(timeslots[x].StartTime.FixSerialisedDate()).format('HH:SS');
                              timeslots[x].EndTime = moment(timeslots[x].EndTime.FixSerialisedDate()).format('HH:SS');
                          }
                          timeslots["Title"] = "Available slots for " + target.date.format("dddd, MMMM Do YYYY")
                          $("#day-bookings").html(Mustache.render($('#day-bookings-tmpl').html(), timeslots));
                      })
                  }
              }
          });
      });

      // The rest of the code goes here!

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