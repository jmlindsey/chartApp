﻿
// strip out all the connecting words
var wordsToRemove = /\s*(^is|was|has|had|of|^a\s+|\s+a$|\s+a\s+|\s+to\s+|^to\s+|\s+to$\s+at\s+|^at\s+|\s+at$)\s*/g;


// vital and intervention tokens to regex match from dictation string
var vitalTokens = [
        {
            re: /(respiratory rate|breathing)\s[0-9]+/,
            valueRe: /[0-9]+/,
            action: (value) => {
                $(function () {
                    $('#rr').val(value).trigger('change');
                });
            }
        },
        {
            re: /(heart rate)\s[0-9]+/,
            valueRe: /[0-9]+/,
            action: (value) => {
                $(function () {
                    $('#hr').val(value).trigger('change');
                });
            }
        },
        {
            re: /(saturation)\s[0-9]+/,
            valueRe: /[0-9]+/,
            action: (value) => {
                $(function () {
                    $('#saturation').val(value).trigger('change');
                });
            }
        },

        // breath sounds not implemented yet
        {
            re: /(clear|diminished|coarse|wheezes)\s(all lobes|upper lobes|upper left lobe|left upper lobe)/,
            valueRe: /clear|diminished|coarse|wheezes/,
            action: (value) => {
                $(function () {
                    $('#uls').val(value).trigger('change');
                });
            }
        },
        {
            re: /(clear|diminished|coarse|wheezes)\s(all lobes|upper lobes|upper right lobe|right upper lobe)/,
            valueRe: /clear|diminished|coarse|wheezes/,
            action: (value) => {
                $(function () {
                    $('#urs').val(value).trigger('change');
                })
            }
        },
        {
            re: /(clear|diminished|coarse|wheezes)\s(all lobes|lower lobes|lower left lobe|left lower lobe)/,
            valueRe: /clear|diminished|coarse|wheezes/,
            action: (value) => {
                $(function () {
                    $('#lls').val(value).trigger('change');
                })
            }
        },
        {
            re: /(clear|diminished|coarse|wheezes)\s(all lobes|lower lobes|lower right lobe|right lower lobe)/,
            valueRe: /clear|diminished|coarse|wheezes/,
            action: (value) => {
                $(function () {
                    $('#lrs').val(value).trigger('change');
                })
            }
        },
        {
            re: /(flow rate|placed on)\s[0-9]+/,
            valueRe: /[0-9]+/,
            action: (value) => {
                $(function () {
                    $('#flow').val(value).trigger('change');
                })
            }
        },
        {
            re: /Nebulizer treatment|nebulizer|neb|breathing treatment/,
            valueRe: /neb|breathing treatment/,
            action: (value) => {
                $(function () {
                    if ($('#activity1').val().length == 0) {
                        $('#activity1').val('nebulizer').trigger('change');
                    }
                    else if ($('#activity2').val().length == 0
                        && $('#activity1').val() != 'nebulizer') {
                        $('#activity2').val('nebulizer').trigger('change');
                    }
                    else if ($('#activity3').val().length == 0
                        && $('#activity2').val() != 'nebulizer'
                        && $('#activity1').val() != 'nebulizer') {
                        $('#activity3').val('nebulizer').trigger('change');
                    }
                    else if ($('#activity4').val().length == 0
                        && $('#activity3').val() != 'nebulizer'
                        && $('#activity2').val() != 'nebulizer'
                        && $('#activity1').val() != 'nebulizer') {
                        $('#activity4').val('nebulizer').trigger('change');
                    }
                })
            }
        },
        {
            re: /meter dose inhaler|inhaler|mdi/,
            valueRe: /meter dose inhaler|inhaler|mdi/,
            action: (value) => {
                $(function () {
                    if ($('#activity1').val().length == 0) {
                        $('#activity1').val('mdi').trigger('change');
                    }
                    else if ($('#activity2').val().length == 0
                        && $('#activity1').val() != 'mdi') {
                        $('#activity2').val('mdi').trigger('change');
                    }
                    else if ($('#activity3').val().length == 0
                        && $('#activity2').val() != 'mdi'
                        && $('#activity1').val() != 'mdi') {
                        $('#activity3').val('mdi').trigger('change');
                    }
                    else if ($('#activity4').val().length == 0
                        && $('#activity3').val() != 'mdi'
                        && $('#activity2').val() != 'mdi'
                        && $('#activity1').val() != 'mdi') {
                        $('#activity4').val('mdi').trigger('change');
                    }

                })
            }
        },
        {
            re: /percussion|cpt/,
            valueRe: /percussion|cpt/,
            action: (value) => {
                $(function () {
                    if ($('#activity1').val().length == 0) {
                        $('#activity1').val('cpt').trigger('change');
                    }
                    else if ($('#activity2').val().length == 0
                        && $('#activity1').val() != 'cpt') {
                        $('#activity2').val('cpt').trigger('change');
                    }
                    else if ($('#activity3').val().length == 0
                        && $('#activity2').val() != 'cpt'
                        && $('#activity1').val() != 'cpt') {
                        $('#activity3').val('cpt').trigger('change');
                    }
                    else if ($('#activity4').val().length == 0
                        && $('#activity3').val() != 'cpt'
                        && $('#activity2').val() != 'cpt'
                        && $('#activity1').val() != 'cpt') {
                        $('#activity4').val('cpt').trigger('change');
                    }
                })
            }
        },
        {
            re: /spirometer|spirometry/,
            valueRe: /spirometer|spirometry/,
            action: (value) => {
                $(function () {
                    if ($('#activity1').val().length == 0) {
                        $('#activity1').val('is').trigger('change');
                    }
                    else if ($('#activity2').val().length == 0
                        && $('#activity1').val() != 'cpt') {
                        $('#activity2').val('is').trigger('change');
                    }
                    else if ($('#activity3').val().length == 0
                        && $('#activity2').val() != 'is'
                        && $('#activity1').val() != 'is') {
                        $('#activity3').val('is').trigger('change');
                    }
                    else if ($('#activity4').val().length == 0
                        && $('#activity3').val() != 'is'
                        && $('#activity2').val() != 'is'
                        && $('#activity1').val() != 'is') {
                        $('#activity4').val('is').trigger('change');
                    }
                })
            }
        },
        {
            re: /well tolerated|good treatment|tolerated well/,
            valueRe: /well tolerated|good treatment|tolerated well/,
            action: (value) => {
                $(function () {
                    $('#wellTolerated').prop("checked", true).trigger('click');
                });
            }
        },
        {
            re: /poorly tolerated|tolerated poorly|poor toleration/,
            valueRe: /poorly tolerated|tolerated poorly|poor toleration/,
            action: (value) => {
                $(function () {
                    $('#poorlyTolerated').prop("checked", true).trigger('click');
                });
                
            }
        },
        {
            re: /productive/,
            valueRe: /productive/,
            action: (value) => {
                $(function () {
                    $('#productive').prop("checked", true).trigger('click');
                });
            }
        },
        {
            re: /non productive/,
            valueRe: /non productive/,
            action: (value) => {
                $(function () {
                    $('#nonProductive').prop("checked", true).trigger('click');
                    console.log('checked!');
                });
            }
        },

        // submit the chart
        {
            re: /add chart/,
            valueRe: /add chart/,
            action: (value) => {
                $(function () {
                    $('#submitButton').click();
                });
            }
        }
    ];


    // textProcessor takes input from speechHandler  
    function TextProcessor() {

        var self = this;

        // calls an action method (to fill in form) based on vital or intervention value matched
        this.findVitals = function (text) {
            for (var index in vitalTokens) {
                var value;
                var vital = vitalTokens[index];
                var match = text.match(vital.re);
                if (match) {
                    value = match[0].match(vital.valueRe)[0];
                    console.log('value: ' + value);
                }
                if (value) {
                    vital.action(value);
                    value = null;
                }
            }
        };

        // removes all extemporaneous words from input string and passes string to findVitals
        this.process = function (text) {
            console.log('processing');
            text = text.replace(wordsToRemove, ' ').trim().toLowerCase();
            console.log(text);
            self.findVitals(text);
        };
    }


// highligts each changed vital / intervention
$(function () {
    $("input").change(function () {
        $(this).css({ 'font-weight': 900 });
        $(this).animate({
            backgroundColor: "#ffff00"
            //color: "white",
        },
            1000);

    });
});