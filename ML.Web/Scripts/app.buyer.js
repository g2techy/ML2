function BuyerSearchInit() {
	$(function () {
		var _buyerIDtoBeDeleted = -1;
		$("#btnSearch").click(function () {
			SearchBuyers();
		});
		$("#btnAdd").click(function () {
			window.location = _buyerAddUrl;
		});
		function RebindJS() {
			$("[data-edit][data-buyerID]").click(function () {
				var _buyerID = $(this).attr("data-buyerID");
				window.location = _buyerAddUrl + "?buyerID=" + _buyerID;
			});
			$("[data-del][data-buyerID]").click(function () {
				_buyerIDtoBeDeleted = $(this).attr("data-buyerID");
			});
			$("[data-pager]").click(function () {
				NavigatePage($(this).attr("data-si"), $(this).attr("data-ps"));
			});
			_buyerIDtoBeDeleted = -1;
			$('[data-del][data-toggle="confirmation"]').confirmation({
				singleton: true,
				btnOkClass: 'btn btn-primary btn-xs',
				btnCancelClass: 'btn btn-info btn-xs',
				onConfirm: function () {
					DeleteBuyer(_buyerIDtoBeDeleted);
				}
			});
		}
		RebindJS();

		function SearchBuyers() {
			var _data = $('#frmBuyerRearch').serialize();
			$.ajax({
				type: 'POST',
				url: "/Buyer/Index",
				data: _data,
				dataType: "text",
				success: function (result) {
					$('#divBuyerList').html(result);
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
				url: _buyerListUrl + "?st=" + stIndex + "&ps=" + pageSize,
				data: "",
				dataType: "text",
				success: function (result) {
					$('#divBuyerList').html(result);
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		function DeleteBuyer(buyerID) {
			$.ajax({
				type: 'GET',
				url: _buyerDelUrl + "?buyerID=" + buyerID,
				data: "",
				dataType: "text",
				success: function (result) {
					$('#divBuyerList').html(result);
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
	});
}

function BuyerAddInit() {
	$(function () {
		$("#btnSearch").click(function () {
			window.location = _buyerSearchUrl;
		});
	});
}

