
function InitLayout() {
	$(function () {
		$.ajaxSetup({
			beforeSend: function () {
				$("#divAjaxDialog").dialog("open");
			},
			complete: function () {
				$("#divAjaxDialog").dialog("close");
			},
			error: function (jqXHR, textStatus, errorThrown) {
				try {
					var _errMsg = "";
					var _errTrace = "";
					var _errID = "";
					var _errNumber = "E000000";
					if (jqXHR.status === 0) {
						_errMsg = 'Not connect. Verify Network.';
					} else if (jqXHR.status == 404) {
						_errMsg = 'Requested page not found. [404]';
					}
					else if (textStatus === 'parsererror') {
						_errMsg = 'Requested JSON parse failed.';
					} else if (textStatus === 'timeout') {
						_errMsg = 'Time out error.';
					} else if (textStatus === 'abort') {
						_errMsg = 'Ajax request aborted.';
					} else {
						try {
							var _errObj = JSON.parse(jqXHR.responseText);
							if (_errObj["ErrorID"]) {
								_errID = _errObj.ErrorID;
							}
							if (_errObj["ErrorNumber"]) {
								_errNumber = _errObj.ErrorNumber;
							}
							if (_errObj["ErrorMessage"]) {
								_errMsg = _errObj.ErrorMessage;
							}
							if (_errObj["ErrorTrace"]) {
								_errTrace = _errObj.ErrorTrace;
							}
						}
						catch (ers) {
							if (jqXHR.status == 500) {
								_errMsg = 'Internal Server Error [500].';
							}
							_errMsg = jqXHR.responseText;
						}
					}
					var _errDiv = $("#jsError");
					if (_errDiv.length > 0) {
						_errDiv.find("#spnErrorID").html(_errID);
						_errDiv.find("#spnErrorNumber").html(_errNumber);
						_errDiv.find("#divErrorMessage").html(_errMsg);
						_errDiv.find("#divErrorTrace").html(_errTrace);
						_errDiv.modal("show");
					}
				}
				catch (e) {
				}
			}
		});
		$.datepicker.setDefaults({
			dateFormat: 'yy-mm-dd',
			"changeMonth": true,
			"changeYear": true
		});
		$('.date-picker').datepicker({});
		$('#divAjaxDialog').dialog({
			modal: true, autoOpen: false, height: 60, width: 'auto', resizable: false,
			dialogClass: 'noTitleStuff'
		});
	});
}