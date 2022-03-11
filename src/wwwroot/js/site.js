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
		var callbackurl = button.attr("data-callbackurl") + "?transaction-id=" + trId + "&state=" + state + "&external-id=" + getUid();
		runCallback(callbackurl, redirectUrl);
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

	function getUid() {
		var firstPart = (Math.random() * 46656) | 0;
		var secondPart = (Math.random() * 46656) | 0;
		firstPart = ("000" + firstPart.toString(36)).slice(-3);
		secondPart = ("000" + secondPart.toString(36)).slice(-3);
		return firstPart + secondPart;
	}
});