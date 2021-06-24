'use strict';

// Handler for "Подсчитать" button click event.
// Gets the values from the input field, split it 
// on two arrays with correct and incorrect values 
// and applies analysis results of searched strings 
// into the table.
$('#countBtn').on('click', function(e) {
    e.preventDefault();
    let numbersArray = getInputValuesArray();
    let errorsArray = [];

    if(numbersArray.length > 0) {
        clearTableRows();

        numbersArray.forEach(function(val) {
            if(isValueValid(val)) {
                getStringById(val, analizeTheString);
            }
            else {
                errorsArray.push(val);
            }                
        });

        if(errorsArray.length > 0) {
            showErros(errorsArray);
        }
        else
        {
            clearErrorData();
        }
    }         
});

// Gets the text value from the input field, 
// parse it into number array and returns it
function getInputValuesArray() {
    let inputValue = $('#stringsInput').val().trim();
    let filteredNumsArray = [];

    if(inputValue.length > 0)
    {
        let inputArray = inputValue.split(/,|;/); 
        filteredNumsArray = inputArray.filter(isUnique);
    }    

    return filteredNumsArray;
}

// A filter comparer that checks values (val) of array (self) 
// if they are unique
function isUnique(val, index, self) {
    return self.indexOf(val) === index;
}

// Checks an input value if it can be parsed 
// to number and it is integer
function isValueValid(inputValue) {
    if(!isNaN(+inputValue) && inputValue.indexOf('.') < 0) {
        return +inputValue >= 1 && +inputValue <= 20;
    }
    else {
        return false;
    }
}

// Calls a GET method, then sends its result 
// as the first argument of the callback function (callback) and calls it
function getStringById(id, callback) {
    let url = 'api/textstrings/' + id;
    $.get(url, function(result) {
        if(result)
        {
            callback(result.text, appendResultToTable);
        }        
    });
}

// Calls a GET method with a string as it's parameter, 
// then call the callback function (callback) with its result
function analizeTheString(str, callback) {
    let url = '/Text/AnalizeTheString?str=' + str;
    $.get(url, function(result) {
        callback(result);
    });
}

// Removes all current results 
// from table with id 'resultsTable'
function clearTableRows() {
    $('#resultsTable tbody tr').remove();
}

// Appends analysis results (result) of searched value 
// to table with id 'resultsTable'
function appendResultToTable(result) {
    let resultTemplate = 
        `<tr>
            <td>${result.text}</td>
            <td class="text-center">${result.wordsCount}</td>
            <td class="text-center">${result.vowelsCount}</td>
        </tr>`;
    $('#resultsTable tbody').append(resultTemplate);
}

// Writes input value errors inside hidden span element  
// and shows it if it is hidden
function showErros(errorsArray) {
    let errorStr = errorsArray.join(', ');
    let $errorSpanElem = $('#errorsDiv span');
    $($errorSpanElem).text(errorStr);

    if(!$errorSpanElem.parent().is(':visible')) {
        $($errorSpanElem.parent()).show(400);
    }
}

// Clears input value errors from span element 
// and hide it if new input errors weren't found
function clearErrorData() {
    let $errorSpanElem = $('#errorsDiv span');
    $($errorSpanElem).text('');
    let errorStringLength = $errorSpanElem.text().length;

    if(errorStringLength === 0 && $errorSpanElem.parent().is(':visible')) {        
        $($errorSpanElem.parent()).hide(200);
    }
}