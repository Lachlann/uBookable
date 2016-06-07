var DEBUG = true;
if (!DEBUG) {
    if (!window.console) window.console = {};
    var methods = ["log", "debug", "warn", "info"];
    for (var i = 0; i < methods.length; i++) {
        console[methods[i]] = function () { };
    }
}
console.log(new Date("06-14-2016"));



/*	*/

  // IIFE - Immediately Invoked Function Expression
  (function(yourcode) {

      // The global jQuery object is passed as a parameter
      yourcode(window.jQuery, window, document);

  }(function($, window, document) {

      // The $ is now locally scoped 

      // Listen for the jQuery ready event on the document
      $(function() {
          var dateconfig = {
              startDate: new Date(),
              selectForward: true,
              beforeShowDay: function (t) {

                  var valid = !(t.withoutTime().getTime() === new Date("06-14-2016").getTime());  //disable saturday and sunday
                  var _class = '';
                  var _tooltip = valid ? '' : 'unavailable';
                  return [valid, _class, _tooltip];
              }
          }
          $('.datepicker').dateRangePicker(dateconfig);
      });

      // The rest of the code goes here!

  }
));


  Date.prototype.withoutTime = function () {
      var d = new Date(this);
      d.setHours(0, 0, 0, 0, 0);
      return d
  }