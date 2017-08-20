function DashboardInit() {
	$(function () {
		$("a[data-fn]").click(function (e) {
			e.preventDefault();
			var _fn = $(this).attr("data-fn");
			if (typeof _fn != 'undefined' && typeof eval(_fn) === "function") {
				eval(_fn)();
			}
		});

		var _saleChartOptions = {
			chart: {
				type: 'column'
			},
			title: {
				text: ''
			},
			xAxis: {
				categories: []
			},
			yAxis: {
				min: 0,
				title: {
					text: 'Total Net Sale Amount'
				},
				stackLabels: {
					enabled: true,
					style: {
						fontWeight: 'bold',
						color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
					}
				}
			},
			legend: {
				align: 'right',
				x: 0,
				verticalAlign: 'top',
				y: 25,
				floating: true,
				backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
				borderColor: '#CCC',
				borderWidth: 1,
				shadow: false
			},
			tooltip: {
				headerFormat: '<b>{point.x}</b><br/>',
				pointFormat: '{series.name}: {point.y:,.2f}'
			},
			plotOptions: {
				column: {
					dataLabels: {
						enabled: true,
						color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
					}
				}
			},
			series: []
		};

		var _brokChartOptions = {
			chart: {
				type: 'column'
			},
			title: {
				text: ''
			},
			xAxis: {
				categories: []
			},
			yAxis: {
				min: 0,
				title: {
					text: 'Total Brokerage'
				},
				stackLabels: {
					enabled: true,
					style: {
						fontWeight: 'bold',
						color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
					}
				}
			},
			legend: {
				align: 'right',
				x: 0,
				verticalAlign: 'top',
				y: 25,
				floating: true,
				backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
				borderColor: '#CCC',
				borderWidth: 1,
				shadow: false
			},
			tooltip: {
				headerFormat: '<b>{point.x}</b><br/>',
				pointFormat: '{series.name}: {point.y:,.2f}<br/>Total: {point.stackTotal:,.2f}'
			},
			plotOptions: {
				column: {
					stacking: 'normal',
					dataLabels: {
						enabled: true,
						color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
					}
				}
			},
			series: []
		};

		var _brokPieChartOptions = {
			chart: {
				plotBackgroundColor: null,
				plotBorderWidth: null,
				plotShadow: false,
				type: 'pie'
			},
			title: {
				text: ''
			},
			tooltip: {
				pointFormat: '{series.name}: <b>{point.y:,.2f}</b>'
			},
			plotOptions: {
				pie: {
					allowPointSelect: true,
					cursor: 'pointer',
					dataLabels: {
						enabled: true,
						//format: '<b>{point.name}</b>: {point.percentage:.2f} % - {point.y:,.2f}',
						format: '<b>{point.name}</b>: {point.percentage:.2f} %',
						style: {
							color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
						},
						connectorColor: 'silver'
					}
				}
			},
			series: []
		};

		var _loanChartOptions = {
			chart: {
				type: 'column'
			},
			title: {
				text: ''
			},
			xAxis: {
				categories: []
			},
			yAxis: {
				min: 0,
				title: {
					text: 'Total Pay Amount'
				},
				stackLabels: {
					enabled: true,
					style: {
						fontWeight: 'bold',
						color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
					}
				}
			},
			legend: {
				align: 'right',
				x: -25,
				verticalAlign: 'top',
				y: 0,
				floating: true,
				backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
				borderColor: '#CCC',
				borderWidth: 1,
				shadow: false
			},
			tooltip: {
				headerFormat: '<b>{point.x}</b><br/>',
				pointFormat: '{series.name}: {point.y:,.2f}<br/>Total: {point.stackTotal}'
			},
			plotOptions: {
				column: {
					stacking: 'normal',
					dataLabels: {
						enabled: true,
						color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
					}
				}
			},
			series: []
		};

		var _intPaidChartOptions = {
			chart: {
				type: 'column'
			},
			title: {
				text: ''
			},
			xAxis: {
				categories: []
			},
			yAxis: {
				min: 0,
				title: {
					text: 'Total Interest Received'
				},
				stackLabels: {
					enabled: true,
					style: {
						fontWeight: 'bold',
						color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
					}
				}
			},
			legend: {
				align: 'right',
				x: -25,
				verticalAlign: 'top',
				y: 0,
				floating: true,
				backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
				borderColor: '#CCC',
				borderWidth: 1,
				shadow: false
			},
			tooltip: {
				headerFormat: '<b>{point.x}</b><br/>',
				pointFormat: '{series.name}: {point.y:,.2f}<br/>Total: {point.stackTotal}'
			},
			plotOptions: {
				column: {
					stacking: 'normal',
					dataLabels: {
						enabled: true,
						color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
					}
				}
			},
			series: []
		};

		function RebindJS() {
			$("[data-pager]").click(function () {
				NavPageDuePayments($(this).attr("data-si"), $(this).attr("data-ps"));
			});
			$("[data-edit][data-saleID]").click(function () {
				var _saleID = $(this).attr("data-saleID");
				window.location = "/Sale/Update/?saleID=" + _saleID;
			});
			$('[data-toggle="popover"]').popover({
				trigger: 'hover',
				placement: 'left',
				html: true,
				content: function () {
					return $(this).parent().find("div").html();
				}
			});
		}

		function loadSaleTab() {
			loadCharts("#sale div[data-chart]");
		}
		function loadPaymentsTab() {
			$("#payments").find("div[data-list]").each(function () {
				var _elem = $(this);
				var _isLoaded = _elem.data("loaded");
				if (_isLoaded == "0") {
					var _fn = $(this).data("fn");
					if (typeof _fn != 'undefined' && typeof eval(_fn) === "function") {
						eval(_fn);
					}
				}
			});
		}
		function loadBrokerageTab() {
			loadCharts("#brokerage div[data-chart]");
		}
		function loadLoanTab() {
			loadCharts("#loan div[data-chart]");
		}
		function loadCharts(chartSelector) {
			$(chartSelector).each(function () {
				var _chartElem = $(this)
				var _isLoaded = _chartElem.data("loaded");
				if (_isLoaded == "0") {
					var _chIdx = _chartElem.data("ch-idx");
					var _chartOptions = eval(_chartElem.data("ch-opt"));
					$.getJSON("/Dashboard/ChartData/?chartType=" + _chIdx, function (data) {
						if (data["errorCode"] && data.errorCode == "1") {
							_chartElem.html("Info : " + data.errorMessage);
						} else {
							if (_chartOptions["series"] && data["series"]) {
								_chartOptions.series = data.series;
							}
							if (_chartOptions["xAxis"] && data["categories"]) {
								_chartOptions.xAxis.categories = data.categories
							}
							Highcharts.chart(_chartElem.attr("id"), _chartOptions);
						}
						_chartElem.data("loaded", "1");
					}).fail(function () {
						_chartElem.html("Error occured while loading chart...");
					});
				}
			});
		}
		function NavPageDuePayments(stIndex, pageSize) {
			$.ajax({
				type: 'GET',
				url: "/Dashboard/DuePayments/?st=" + stIndex + "&ps=" + pageSize,
				dataType: "text",
				success: function (result) {
					$('#divDuePayments').html(result);
					$('#divDuePayments').data("loaded", "1");
					RebindJS();
				},
				error: function (data) {
					alert(data);
				}
			});
		}
		RebindJS();
		$("a[data-fn]").first().click();
	});
}