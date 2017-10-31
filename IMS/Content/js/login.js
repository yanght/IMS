$(function () {
    $("#loginbtn").click(function () {
        var username = $("input[name='UserName']").val();
        var password = $("input[name='Password']").val();
        if ($.trim(username).length == 0) {
            alert("请输入用户名");
            return false;
        }
        if ($.trim(password).length == 0) {
            alert("请输入密码");
            return false;
        }
        $("form").submit();
    })
})