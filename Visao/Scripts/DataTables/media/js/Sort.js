jQuery.fn.dataTableExt.aTypes.unshift(
               function (sData) {
                   if (sData !== null && sData.match(/^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[012])\/(19|20|21)\d\d$/)) {
                       return 'date-uk';
                   }
                   return null;
               }
           );

jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-uk-pre": function (a) {
        var ukDatea = a.split('/');
        return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
    },

    "date-uk-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "date-uk-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
})

// Currency \ Money values Datatables.net 

jQuery.fn.dataTableExt.aTypes.unshift(
     function (sData) {
         if (sData.indexOf("R$ ") > -1) {
             return 'currency';
         } else {
             var sValidChars = "0123456789-,.";
             var Char;
             var bDecimal = false;

             /* Check the numeric part */
             for (i = 0 ; i < sData.length ; i++) {
                 Char = sData.charAt(i);
                 if (sValidChars.indexOf(Char) == -1) {
                     return null;
                 }

                 /* Only allowed one decimal place... */
                 if (Char == ",") {
                     if (bDecimal) {
                         return null;
                     }
                     bDecimal = true;
                 }
             }

             return 'numeric-comma';
         }
         
         
     }
 );


jQuery.fn.dataTableExt.oSort['numeric-comma-asc'] = function (a, b) {

    var xx = (a == "-") ? 0 : a.replace(".", "");
    var yy = (b == "-") ? 0 : b.replace(".", "");

    var x = (a == "-") ? 0 : xx.replace(/,/, ".");
    var y = (b == "-") ? 0 : yy.replace(/,/, ".");

    x = parseFloat(x);
    y = parseFloat(y);
    return ((x < y) ? -1 : ((x > y) ? 1 : 0));
};

jQuery.fn.dataTableExt.oSort['numeric-comma-desc'] = function (a, b) {

    var xx = (a == "-") ? 0 : a.replace(".", "");
    var yy = (b == "-") ? 0 : b.replace(".", "");

    var x = (a == "-") ? 0 : xx.replace(/,/, ".");
    var y = (b == "-") ? 0 : yy.replace(/,/, ".");

    x = parseFloat(x);
    y = parseFloat(y);
    return ((x < y) ? 1 : ((x > y) ? -1 : 0));
};

jQuery.fn.dataTableExt.oSort['currency-asc'] = function (a, b) {
    /* Remove any commas (assumes that if present all strings will have a fixed number of d.p) */
    var x = a == "-" ? 0 : a.replace(".", "").replace(",", ".");
    var y = b == "-" ? 0 : b.replace(".", "").replace(",", ".");

    x = x.substring(3);
    y = y.substring(3);

    /* Parse and return */
    x = parseFloat(x);
    y = parseFloat(y);
    return x - y;
};

jQuery.fn.dataTableExt.oSort['currency-desc'] = function (a, b) {
    /* Remove any commas (assumes that if present all strings will have a fixed number of d.p) */
    var x = a == "-" ? 0 : a.replace(".", "").replace(",", ".");
    var y = b == "-" ? 0 : b.replace(".", "").replace(",", ".");

    /* Remove the currency sign */
    x = x.substring(3);
    y = y.substring(3);

    /* Parse and return */
    x = parseFloat(x);
    y = parseFloat(y);
    return y - x;
};