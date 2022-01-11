window.JsFunctionsmt = {
    //enabled disabled btn
    disenabButton: function (idbutton, statButton) {
        document.getElementById(idbutton).disabled = statButton;
    },

    hidenButton: function (idbutton, statButton) {
        document.getElementById(idbutton).hidden = statButton;
    },
    hideshowpopup: function (idpopup, sts) {
        $('#' + idpopup).modal(sts);
    },
    loadiframe: function (idiframe) {
        
        document.getElementById(idiframe).src += '';
    },
    focusElement: function (id) {
        const element = document.getElementById(id); element.focus();
    },
    highlight_row: function (idtable) {
        var table = document.getElementById(idtable);
        var cells = table.getElementsByTagName('td');

        for (var i = 0; i < cells.length; i++) {
            // Take each cell
            var cell = cells[i];
            // do something on onclick event for cell
            cell.onclick = function () {
                // Get the row id where the cell exists
                var rowId = this.parentNode.rowIndex;

                var rowsNotSelected = table.getElementsByTagName('tr');
                for (var row = 0; row < rowsNotSelected.length; row++) {
                    rowsNotSelected[row].style.backgroundColor = "";
                    rowsNotSelected[row].classList.remove('selected');
                }
                var rowSelected = table.getElementsByTagName('tr')[rowId];
                rowSelected.style.backgroundColor = "#87bb4b";
                rowSelected.className += " selected";
            }
        }

    }

};


$('#btn1').click(function () {
    // reset modal if it isn't visible
    if (!($('.modal.in').length)) {
        $('.modal-dialog').css({
            top: 0,
            left: 0
        });
    }
    $('#myModal').modal({
        backdrop: false,
        show: true
    });

    $('.modal-dialog').draggable({
        handle: ".modal-header"
    });
});

