$(function() {
	$("#accept-btn").click(function() {
		process($(this), 'approve', "data-okurl");
	});

	$("#reject-btn").click(function() {
		process($(this), 'reject', "data-failurl");
	});

	function process(button, state, callbackUrlAttr) {
		var redirectUrl = button.attr(callbackUrlAttr);
		var trId = button.attr("data-id");

		$.ajax({
			url: "deposit/callback?id=" + trId,
			success: function(callbackurl) {
				runCallback(callbackurl, redirectUrl);
			},
			error: function(jqXhr, exception) {
				alert(jqXhr.responseText + "\r\n" + jqXhr.statusText);
			},
			async: false
		});
	}

	function runCallback(url, redirectUrl) {
		$.ajax({
			url: url,
			success: function(result) {
				$(location).prop('href', redirectUrl);
			},
			error: function(jqXhr, exception) {
				alert(jqXhr.responseText + "\r\n" + jqXhr.statusText);
			},
			async: false
		});
	}
});