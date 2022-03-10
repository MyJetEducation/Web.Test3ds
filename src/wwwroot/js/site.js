$(function() {
	$("#accept-btn").click(function() {
		process($(this), 'accept', "data-okurl");
	});

	$("#reject-btn").click(function() {
		process($(this), 'reject', "data-failurl");
	});

	function process(button, state, callbackUrlAttr) {
		var redirectUrl = button.attr(callbackUrlAttr);
		var trId = button.attr("data-id");
		var callbackurl = button.attr("data-callbackurl") + "?transaction-id=" + trId + "state=" + state + "&external-id=" + getUid();
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
		var u = Date.now().toString(16) + Math.random().toString(16) + '0'.repeat(16);
		var guid = [u.substr(0, 8), u.substr(8, 4), '4000-8' + u.substr(13, 3), u.substr(16, 12)].join('-');
		return guid;
	}
});