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
			$('#SaleDate,#TotalWeight,#RejectionWeight,#SelectionWeight,#UnitPrice,#NetSaleAmount,#DueDays').val('');
		}
		$('#TotalWeight,#RejectionWeight,#SelectionWeight,#UnitPrice').focusout(function () {
			calculateWt();
		});
		$('#btnSearch').click(function () {
			window.location = _salesSearchUrl;
		});
		function calculateWt() {
			var _totalWt = $('#TotalWeight').val();
			var _rejWt = $('#RejectionWeight').val();
			var _selWt = _totalWt - (_totalWt * (_rejWt / 100));
			$('#SelectionWeight').val(_selWt.toFixed(2));
			var _unitPrice = $('#UnitPrice').val();
			$('#NetSaleAmount').val((_unitPrice * _selWt).toFixed(2));
		}
	});
}

function SaleUpdateInit() {
	$(function () {

		$('#TotalWeight,#RejectionWeight,#SelectionWeight,#UnitPrice').focusout(function () {
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

		function RebindJS() {
			$('[data-del][data-bdid]').click(function () {
				DeleteBrokerage($(this).attr('data-bdid'));
			});
			$('[data-del][data-payID]').click(function () {
				DeletePayment($(this).attr('data-payID'));
			});
		}
		RebindJS();

		function calculateWt() {
			var _totalWt = $('#TotalWeight').val();
			var _rejWt = $('#RejectionWeight').val();
			var _selWt = _totalWt - (_totalWt * (_rejWt / 100));
			$('#SelectionWeight').val(_selWt.toFixed(2));
			var _unitPrice = $('#UnitPrice').val();
			$('#NetSaleAmount').val((_unitPrice * _selWt).toFixed(2));
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
		calculateWt();
		ClearPayFields();
		ClearBrokerageFields();
		CheckSaleStatus();
	});
}