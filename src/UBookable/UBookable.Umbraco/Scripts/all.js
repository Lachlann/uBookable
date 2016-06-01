var DEBUG = true;
if (!DEBUG) {
    if (!window.console) window.console = {};
    var methods = ["log", "debug", "warn", "info"];
    for (var i = 0; i < methods.length; i++) {
        console[methods[i]] = function () { };
    }
}
console.log("GO")

uBookable.SaveBooker("Kirsteen").done(function (booker) {
    console.log("booker");
    console.log(booker);
    uBookable.SaveBooking('151', booker.BookerID, new Date("October 15, 2016 11:13:00"), new Date("October 19, 2016 11:13:00")).
        done(function (data) {
            console.log("booking");
            console.log(data);
        });
});



//new Date("October 13, 2016 11:13:00")

//new Date("October 14, 2016 11:13:00")