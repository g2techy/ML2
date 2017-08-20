function LoanSearchInit() {
	$(function () {
		var _loanIDtoBeDeleted = -1;
		$("#btnSearch").click(function () {
			SearchLoan();
		});
		$("#btnAdd").click(function () {
			window.location = _loanAddUrl;
		});
		$('#StartDate,#EndDate').val('');

		function RebindJS() {
			$("[data-edit][data-loanID]").click(function () {
				var _loanID = $(this).attr("data-loanID");
				window.location = _loanUpdateUrl + "?loanID=" + _loanID;
			});
			$("[data-del][data-loanID]").click(function () {
				_loanIDtoBeDeleted = $(this).attr("data-loanID");
			});
			$("[data-pager]").click(function () {
				NavigatePage($(this).attr("data-si"), $(this).attr("data-ps"));
			});
			$('[data-toggle="popover"]').popover({
				trigger: 'hover',
				placement: 'left',
				html: true,
				content: function () {
					return $(this).parent().find("div").html();
				}
			});
			_loanIDtoBeDeleted = -1;
			$('[data-del][data-toggle="confirmation"]').confirmation({
				singleton: true,
				btnOkClass: 'btn btn-primary btn-xs',
				btnCancelClass: 'btn btn-info btn-xs',
				onConfirm: function () {
					DeleteLoan(_loanIDtoBeDeleted);
				}
			});
		}
		RebindJS();

		function SearchLoan() {
			var _data = $('#frmLoanSearch').serialize();
			$.ajax({
				type: 'POST',
				url: _loanSearchUrl,
				data: _data,
				dataType: "text",
				success: function (result) {
					$('#divLoanList').html(result);
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function NavigatePage(stIndex, pageSize) {
			$.ajax({
				type: 'GET',
				url: _loanListUrl + "?st=" + stIndex + "&ps=" + pageSize,
				data: "",
				dataType: "text",
				success: function (result) {
					$('#divLoanList').html(result);
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function DeleteLoan(loanID) {
			$.ajax({
				type: 'GET',
				url: _loanDeleteUrl + "?loanID=" + loanID,
				data: "",
				dataType: "text",
				success: function (result) {
					$('#divLoanList').html(result);
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
	});
}

function LoanAddInit() {
	$(function () {
		if (_isFirstRequest == "1") {
			$('#StartDate,#EndDate,#PrincipalAmount,#MonthlyInterest').val('');
		}
		$('#btnSearch').click(function () {
			window.location = _loanSearchUrl;
		});
		$('#btnSave').click(function () {
			if (!$('#frmAddLoan').valid()) {
				return false;
			}
			return true;
		});
	});
}

function LoanUpdateInit() {
	$(function () {
		$('#btnAddPayment').click(function () {
			AddPayment();
		});
		$('#btnClose').click(function () {
			CloseLoan();
		});
		$('#btnCalcInt').click(function () {
			CalcInt($("#interestAsOn").val());
		});
		$('#btnSearch').click(function () {
			window.location = _loanSearchUrl;
		});
		$('#btnUpdate').click(function () {
			if (!$('#frmUpdLoan').valid()) {
				return false;
			}
			return true;
		});
		
		function RebindJS() {
			$('[data-del][data-payID]').click(function () {
				DeletePayment($(this).attr('data-payID'));
			});
			$('.date-picker').datepicker({});
			$('#btnCalcInt').click(function () {
				CalcInt($("#interestAsOn").val());
			});
		}
		RebindJS();

		function DeletePayment(payID) {
			$.ajax({
				type: 'GET',
				url: _payDelUrl,
				data: "loanPayID=" + payID,
				dataType: "text",
				success: function (result) {
					$('#divPayGrid').html(result);
					CheckLoanStatus();
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function AddPayment() {
			if (!$('#frmPayment').valid()) {
				return false;
			}
			var _data = $('#frmPayment').serialize();
			$.ajax({
				type: 'POST',
				url: _payAddUrl,
				data: _data,
				dataType: "text",
				success: function (result) {
					$('#divPayGrid').html(result);
					ClearPayFields();
					CheckLoanStatus();
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function ClearPayFields() {
			$('#frmPayment input, #frmPayment select').not('[type=hidden]').val('');
		}
		function CheckLoanStatus() {
			var _princAmt = parseFloat($('#PrincipalAmount').val());
			if ((_princPaid >= (_princAmt - 1))) {
				$('#btnClose').removeAttr("disabled");
			} else {
				$("#btnClose").attr("disabled", "disabled");
			}
			var _status = $('#Status').val();
			if (_status == '4') {
				$("button, input, select").not("#btnSearch").attr("disabled", "disabled");
			}
			if (!_isFirstLoad) {
				CalcInt();
			}
			_isFirstLoad = false;
		}
		function CloseLoan() {
			$.ajax({
				type: 'POST',
				url: _loanCloseUrl,
				data: "loanID=" + $("#LoanID").val(),
				dataType: "text",
				success: function (result) {
					window.location.reload();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function CalcInt(intAsOn) {
			var _data = "loanID=" + $("#LoanID").val();
			if (intAsOn != null && intAsOn != 'undefined') {
				_data += "&intAsOn=" + intAsOn;
			}
			$.ajax({
				type: 'GET',
				url: _calcIntUrl,
				data: _data,
				dataType: "text",
				success: function (result) {
					$('#divCalcInt').html(result);
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		var _isFirstLoad = true;
		ClearPayFields();
		CheckLoanStatus();
	});
}