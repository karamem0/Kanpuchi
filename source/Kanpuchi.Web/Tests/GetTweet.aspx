<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Tweet API テスト</title>
    <link rel="stylesheet" href="/Content/bootstrap.min.css">
    <script type="text/javascript" src="/Scripts/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="/Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="/Scripts/base64.min.js"></script>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="/Scripts/html5siv.min.js"></script>
    <script type="text/javascript" src="/Scripts/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <div class="container">
        <h1>Tweet API テスト</h1>
        <form id="request-header" class="form-horizontal">
            <div class="form-group">
                <label class="col-sm-2 control-label" for="authorization">deviceid</label>
                <div class="col-sm-10">
                    <input type="text" id="deviceid" name="deviceid" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for="authorization">devicekey</label>
                <div class="col-sm-10">
                    <input type="text" id="devicekey" name="devicekey" class="form-control" />
                </div>
            </div>
        </form>
        <form id="request-query" class="form-horizontal">
            <div class="form-group">
                <label class="col-sm-2 control-label" for="minid">minid</label>
                <div class="col-sm-10">
                    <input type="number" id="minid" name="minid" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for="maxid">maxid</label>
                <div class="col-sm-10">
                    <input type="number" id="maxid" name="maxid" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for="minid">itemcount</label>
                <div class="col-sm-10">
                    <input type="number" id="itemcount" name="itemcount" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12 text-right">
                    <button type="button" id="button" class="btn btn-default">実行</button>
                </div>
            </div>
        </form>
        <div class="panel panel-default">
            <div id="console" class="panel-body"></div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#button").click(function () {
                $("#console").html(null);
                $.ajax({
                    type: "GET",
                    url: encodeURI("/api/tweet?" + $("#request-query").serialize()),
                    beforeSend: function (xhr) {
                        var name = $("#deviceid").val();
                        var password = $("#devicekey").val();
                        var credentials = Base64.encode(name + ":" + password)
                        xhr.setRequestHeader("Authorization", "Basic " + credentials);
                    },
                    success: function (data, status, request) {
                        $("#console").html(
                            "status: " + request.status + "<br>" +
                            "body: " + request.responseText);
                    },
                    error: function (request, status, error) {
                        $("#console").html(
                            "status: " + request.status + "<br>" +
                            "body: " + request.responseText);
                    }
                });
            });
        });
    </script>
</body>
</html>
