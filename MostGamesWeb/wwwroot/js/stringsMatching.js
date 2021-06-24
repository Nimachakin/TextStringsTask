'use strict';

applyBtnHandlers();

function applyBtnHandlers() {
    applyIndexBtnHandler();    
    applyInputFileHandlers();
    applySubmitFormHandler();
    
}

function applyInputFileHandlers() {
    $('#ruTextFileInputBtn').on('click', function(e) {
        e.preventDefault();
        $('#ruTextFileInput').trigger('click');
    });
    
    $('#enTextFileInputBtn').on('click', function(e) {
        e.preventDefault();
        $('#enTextFileInput').trigger('click');
    });
    
    $('#ruTextFileInput').on('change', function(e) {
        e.preventDefault();
        let chosenFileName = GetFileName($(this));
        $('#ruTextFileInputBtn').val(chosenFileName);
    });
    
    $('#enTextFileInput').on('change', function(e) {
        e.preventDefault();
        let chosenFileName = GetFileName($(this));
        $('#enTextFileInputBtn').val(chosenFileName);
    });
}

function applySubmitFormHandler() {
    $('#fileForm').on('submit', function(e) {
        e.preventDefault();
        $('#resultsBlock').empty();
        let ruFile = GetTextFileFromInput($('#ruTextFileInput'));
        let enFile = GetTextFileFromInput($('#enTextFileInput'));
    
        if(ruFile && enFile) {
            let form = $(this);
            let formData = new FormData();
            formData.append('ruTextFile', ruFile);
            formData.append('enTextFile', enFile);
    
            $.ajax({
                url: form.attr('action'), 
                type: form.attr('method'), 
                data: formData, 
                processData: false, 
                contentType: false
            })
            .then(function(content) {            
                $('#resultsBlock').append(content);
            })
            .fail(function() {
                $('#resultsBlock').append('Что-то пошло не так :(');
            });
        }  
    }); 
}

function applyIndexBtnHandler() {
    $('#ruIndexBtn').on('click', function(e) {
        e.preventDefault();
        let inputVal = $('#ruStringInput').val();
    
        if(inputVal && inputVal.length > 0) {
            let url = 'StringMatching/CountStringIndex?str=' + inputVal;
            $.post(url, function(result) {
                $('#ruIndexResult').val(result);
            })
        }   
    });
}

function GetTextFileFromInput($input) {
    if($($input)) {
        let file = $($input)[0].files[0];
        return file;
    }
    else {
        return null;
    }
};

function GetFileName($fileInput) {
    if($($fileInput)) {
        let fileName = $($fileInput)[0].files[0]['name'];
        return fileName;
    }
    else {
        return '';
    }
}