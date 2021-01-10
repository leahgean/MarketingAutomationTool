$(document).ready(function ($) {
    $('#accordion-left').dcAccordion({
        eventType: 'click',
        autoClose: false,
        saveState: false,
        disableLink: false,
        showCount: false,
        speed: 'slow'
    });
    $('#accordion-right').dcAccordion({
        eventType: 'click',
        autoClose: false,
        saveState: false,
        disableLink: false,
        showCount: false,
        speed: 'slow'
    });
});