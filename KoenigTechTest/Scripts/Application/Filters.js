
mainModule.filter("customNumberFilter", function() {
    return function (input, fractionSize) {
        input = parseFloat(input);

        if (input % 1 === 0) {
            input = input.toFixed(0);
        }
        else {
            input = input.toFixed(fractionSize || 2);
        }

        return input.toString()//.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    };
});