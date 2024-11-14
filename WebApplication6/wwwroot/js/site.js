/**
* Template Name: NiceAdmin
* Updated: Aug 30 2023 with Bootstrap v5.3.1
* Template URL: https://bootstrapmade.com/nice-admin-bootstrap-admin-html-template/
* Author: BootstrapMade.com
* License: https://bootstrapmade.com/license/
*/
(function () {
    "use strict";

    /**
     * Easy selector helper function
     */
    const select = (el, all = false) => {
        el = el.trim()
        if (all) {
            return [...document.querySelectorAll(el)]
        } else {
            return document.querySelector(el)
        }
    }

    /**
     * Easy event listener function
     */
    const on = (type, el, listener, all = false) => {
        if (all) {
            select(el, all).forEach(e => e.addEventListener(type, listener))
        } else {
            select(el, all).addEventListener(type, listener)
        }
    }

    /**
     * Easy on scroll event listener 
     */
    const onscroll = (el, listener) => {
        el.addEventListener('scroll', listener)
    }

    /**
     * Sidebar toggle
     */
    if (select('.toggle-sidebar-btn')) {
        on('click', '.toggle-sidebar-btn', function (e) {
            select('body').classList.toggle('toggle-sidebar')
        })
    }

    /**
     * Search bar toggle
     */
    if (select('.search-bar-toggle')) {
        on('click', '.search-bar-toggle', function (e) {
            select('.search-bar').classList.toggle('search-bar-show')
                })
    }
})();

function onClickCheckbox(event) {
    var element = $(event.target);
    var parentElement = $(event.target).parent();
    var id = parseInt($(element).val());
    var index = paymentOrderIDs.indexOf(id);
    var countItem = $('input[name="selecteditem"]').length;

    if (element.is(':checked')) {
        if (index <= 0) {
            paymentOrderIDs.push(id);
        }
        //$(parentElement).parent().addClass('bg-whitesmoke');

        if (countItem == paymentOrderIDs.length) {
            $('#chk_checkall_paymentorder').prop('checked', true);
        }
    } else {
        if (index > -1) {
            paymentOrderIDs.splice(index, 1);
        }
        //$(parentElement).parent().removeClass('bg-whitesmoke');

        $('#chk_checkall_paymentorder').prop('checked', false);
    }
}
