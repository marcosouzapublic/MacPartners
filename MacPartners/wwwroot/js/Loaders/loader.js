let previousHtml = "";
let loader = '<span class="spinner-border" role="status" aria-hidden="true"></span>';

function setLoader(ele) {
    previousHtml = ele.html();
    ele.html(loader);
}

function removeLoader(ele) {
    ele.html(previousHtml);
    previousHtml = "";
}