"use strict"

Array.prototype.last = function () { return this[this.length - 1]; }

// localstorage functions
var smajtom = (function (global) {
    var KEY = 'smajtom',
        MAX_SIZE = 30,
        getCache = null;

    function generateId(id) {
        return (id + '').replace(/,/g, '');
    }

    function set(id) {
        id = generateId(id);
        var existing = get();

        if (!exists(id)) {
            if (existing.length >= MAX_SIZE) {
                // prune storage
                existing.splice(0, (existing.length + 1) - MAX_SIZE);
            }

            existing.push(id);
        }

        localStorage.setItem(KEY, existing.join(','));
    }

    function get() {
        var existing = localStorage.getItem(KEY);
        if (existing !== null) {
            return existing.split(',');
        }

        return [];
    }

    function exists(id) {
        id = generateId(id);
        var existing = get();

        if (existing.indexOf(id) > -1) {
            return true;
        }

        return false;
    }

    return {
        set: set,
        get: get,
        exists: exists
    };
}(window));

var $audioBites = $('audio');
$audioBites.each(function () {
    // generate an estimated audio bite id
    $(this).data('ref', $(this).find('source').attr('src').split('/').last().replace(/[^\w]/gi, ''));

    if (smajtom.exists($(this).data('ref'))) {
        $(this).addClass('smajta');
    }
}).bind('playing', function (event) {
    // set audio bite as listed to
    smajtom.set($(this).data('ref'));
    $(this).addClass('smajta');

    // when an audio bite starts playing pause the others
    var currentBite = this;
    $audioBites.each(function () {
        if (this !== currentBite) {
            this.pause();
            // this.currentTime = 0;
        }
    });
});