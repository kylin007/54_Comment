var datetime = null;

function getTime() {
    var url = document.getElementById("MyIframe").getAttribute("src").toString();
    $.ajax({
        url: '/Home/returnTime',
        type: 'POST', //GET
        dataType: 'json',    //返回的数据格式：json/xml/html/script/jsonp/text
        async: false,
        data: {
            url: url
        },
        success: function (result) {
            var json = eval("(" + result + ")");
            datetime = json[0].Time;
            return json[0].Time;
        },
        error: function (result) {
            var myDate = new Date()
            return myDate;
        }
    });

}
function gotoUrl() {
    var OUrlInput = document.getElementById("UrlInput");
    var UrlInput_value = OUrlInput.value;
    //alert(UrlInput_value);
    var ifSrc = document.getElementsByName("MyIframe")[0].src;
    if (UrlInput_value.indexOf("http")<0) {
        UrlInput_value = "http://" + UrlInput_value;
    }
    document.getElementsByName("MyIframe")[0].src = UrlInput_value;
}
function remove_comment(thisa){
    var Otd = thisa.parentElement.parentElement;
    Otd.parentElement.removeChild(Otd);
}
function addUserComment(userId, userName) {
    var page_url = document.getElementById("MyIframe").getAttribute("src").toString();
    if (page_url.indexOf("http://www.hi-54.com/a-") < 0) {
        layer.alert("请选择五四文章页，页面格式如下：http://www.hi-54.com/a-*")
        return;
    }
    $.ajax({
        url: '/Home/returnOneComment',
        type: 'POST', //GET
        dataType: 'json',    //返回的数据格式：json/xml/html/script/jsonp/text
        async: false,
        success: function (result) {
            var json = eval("(" + result + ")");
            var CommentValue = json[0].CommentValue;
            var time = getTime();

            var Html = "<tr><td class=\"comm_name\"  style=\"line-height:30px;\" data-value=\"" + userId + "\">" + userName + "</td>";
            Html += "<td><input class=\"comment_input form-control comm_value\" value=\"" + CommentValue + "\" /></td>";
            Html += "<td><input class=\"comment_input form-control comm_time\" value=\"" + datetime + "\" /></td>";
            Html += "<td style=\"line-height:30px;\"><a  style=\"cursor: pointer;\" onclick=\"remove_comment(this)\">移除</a></td>";
            Html += "</tr>";
            $("#tab_Com").append(Html);
        },
        error: function (result) {
            layer.alert("生成评论出现错误");
        }
    })
}
function RandomlyAdd() {
    var page_url = document.getElementById("MyIframe").getAttribute("src").toString();
    if (page_url.indexOf("http://www.hi-54.com/a-") < 0) {
        layer.alert("请选择五四文章页，页面格式如下：http://www.hi-54.com/a-*")
        return;
    }
    var number = randomNum(1, 10);
    var errornumber = 0;
    for (var i = 0; i < number; i++) {
        $.ajax({
            url: '/Home/returnUserAndComment',
            type: 'POST', //GET
            dataType: 'json',    //返回的数据格式：json/xml/html/script/jsonp/text
            async: false,
            success: function (result) {
                var json = eval("(" + result + ")");
                var CommentValue = json[0].CommentValue;
                var UserName = json[0].UserName;
                var UserValue = json[0].UserValue;
                var time = getTime();

                var Html = "<tr><td class=\"comm_name\"  style=\"line-height:30px;\" data-value=\"" + UserValue + "\">" + UserName + "</td>";
                Html += "<td><input class=\"comment_input form-control comm_value\" value=\"" + CommentValue + "\" /></td>";
                Html += "<td><input class=\"comment_input form-control comm_time\" value=\"" + datetime + "\" /></td>";
                Html += "<td style=\"line-height:30px;\"><a  style=\"cursor: pointer;\" onclick=\"remove_comment(this)\">移除</a></td>";
                Html += "</tr>";
                $("#tab_Com").append(Html);
            },
            error: function (result) {
                errornumber++
            }
        })
    }
    if(errornumber!=0){
        layer.alert("出现未知错误，仅生成" + number - errornumber + "条");
    }
}
function randomNum(minNum, maxNum) {
    switch (arguments.length) {
        case 1:
            return parseInt(Math.random() * minNum + 1, 10);
            break;
        case 2:
            return parseInt(Math.random() * (maxNum - minNum + 1) + minNum, 10);
            break;
        default:
            return 0;
            break;
    }
}
function Send() {
    var OTable = document.getElementById("tab_Com");
    var OName = OTable.getElementsByClassName("comm_name");
    var OValue = OTable.getElementsByClassName("comm_value");
    var OTime = OTable.getElementsByClassName("comm_time");
    var successnumber = 0;
    var errornumber = 0;
    for (var i = 0; i < OName.length; i++) {
        var NowTime = new Date();

        //2把字符串格式转换为日期类
        var comm_Time = OTime[i].value.toString();
        var comm_Name = OName[i].innerText;
        var comm_Value = OValue[i].value;
        var comm_UId = OName[i].attributes["data-value"].value.toString();

        comm_Time = comm_Time.substring(0, 19);
        comm_Time = comm_Time.replace(/-/g, '/');

        var timestamp = new Date(comm_Time).getTime();
        var now_time = NowTime.getTime();

        if (timestamp > now_time) {
            //存入数据库等待执行
            $.ajax({
                url: '/Home/AddDBComment',
                type: 'POST', //GET
                dataType: 'json',    //返回的数据格式：json/xml/html/script/jsonp/text
                async: false,
                data: {
                    UId: comm_UId,
                    Name: comm_Name,
                    Text: comm_Value,
                    CommTime: comm_Time
                },
                success: function (result) {
                    var json = eval("(" + result + ")");
                    var resultValue = json[0].result;
                    if (resultValue || resultValue == "True") {
                        successnumber++;
                    }
                    else {
                        errornumber++;
                    }
                },
                error: function (result) {
                    errornumber++;
                }
            });
        }
        else {
            //现在执行插入任务
            //alert("现在执行插入任务");
        }

        if (errornumber != 0) {
            layer.alert((OName.length-successnumber-errornumber) + "条数据成功发布\n" + successnumber + "条数据因大于系统时间存入数据库等待执行\n" + errornumber + "条插入数据库失败\n")
        }
        else {
            layer.alert((OName.length - successnumber) + "条数据成功发布\n" + successnumber + "条数据因大于系统时间存入数据库等待执行\n")
        }

        //$.ajax({
        //    url: '/Home/SendComment',
        //    type: 'POST', //GET
        //    dataType: 'json',    //返回的数据格式：json/xml/html/script/jsonp/text
        //    async: false,
        //    data: {
        //        url: url
        //    },
        //    success: function (result) {
        //    },
        //    error: function (result) {
        //    }
        //});
    }
}