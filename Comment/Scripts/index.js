



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