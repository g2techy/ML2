function SaleSearchInit() {
	$(function () {
		var _saleIDtoBeDeleted = -1;
		$("#btnSearch").click(function () {
			SearchSales();
		});
		$("#btnAdd").click(function () {
			window.location = _salesAddUrl;
		});
		$('#StartDate,#EndDate').val('');

		function RebindJS() {
			$("[data-edit][data-saleID]").click(function () {
				var _saleID = $(this).attr("data-saleID");
				window.location = _salesUpdateUrl + "?saleID=" + _saleID;
			});
			$("[data-del][data-saleID]").click(function () {
				_saleIDtoBeDeleted = $(this).attr("data-saleID");
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
			_saleIDtoBeDeleted = -1;
			$('[data-del][data-toggle="confirmation"]').confirmation({
				singleton: true,
				btnOkClass: 'btn btn-primary btn-xs',
				btnCancelClass: 'btn btn-info btn-xs',
				onConfirm: function () {
					DeleteSale(_saleIDtoBeDeleted);
				}
			});
		}
		RebindJS();

		function SearchSales() {
			var _data = $('#frmSaleRearch').serialize();
			$.ajax({
				type: 'POST',
				url: _salesSearchUrl,
				data: _data,
				dataType: "text",
				success: function (result) {
					$('#divSalesList').html(result);
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
				url: _salesListUrl + "?st=" + stIndex + "&ps=" + pageSize,
				data: "",
				dataType: "text",
				success: function (result) {
					$('#divSalesList').html(result);
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function DeleteSale(saleID) {
			$.ajax({
				type: 'GET',
				url: _salesDeleteUrl + "?saleID=" + saleID,
				data: "",
				dataType: "text",
				success: function (result) {
					$('#divSalesList').html(result);
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
	});
}

function SaleAddInit() {
	$(function () {
		if (_isFirstRequest == "1") {
			$('#SaleDate,#TotalWeight,#RejectionWeight,#SelectionWeight,#UnitPrice,#NetSaleAmount,#DueDays,#LessPer').val('');
		}
		$('#TotalWeight,#RejectionWeight,#SelectionWeight,#UnitPrice,#LessPer').focusout(function () {
			calculateWt();
		});
		$('#btnSearch').click(function () {
			window.location = _salesSearchUrl;
		});
		$('#btnSave').click(function () {
			if (!$('#frmAddSale').valid()) {
				return false;
			}
			var _totalWt = $('#TotalWeight').val();
			var _rejWt = $('#RejectionWeight').val();
			if (parseInt(_rejWt) >= parseInt(_totalWt)) {
				return false;
			}
			return true;
		});
		function calculateWt() {
			var _totalWt = $('#TotalWeight').val();
			var _rejWt = $('#RejectionWeight').val();
			var _selWt = (_totalWt - _rejWt);
			$('#SelectionWeight').val(_selWt.toFixed(2));
			var _unitPrice = $('#UnitPrice').val();
			var _lessPer = $('#LessPer').val();
			if (_lessPer == "") {
				_lessPer = 0
			}
			var _netSalAmt = (_unitPrice * _selWt) - ((_unitPrice * _selWt) * _lessPer / 100); 
			$('#NetSaleAmount').val(_netSalAmt.toFixed(2));
		}
	});
}

function SaleUpdateInit() {
	$(function () {

		$('#TotalWeight,#RejectionWeight,#SelectionWeight,#UnitPrice,#LessPer').focusout(function () {
			calculateWt();
		});
		$('#btnAddBrokerage').click(function () {
			AddBrokerage();
		});
		$('#btnAddPayment').click(function () {
			AddPayment();
		});
		$('#btnClose').click(function () {
			CloseSale();
		});
		$('#btnSearch').click(function () {
			window.location = _salesSearchUrl;
		});
		$('#btnUpdate').click(function () {
			if (!$('#frmUpdSale').valid()) {
				return false;
			}
			var _totalWt = $('#TotalWeight').val();
			var _rejWt = $('#RejectionWeight').val();
			if (parseInt(_rejWt) >= parseInt(_totalWt)) {
				return false;
			}
			return true;
		});
		$('#btnUpdatePayment').click(function () {
			UpdateBrokPayment();
		});

		function RebindJS() {
			$('[data-del][data-bdid]').click(function () {
				DeleteBrokerage($(this).attr('data-bdid'));
			});
			$('[data-del][data-payID]').click(function () {
				DeletePayment($(this).attr('data-payID'));
			});
			$('[data-pay][data-bdid]').click(function () {
				ShowBrokPayModel($(this).attr('data-bdid'));
			});
		}
		RebindJS();

		function calculateWt() {
			var _totalWt = $('#TotalWeight').val();
			var _rejWt = $('#RejectionWeight').val();
			var _selWt = (_totalWt - _rejWt);
			$('#SelectionWeight').val(_selWt.toFixed(2));
			var _unitPrice = $('#UnitPrice').val();
			var _lessPer = $('#LessPer').val();
			if (_lessPer == "") {
				_lessPer = 0
			}
			var _netSalAmt = (_unitPrice * _selWt) - ((_unitPrice * _selWt) * _lessPer / 100);
			$('#NetSaleAmount').val(_netSalAmt.toFixed(2));
		}
		function DeleteBrokerage(bdid) {
			$.ajax({
				type: 'GET',
				url: _brokDelUrl,
				data: "BDID=" + bdid,
				dataType: "text",
				success: function (result) {
					$('#divBrokGrid').html(result);
					CheckSaleStatus();
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function AddBrokerage() {
			if (!$('#frmBrokerage').valid()) {
				return false;
			}
			var _data = $('#frmBrokerage').serialize();
			$.ajax({
				type: 'POST',
				url: _brokAddUrl,
				data: _data,
				dataType: "text",
				success: function (result) {
					$('#divBrokGrid').html(result);
					ClearBrokerageFields();
					CheckSaleStatus();
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function DeletePayment(payID) {
			$.ajax({
				type: 'GET',
				url: _payDelUrl,
				data: "payID=" + payID,
				dataType: "text",
				success: function (result) {
					$('#divPayGrid').html(result);
					CheckSaleStatus();
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
					CheckSaleStatus();
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function ClearPayFields() {
			$('#frmPayment input').not('[type=hidden]').val('');
		}
		function ClearBrokerageFields() {
			$('#frmBrokerage input').not('[type=hidden]').val('');
		}
		function CheckSaleStatus() {
			var _netAmount = parseFloat($('#NetSaleAmount').val());
			if ((_totalPayAmount >= _netAmount) && _totalBrokerage > 0) {
				$('#btnClose').removeAttr("disabled");
			} else {
				$("#btnClose").attr("disabled", "disabled");
			}
			var _status = $('#Status').val();
			if (_status == '4') {
				$("button, input, select").not("#btnSearch").attr("disabled", "disabled");
			}
			$("#spnNetSaleAmt").html(_netAmount.toFixed(2));
			$("#spnTotalPayment").html(_totalPayAmount.toFixed(2));
			$("#spnOutPayment").html((_netAmount - _totalPayAmount).toFixed(2));
		}
		function CloseSale() {
			$.ajax({
				type: 'POST',
				url: _saleCloseUrl,
				data: "saleID=" + $("#SaleID").val(),
				dataType: "text",
				success: function (result) {
					window.location.reload();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function ShowBrokPayModel(bdid) {
			ClearBrokPayFields();
			var _model = $("#modelBrokPayment");
			_model.modal("show");
			_model.find("#BDID").val(bdid);
		}
		function UpdateBrokPayment() {
			if (!$('#frmBrokPayment').valid()) {
				return false;
			}
			var _data = $('#frmBrokPayment').serialize();
			$.ajax({
				type: 'POST',
				url: _brokPayUrl,
				data: _data,
				dataType: "text",
				success: function (result) {
					$('#divBrokGrid').html(result);
					debugger;
					ClearBrokPayFields();
					RebindJS();
					$("#modelBrokPayment").modal("hide");
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function ClearBrokPayFields() {
			$('#frmBrokPayment input, #frmBrokPayment textarea').not('[type=hidden]').val('');
		}
		calculateWt();
		ClearPayFields();
		ClearBrokerageFields();
		CheckSaleStatus();
	});
}